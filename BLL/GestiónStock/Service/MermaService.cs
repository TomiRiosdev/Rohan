using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónStock.Exceptions;
using BLL.GestiónStock.Interface;
using DAO.Interface;


namespace BLL.GestiónStock
{
    public class MermaService : IMermaService
    {
        private readonly IUnitOfWork _uow;

        public MermaService
        (
            IUnitOfWork uow
        )
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Recorre analíticamente los inventarios consolidados y los lotes activos de una sucursal para compilar 
        /// el Tablero de Alertas Tempranas de Depósito, aislando quiebres de stock crítico y mermas preventivas por vencimiento.
        /// </summary>
        /// <param name="idSucursal">Guid de la sucursal bajo análisis.</param>
        /// <returns>Colección de DTOs de Alerta listos para activar los colores del semáforo visual de la UI.</returns>
        /// <exception cref="StockDomainException">Lanzada si falla el procesamiento predictivo o las consultas SQL Server.</exception>
        public IEnumerable<InventarioAlertaDTO> ObtenerAlertasInventario(Guid idSucursal)
        {
            try
            {
                if (idSucursal == Guid.Empty)
                    throw new StockValidationException("No se puede computar el tablero analítico sin una sucursal de contexto válida.");

                var alertas = new List<InventarioAlertaDTO>();
                DateTime hoy = DateTime.Today;
                DateTime limiteProximoVencer = hoy.AddDays(15); // Margen preventivo de resguardo de 15 días

                // REGLA 1: QUIEBRE DE STOCK MÍNIMO (Análisis del Consolidado)
                var stockConsolidado = _uow.StockPorSucursalRepository.GetConsolidadoBySucursal(idSucursal);

                foreach (var item in stockConsolidado)
                {
                    int total = item.CantidadTotal ?? 0;
                    int minimo = item.StockMinimo ?? 0;

                    if (total <= minimo)
                    {
                        alertas.Add(new InventarioAlertaDTO
                        {
                            IdProducto = item.IdProducto,
                            ProductoNombre = item.IdProductoNavigation?.Nombre ?? "Producto Desconocido",
                            TipoAlerta = TipoAlertaEnum.StockBajo.ToString().ToUpper(),
                            DetalleMensaje = $"Alerta de reposición: El volumen físico disponible ({total} u.) perforó el límite de resguardo ({minimo} u.).",
                            CantidadAfectada = total
                        });
                    }
                }

                // REGLA 2: CONTROL DE FRESCURA Y ROTACIÓN (Análisis de Lotes con Stock Disponible)
                var lotesActivos = _uow.LoteRepository.GetLotesActivosPorSucursal(idSucursal)
                    .Where(l => l.CantidadActual > 0)
                    .ToList();

                foreach (var lote in lotesActivos)
                {
                    if (!lote.FechaIngreso.HasValue) continue;

                    // 💡 Nota Logística: Al agregar FechaVencimiento en tu Lote, reemplazar esta simulación por 'lote.FechaVencimiento.Value'
                    DateTime fechaVence = lote.FechaIngreso.Value.AddMonths(6).Date;
                    int cantidadLote = lote.CantidadActual ?? 0;

                    // Caso A: El lote ya caducó adentro del depósito (Merma Crítica irreversible)
                    if (fechaVence < hoy)
                    {
                        alertas.Add(new InventarioAlertaDTO
                        {
                            IdProducto = lote.IdProducto ?? Guid.Empty,
                            ProductoNombre = lote.IdProductoNavigation?.Nombre ?? "Materia Prima No Identificada",
                            TipoAlerta = TipoAlertaEnum.Vencido.ToString().ToUpper(),
                            DetalleMensaje = $"¡MERMA CRÍTICA DE DESECHO! El lote [{lote.NumeroLote}] expiró su vida útil el {fechaVence:dd/MM/yyyy}.",
                            CantidadAfectada = cantidadLote,
                            NumeroLote = lote.NumeroLote,
                            FechaVencimiento = fechaVence
                        });
                    }
                    // Caso B: Ventana preventiva (El lote va a vencer dentro de los próximos 15 días)
                    else if (fechaVence >= hoy && fechaVence <= limiteProximoVencer)
                    {
                        int diasRestantes = (fechaVence - hoy).Days;
                        alertas.Add(new InventarioAlertaDTO
                        {
                            IdProducto = lote.IdProducto ?? Guid.Empty,
                            ProductoNombre = lote.IdProductoNavigation?.Nombre ?? "Materia Prima No Identificada",
                            TipoAlerta = TipoAlertaEnum.ProximoAVencer.ToString().ToUpper(),
                            DetalleMensaje = $"Merma Preventiva (Acelerar Rotación): Lote [{lote.NumeroLote}] caducará en {diasRestantes} días.",
                            CantidadAfectada = cantidadLote,
                            NumeroLote = lote.NumeroLote,
                            FechaVencimiento = fechaVence
                        });
                    }
                }

                return alertas;
            }
            catch (RohanStockException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new StockDomainException("Error de cómputo algorítmico al intentar consolidar el reporte de mermas preventivas.", ex);
            }
        }

