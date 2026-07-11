using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónStock.Exceptions;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Mapper;
using BLL.Infrastructure;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;
using Models;

namespace BLL.GestiónStock
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<StockPorSucursalDTO> _validator;
        private readonly IKardexService _kardex;

        public StockService
        (
            IUnitOfWork uow,
            IValidator<StockPorSucursalDTO> validator,
            IKardexService kardex
        )
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _kardex = kardex ?? throw new ArgumentNullException(nameof(kardex));
        }

        /// <summary>
        /// Procesa un ajuste de inventario manual desde la UI. 
        /// Valida las reglas de negocio iniciales, calcula el equivalente en unidades individuales netas basándose 
        /// en el factor de bultos del maestro de productos, crea el lote físico y delega la auditoría al KardexService.
        /// </summary>
        /// <param name="stockDto">Datos de transferencia capturados en el formulario manual.</param>
        /// <param name="idSucursal">Identificador de la sucursal activa de la sesión.</param>
        /// <exception cref="StockValidationException">Lanzada si los datos del DTO fallan las reglas de FluentValidation.</exception>
        /// <exception cref="TechoOperativoException">Lanzada si el ingreso supera el límite máximo permitido para el producto.</exception>
        /// <exception cref="StockDomainException">Lanzada ante fallas críticas en el servidor o infraestructura de datos.</exception>
        public void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal, string usuarioNombre)
        {
            try
            {
                // 1. Ejecutar Validaciones de Estructura e Interfaz
                ValidarDto(stockDto);

                if (idSucursal == Guid.Empty)
                    throw new StockValidationException("El contexto de la sucursal provisto es inválido o no se encuentra activo.");

                // 2. Normalización Logística: Traducir Bultos Cerrados a Unidades Sueltas Mínimas
                int unidadesIndividualesNetas = 0;
                if (stockDto.EsIngresoPorBulto)
                {
                    unidadesIndividualesNetas = stockDto.CantidadTotal * stockDto.CantidadPorBulto;
                }
                else
                {
                    unidadesIndividualesNetas = stockDto.CantidadTotal;
                }

                TipoMovimientoEnum tipoMovimiento = (TipoMovimientoEnum)stockDto.IdTipoMovimiento;

                // 3. Control Analítico de Regla de Negocio: Techo Operativo (Máximos de Resguardo)
                if (tipoMovimiento == TipoMovimientoEnum.IngresoManual && unidadesIndividualesNetas > stockDto.StockMaximo)
                {
                    throw new TechoOperativoException(stockDto.StockMaximo, unidadesIndividualesNetas);
                }
                // Control de vencimiento al ingresar mercadería: Si el producto tiene una vida útil definida, calcular la fecha de vencimiento estimada
                DateTime? fechaVencimientoCalculada = null;
               
                int diasParaSumar = stockDto.DiasVidaUtil ?? 0;

                if (diasParaSumar == 0)
                {
                    var productoMaestro = _uow.ProductoRepository.GetById(stockDto.IdProducto);

                  
                    diasParaSumar = productoMaestro?.DiasVidaUtil ?? 0;
                }

                if (diasParaSumar > 0)
                {
                    fechaVencimientoCalculada = DateTime.Today.AddDays(diasParaSumar);
                }

                // 4. Persistencia de la Trazabilidad: Creación del Lote Físico de Mercadería
                var nuevoLote = new Lote
                {
                    IdLote = Guid.NewGuid(),
                    IdProducto = stockDto.IdProducto,
                    IdSucursal = idSucursal,
                    CantidadInicial = unidadesIndividualesNetas,
                    CantidadActual = unidadesIndividualesNetas > 0 ? unidadesIndividualesNetas : 0,
                    CostoUnitario = stockDto.CostoUnitario,
                    FechaIngreso = DateTime.Now,
                    NumeroLote = string.IsNullOrEmpty(stockDto.NumeroLote)
                                 ? $"MAN-{DateTime.Now:yyyyMMddHHmmss}"
                        :        stockDto.NumeroLote,
                    FechaVencimiento = fechaVencimientoCalculada
                };
                _uow.LoteRepository.Add(nuevoLote);

                // 5. Orquestación: Enviar el Lote y las Unidades Netas Calculadas al Motor de Auditoría
                string comentarioFinal = string.IsNullOrEmpty(stockDto.Observaciones)
                ? $"Ajuste manual de stock ({tipoMovimiento}). Lote: {nuevoLote.NumeroLote}"
                : stockDto.Observaciones;

                _kardex.RegistrarMovimiento(idSucursal, nuevoLote, tipoMovimiento, unidadesIndividualesNetas, comentarioFinal, usuarioNombre);


            }
            catch (RohanStockException)
            {
                throw; // Relanzamos nuestras excepciones controladas directas a la UI
            }
            catch (Exception ex)
            {
                var context = ExceptionContext.Crear(ex, new object[] { stockDto, idSucursal });
                ExceptionLogger.Log(context);

                throw new StockDomainException("Error crítico interno al orquestar el ajuste de inventario en el servidor.", ex);
            }
        }

        /// <summary>
        /// Registra el ingreso masivo de mercadería asociado a un documento comercial (Orden de Compra / Remito).
        /// </summary>
        public  void RegistrarIngresoPorOrdenCompra(Guid idOrdenCompra, Guid idSucursal, string usuarioNombre, List<RecepcionMercaderiaDTO> detalleRecepcion)
        {
            try
            {
                // 1. Validaciones Defensivas
                if (idOrdenCompra == Guid.Empty)
                    throw new StockValidationException("El identificador de la Orden de Compra es requerido.");

                if (idSucursal == Guid.Empty)
                    throw new StockValidationException("El contexto de la sucursal es inválido.");

                if (detalleRecepcion == null || !detalleRecepcion.Any())
                    throw new StockValidationException("Debe enviar al menos un artículo para registrar el ingreso.");

                // 2. Recuperación del grafo de la BD
                var ordenCompra = _uow.OrdenCompraRepository.GetById(idOrdenCompra, incluirDetalles: true);

                if (ordenCompra == null)
                    throw new StockValidationException($"No se encontró la Orden de Compra (ID: {idOrdenCompra}).");

                if (ordenCompra.IdSucursal != idSucursal)
                    throw new StockValidationException("La Orden de Compra no pertenece a esta sucursal.");

                //  4 es Finalizada. Si ya está en 4, bloquea.
                if (ordenCompra.IdEstadoOc == 4)
                    throw new StockValidationException("Esta Orden de Compra ya fue finalizada y no admite nuevos ingresos.");

                // 3. Iteración cruzada: BD vs Datos de la UI
                int totalPedidoOriginal = 0;
                int totalRealIngresado = 0;

                foreach (var detalleBd in ordenCompra.OrdenCompraDetalle)
                {
                    totalPedidoOriginal += detalleBd.CantidadPedida ?? 0;

                    // Buscamos lo que el usuario tipeó en la grilla para este renglón exacto
                    var ingresoUi = detalleRecepcion.FirstOrDefault(d => d.IdOrdenCompraDetalle == detalleBd.IdOrdenCompraDetalle);

                    // Si el usuario indicó que llegó mercadería (> 0)
                    if (ingresoUi != null && ingresoUi.CantidadRealRecibida > 0)
                    {
                        int unidadesFisicasNetas = ingresoUi.CantidadRealRecibida * ingresoUi.UnidadesPorBulto;

                        var productoMaestro = detalleBd.IdProductoNavigation;
                        if (productoMaestro == null)
                            throw new StockDomainException($"Falta navegación para el producto ID {detalleBd.IdProducto}.");

                        // Cálculo de Vencimiento
                        DateTime? fechaVencimientoCalculada = null;
                        if (productoMaestro.DiasVidaUtil > 0)
                        {
                            fechaVencimientoCalculada = DateTime.Today.AddDays(productoMaestro.DiasVidaUtil ?? 0);
                        }

                        // 4. Creación del Lote Físico con la cantidad REAL recibida
                        var nuevoLote = new Lote
                        {
                            IdLote = Guid.NewGuid(),
                            IdProducto = detalleBd.IdProducto,
                            IdSucursal = idSucursal,
                            CantidadInicial = ingresoUi.CantidadRealRecibida,
                            CantidadActual = ingresoUi.CantidadRealRecibida,
                            CostoUnitario = detalleBd.PrecioPactado,
                            FechaIngreso = DateTime.Now,
                            NumeroLote = $"OC-{DateTime.Now:yyyyMMddHHmmss}-{ordenCompra.IdOrdenCompra.ToString().Substring(0, 4).ToUpper()}",
                            FechaVencimiento = fechaVencimientoCalculada,
                            IdOrdenCompraDetalle = detalleBd.IdOrdenCompraDetalle
                        };

                        _uow.LoteRepository.Add(nuevoLote);

                        // 5. Motor de Auditoría
                        string obsKardex = string.IsNullOrWhiteSpace(ingresoUi.Observaciones)
                            ? $"Ingreso por OC. Lote: {nuevoLote.NumeroLote}"
                            : $"OC: {ingresoUi.Observaciones} - Lote: {nuevoLote.NumeroLote}";

                        _kardex.RegistrarMovimiento(
                            idSucursal,
                            nuevoLote,
                            TipoMovimientoEnum.IngresoPorCompra,
                            ingresoUi.CantidadRealRecibida,
                            obsKardex,
                            usuarioNombre
                        );

                        // Actualizar métricas del renglón en la BD
                        detalleBd.CantidadRecibida += ingresoUi.CantidadRealRecibida;
                        totalRealIngresado += detalleBd.CantidadRecibida ?? 0;
                    }
                }

                // 6. Evaluación Automática de Estados
                // Si ingresó todo lo que se pidió (o más), se cierra (Estado 4 = Finalizado)
                if (totalRealIngresado >= totalPedidoOriginal)
                {
                    ordenCompra.IdEstadoOc = 4;
                }
                else
                {
                    // Si faltó mercadería, se marca como Parcial (Ej: Estado 5). 
                    // Podés cambiar el '5' por el ID real que uses en tu diccionario para ingresos incompletos.
                    ordenCompra.IdEstadoOc = 2;
                }

                _uow.OrdenCompraRepository.Update(ordenCompra);

                // 7. Transacción Atómica a SQL
                _uow.SaveChanges();
            }
            catch (RohanStockException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var context = ExceptionContext.Crear(ex, new object[] { idOrdenCompra, idSucursal });
                ExceptionLogger.Log(context);
                throw new StockDomainException($"Error al orquestar el ingreso de la OC {idOrdenCompra}.", ex);
            }

        }

        /// <summary>
        /// Consulta la base de datos para recuperar la foto actual consolidada del inventario físico de una sucursal.
        /// Mapea los registros a DTOs enriquecidos con propiedades calculadas para el DataGridView.
        /// </summary>
        /// <param name="idSucursal">Guid de la sucursal a consultar.</param>
        /// <returns>Colección de DTOs listos para enlazar a la interfaz gráfica.</returns>
        public IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal)
        {
            try
            {
                if (idSucursal == Guid.Empty) return Enumerable.Empty<StockPorSucursalDTO>();

                // 1. Recuperamos la lista consolidada base
                var listaEntities = _uow.StockPorSucursalRepository.GetConsolidadoBySucursal(idSucursal);

                // 2. Traemos de la DB todos los lotes activos (con existencias reales) de esta sucursal
                var lotesActivos = _uow.LoteRepository.GetLotesActivosPorSucursal(idSucursal)
                    .Where(l => (l.CantidadActual ?? 0) > 0)
                    .ToList();

                // 3. Mapeamos y cruzamos la información analítica de mermas
                var listaDtos = listaEntities.Select(s => s.ToDTO()).ToList();

                foreach (var dto in listaDtos)
                {
                    var lotesDelProducto = lotesActivos.Where(l => l.IdProducto == dto.IdProducto).ToList();     
                    dto.TieneLotesVencidos = lotesDelProducto.Any(l => l.FechaVencimiento.HasValue
                                                                   && l.FechaVencimiento.Value.Date < DateTime.Today);
                }

                return listaDtos;
            }
            catch (Exception ex)
            {
                throw new StockDomainException("No se pudo recuperar el consolidado de stock debido a una falla en la consulta de persistencia.", ex);
            }
        }

        #region Métodos Privados Auxiliares

        private void ValidarDto(StockPorSucursalDTO stockDto)
        {
            if (stockDto == null)
                throw new StockValidationException("La estructura de los datos de transferencia de stock es nula.");

            ValidationResult validacion = _validator.Validate(stockDto);
            if (!validacion.IsValid)
            {
                var primerError = validacion.Errors.First().ErrorMessage;
                throw new StockValidationException(primerError);
            }
        }

        public void RegistrarMermaLote(Guid idLote, int cantidadABajar, string observaciones, Guid idSucursal)
        {
            try
            {
                if (cantidadABajar <= 0)
                    throw new StockValidationException("La cantidad a dar de baja por merma debe ser mayor a cero.");

                // 1. Recuperamos el lote físico directo de la base de datos
                var loteDb = _uow.LoteRepository.GetById(idLote)
                    ?? throw new StockDomainException("El lote seleccionado no existe o ya fue eliminado.");

                // 2. Control de Regla de Negocio: No podés mermar más de lo que realmente hay en el estante
                if (cantidadABajar > loteDb.CantidadActual)
                    throw new StockValidationException($"Operación inválida. El lote solo dispone de {loteDb.CantidadActual} u. y se intentaron mermar {cantidadABajar} u.");

                // 3. Restamos las unidades del lote específico
                loteDb.CantidadActual -= cantidadABajar;
                _uow.LoteRepository.Update(loteDb);

                // 4. Orquestamos la auditoría y el descuento del consolidado reutilizando el Kardex
                // Le pasamos el Enum de Merma. Tu Kardex se va a encargar solito de multiplicar por -1 
                // y restar del stock general de la sucursal, ejecutando el SaveChanges() al final.
                string comentarioAuditoria = string.IsNullOrEmpty(observaciones)
                    ? $"Baja por Merma/Vencimiento. Lote afectado: {loteDb.NumeroLote}"
                    : observaciones;

                _kardex.RegistrarMovimiento(idSucursal, loteDb, TipoMovimientoEnum.EgresoPorMerma, cantidadABajar, comentarioAuditoria, observaciones);
            }
            catch (RohanStockException)
            {
                throw; // Excepciones de validación suben limpio a la UI
            }
            catch (Exception ex)
            {
                // Si explota SQL, capturamos el desastre con tu nuevo Logger
                var context = ExceptionContext.Crear(ex, new object[] { idLote, cantidadABajar, observaciones, idSucursal });
                ExceptionLogger.Log(context);

                throw new StockDomainException("Falla crítica al procesar la baja por merma sanitaria en el servidor.", ex);
            }
        }

        public void RegistrarEgresoManualLote(Guid idLote, int cantidadADescontar, string observaciones, Guid idSucursal, string usuarioNombre)
        {
            try
            {
                if (cantidadADescontar <= 0)
                    throw new StockValidationException("La cantidad a descontar debe ser mayor a cero.");

                // 1. Recuperamos el lote físico
                var loteDb = _uow.LoteRepository.GetById(idLote)
                    ?? throw new StockDomainException("El lote seleccionado no existe o ya fue eliminado.");

                // 2. Control Crítico de Stock Físico
                if (cantidadADescontar > loteDb.CantidadActual)
                    throw new StockValidationException($"Operación inválida. El lote dispone de {loteDb.CantidadActual} u. y se intentaron descontar {cantidadADescontar} u.");

                // 3. Restamos las unidades del lote específico
                loteDb.CantidadActual -= cantidadADescontar;
                _uow.LoteRepository.Update(loteDb);

                // 4. Auditoría y Kardex (¡La magia de tu arquitectura!)
                string comentarioAuditoria = string.IsNullOrWhiteSpace(observaciones)
                    ? $"Descuento manual de stock. Lote afectado: {loteDb.NumeroLote}"
                    : observaciones;

                // IMPORTANTE: Cambia 'EgresoManual' por el Enum exacto que tengas para bajas que NO son merma
                _kardex.RegistrarMovimiento(idSucursal, loteDb, TipoMovimientoEnum.EgresoManual, cantidadADescontar, comentarioAuditoria, usuarioNombre);
            }
            catch (RohanStockException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var context = ExceptionContext.Crear(ex, new object[] { idLote, cantidadADescontar, observaciones, idSucursal });
                ExceptionLogger.Log(context);
                throw new StockDomainException("Falla crítica al procesar el descuento manual de stock en el servidor.", ex);
            }
        }



        #endregion
    }
}