        public ConfiguracionAlertasDTO ObtenerAlertasPorProducto(Guid idProducto)
        {
            // Buscamos el producto en las tablas maestras
            var p = _uow.ProductoRepository.GetById(idProducto)
                ?? throw new StockDomainException("El producto seleccionado no existe en el sistema.");

            // Buscamos si ya tiene un registro base en StockPorSucursal para leer sus máximos/mínimos.
            var stockCon = _uow.StockPorSucursalRepository.GetAll()
                .FirstOrDefault(s => s.IdProducto == idProducto);

            return new ConfiguracionAlertasDTO
            {
                IdProducto = p.IdProducto,
                CodigoSku = p.CodigoSku ?? 0,
                ProductoNombre = p.Nombre ?? "Desconocido",
                // Si no tiene stock registrado aún, sugerimos valores por defecto
                StockMinimo = stockCon?.StockMinimo ?? 10,
                StockMaximo = stockCon?.StockMaximo ?? 10,

                DiasVidaUtil = p.DiasVidaUtil,
                DiasAlertaVencimiento = p.DiasAlertaVencimiento
            };
        }
        public void GuardarConfiguracionAlertas(ConfiguracionAlertasDTO dto)
        {
            try
            {
                // 1. Actualizamos la plantilla de vencimientos en el maestro de Productos
                var p = _uow.ProductoRepository.GetById(dto.IdProducto)
                    ?? throw new StockDomainException("Producto inexistente.");

                p.DiasVidaUtil = dto.DiasVidaUtil > 0 ? dto.DiasVidaUtil : null;
                p.DiasAlertaVencimiento = dto.DiasAlertaVencimiento > 0 ? dto.DiasAlertaVencimiento : null;
                _uow.ProductoRepository.Update(p);

                // 2. Actualizamos los límites operativos de máximos y mínimos en la tabla StockPorSucursal
                var stockCon = _uow.StockPorSucursalRepository.GetAll()
                    .FirstOrDefault(s => s.IdProducto == dto.IdProducto);

                if (stockCon != null)
                {
                    stockCon.StockMinimo = dto.StockMinimo;
                    stockCon.StockMaximo = dto.StockMaximo;
                    _uow.StockPorSucursalRepository.Update(stockCon);
                }

                // 3. Confirmación Atómica en SQL Server
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new StockDomainException("Error crítico al intentar guardar los parámetros de control de inventario.", ex);
            }
        }

        public List<LoteDetalleVencimientoDTO> ObtenerLotesPorProducto(Guid idProducto, Guid idSucursal)
        {
            return _uow.LoteRepository.GetLotesActivosPorSucursal(idSucursal)
                .Where(l => l.IdProducto == idProducto
                         && l.IdSucursal == idSucursal
                         && (l.CantidadActual ?? 0) > 0)
                .Select(l => new LoteDetalleVencimientoDTO
                {
                    NumeroLote = l.NumeroLote ?? "Sin Identificar",
                    CantidadInicial = l.CantidadInicial ?? 0,
                    CantidadActual = l.CantidadActual ?? 0,
                    FechaIngreso = l.FechaIngreso ?? DateTime.Now,
                    FechaVencimiento = l.FechaVencimiento
                })
                .OrderBy(l => l.FechaVencimiento)
                .ToList();
        }
    }
}