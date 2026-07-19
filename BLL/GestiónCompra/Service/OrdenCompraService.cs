using BLL.DomainDtos;
using BLL.GestiónCompra.Exceptions;
using BLL.GestiónCompra.Interface;
using BLL.GestiónCompra.Mapper;
using BLL.GestiónCompra.Validator;
using BLL.Infrastructure;
using DAO.Interface;
using FluentValidation.Results;
using Models;
using System;


namespace BLL.GestiónCompra.Service
{
    public class OrdenCompraService : IOrdenCompraService
    {
        private readonly IUnitOfWork _uow;
        private readonly OrdenCompraDTOValidator _validator = new();
        public OrdenCompraService
        (
            IUnitOfWork uow
        )
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        #region  CRUD 

        public void GenerarOrdenCompra(OrdenCompraDTO dto)
        {
            // 1. Validación Sintáctica Estricta del Contrato
            ValidarDto(dto);

            try
            {
                // 2. Validaciones Lógicas Básicas
                if (dto.IdSucursal == null || dto.IdSucursal == Guid.Empty)
                    throw new ReglaNegocioComprasException("No se especificó la sucursal de destino de la Orden de Compra.");

                if (dto.IdUsuario == null || dto.IdUsuario == Guid.Empty)
                    throw new ReglaNegocioComprasException("No se especificó el usuario responsable de la operación.");

                if (!dto.Detalles.Any())
                    throw new ReglaNegocioComprasException("La orden de compra no contiene productos.");

                // 3. Construcción del Grafo Principal (Maestro)
                Guid idOcNuevo = Guid.NewGuid();

                var nuevaOc = new OrdenCompra
                {
                    IdOrdenCompra = idOcNuevo,
                    IdSucursal = dto.IdSucursal.Value,
                    IdProveedor = dto.IdProveedor.Value,
                    IdUsuario = dto.IdUsuario.Value,
                    IdEstadoOc = 1, // 1 = Pendiente 
                    FechaOc = DateTime.Now,
                    NroSolicitud = CodigoGenerador.GenerarNumeroOcUnicoNumerico(),
                    CostoTotal = dto.CostoTotal, // Magia de tu DTO calculado
                    OrdenCompraDetalle = new List<OrdenCompraDetalle>()
                };

                // 4. Mapeo de Renglones (Detalles)
                foreach (var detDto in dto.Detalles)
                {
                    var ocDetalle = new OrdenCompraDetalle
                    {
                        IdOrdenCompraDetalle = Guid.NewGuid(),
                        IdOrdenCompra = idOcNuevo,
                        IdProducto = detDto.IdProducto,
                        CantidadPedida = detDto.CantidadPedida,
                        CantidadRecibida = 0,
                        PrecioPactado = detDto.PrecioPactado,
                        Renglon = detDto.Renglon,
                        IdProductoNavigation = null,
                        VinculoSolicitudOc = new List<VinculoSolicitudOc>()
                    };

                    nuevaOc.OrdenCompraDetalle.Add(ocDetalle);
                }
                
                // 5. Persistencia Atómica a través del UoW
                _uow.OrdenCompraRepository.Add(nuevaOc); 
                _uow.SaveChanges();

                
            }
            catch (RohanComprasException)
            {
                throw; // Dejamos pasar excepciones de negocio a la UI
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error crítico en la capa de datos al registrar la nueva Orden de Compra Manual.", ex);
            }
        }
        
        public void ModificarEstadoOc(Guid idOc, int nuevoEstadoId)
        {
            try
            {
                var oc = _uow.OrdenCompraRepository.GetById(idOc, incluirDetalles: true);
                if (oc == null)
                    throw new ReglaNegocioComprasException("No se encontró la Orden de Compra solicitada.");

                // Regla: No se puede modificar una OC cancelada
                if (oc.IdEstadoOc == 4) // 4 = Cancelada
                    throw new ReglaNegocioComprasException("Operación inválida: La Orden de Compra ya se encuentra Cancelada.");

                oc.IdEstadoOc = nuevoEstadoId;
                _uow.OrdenCompraRepository.Update(oc);
                _uow.SaveChanges();
            }
            catch (RohanComprasException) { throw; }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error al actualizar el estado de la Orden de Compra.", ex);
            }
        }

        public void CancelarOrdenCompra(Guid idOc)
        {
            try
            {
                var oc = _uow.OrdenCompraRepository.GetById(idOc, incluirDetalles: true);
                if (oc == null)
                    throw new ReglaNegocioComprasException("La Orden de Compra que intenta cancelar no existe.");

                // Regla: Solo se puede cancelar si sigue en estado "Emitida" (sin recepciones parciales)
                if (oc.IdEstadoOc != 1)
                    throw new ReglaNegocioComprasException("No se puede cancelar la OC: Ya registra movimientos de mercadería en recepción.");

                oc.IdEstadoOc = 4; // 4 = Cancelada (Baja lógica)
                _uow.OrdenCompraRepository.Update(oc);
                _uow.SaveChanges();
            }
            catch (RohanComprasException) { throw; }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error al procesar la cancelación de la Orden de Compra.", ex);
            }
        }
      
        public void ActualizarOrdenCompra(OrdenCompraDTO Oc)
        {
            ValidarDto(Oc); 

            try
            {
                // 1. Buscamos la OC Original con sus detalles actuales en la base de datos
                var ocDb = _uow.OrdenCompraRepository.GetById(Oc.IdOrdenCompra, incluirDetalles: true);
                if (ocDb == null) throw new ReglaNegocioComprasException("No se encontró la Orden de Compra.");

                if (ocDb.IdEstadoOc != 1) // 1 = Pendiente
                    throw new ReglaNegocioComprasException("Solo se pueden modificar Órdenes de Compra en estado Pendiente.");

                // 2. Actualizamos cabecera 
                ocDb.CostoTotal = Oc.CostoTotal;
                ocDb.IdUsuario = Oc.IdUsuario.Value;

                // 3. ELIMINAMOS los detalles viejos 
                _uow.OrdenCompraRepository.RemoveDetalle(ocDb.OrdenCompraDetalle);
                ocDb.OrdenCompraDetalle.Clear();

                // 4. INSERTAMOS los detalles nuevos/modificados desde el DTO
                int nroRenglon = 1;
                foreach (var detDto in Oc.Detalles)
                {
                    ocDb.OrdenCompraDetalle.Add(new OrdenCompraDetalle
                    {
                        IdOrdenCompraDetalle = Guid.NewGuid(),
                        IdOrdenCompra = ocDb.IdOrdenCompra,
                        IdProducto = detDto.IdProducto,
                        CantidadPedida = detDto.CantidadPedida,
                        CantidadRecibida = 0,
                        PrecioPactado = detDto.PrecioPactado,
                        Renglon = nroRenglon++
                    });
                }

                // 5. Persistencia Atómica
                _uow.OrdenCompraRepository.Update(ocDb);
                _uow.SaveChanges();
            }
            catch (RohanComprasException) { throw; }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error crítico al actualizar la Orden de Compra.", ex);
            }
        }

        #endregion

        #region Automatización e Indicadores

        public bool VerificarSolicitudesPendientes(Guid idSucursal)
        {
            try
            {
                return _uow.CompraSolicitudQueryRepository.HaySolicitudesPendientesPorSucursal(idSucursal);
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error al consultar el indicador de solicitudes pendientes por sucursal.", ex);
            }
        }

        public void GenerarOcAutomaticasDesdeSolicitudes(Guid idSucursal, Guid idSolicitud)
        {
            // Validaciones 
            if (idSucursal == Guid.Empty || idSolicitud == Guid.Empty)
                throw new ComprasValidationException("Error de contexto: Los identificadores de sucursal y solicitud son obligatorios.");

            try
            {
                // 1. Buscamos únicamente la solicitud marcada por el usuario
                var sol = _uow.SolicitudPedidoRepository.GetById(idSolicitud);

                if (sol == null)
                    throw new ReglaNegocioComprasException("La solicitud de pedido seleccionada no existe en el sistema.");

                if (sol.IdSucursal != idSucursal)
                    throw new ReglaNegocioComprasException("La solicitud seleccionada no pertenece a la sucursal activa en su sesión.");

                if (sol.IdEstadoSolicitud != 1) // 1 = Pendiente
                    throw new ReglaNegocioComprasException("Esta solicitud ya fue procesada o cancelada previamente.");

                // 2. Desglosamos 
                var renglonesValidos = sol.SolicitudPedidoDetalle
                    .Where(d => d.IdProducto != null && (d.Cantidad ?? 0) > 0)
                    .ToList();

                if (!renglonesValidos.Any())
                    throw new ReglaNegocioComprasException("La solicitud no contiene renglones con cantidades válidas para comprar.");

                // 3. Agrupamos los ítems consultando a tu tabla intermedia ProductoProveedor
                var renglonesConContextoProveedor = new List<Tuple<SolicitudPedidoDetalle, Guid, decimal>>();

                foreach (var renglon in renglonesValidos)
                {
                    var relacionPrincipal = _uow.CompraSolicitudQueryRepository
                        .ObtenerRelacionProveedorPrincipal(renglon.IdProducto!.Value);

                    if (relacionPrincipal == null)
                    {
                        var productoFallo = renglon.IdProductoNavigation?.Nombre ?? renglon.IdProducto.ToString();
                        throw new ReglaNegocioComprasException($"No se puede procesar la solicitud. El producto [{productoFallo}] no tiene configurado un Proveedor Principal en el catálogo.");
                    }

                    Guid idProveedorReal = relacionPrincipal.IdProveedorNavigation.IdProveedor;
                    decimal precioHistoricoReal = relacionPrincipal.UltimoPrecioCompra ?? 0.00m;

                    renglonesConContextoProveedor.Add(new Tuple<SolicitudPedidoDetalle, Guid, decimal>(renglon, idProveedorReal, precioHistoricoReal));
                }

                // 4. Agrupamos por el ID del Proveedor resultante 
                var gruposPorProveedor = renglonesConContextoProveedor.GroupBy(r => r.Item2);

                foreach (var grupo in gruposPorProveedor)
                {
                    Guid idProveedor = grupo.Key;
                    Guid idOcNuevo = Guid.NewGuid();

                    var nuevaOc = new OrdenCompra
                    {
                        IdOrdenCompra = idOcNuevo,
                        IdSucursal = idSucursal,       
                        IdProveedor = idProveedor,
                        IdUsuario = sol.IdUsuario,     
                        IdEstadoOc = 1,              
                        FechaOc = DateTime.Now,
                        NroSolicitud = CodigoGenerador.GenerarNumeroOcUnicoNumerico(),
                        IdProveedorNavigation = null,
                        IdEstadoSolicitudNavigation = null,

                        OrdenCompraDetalle = new List<OrdenCompraDetalle>()
                    };

                    int nroRenglonOc = 1;
                    decimal costoTotalAcumulado = 0;

                    foreach (var item in grupo)
                    {
                        decimal precioPactadoInicial = item.Item3;
                        int cantidadPedida = item.Item1.Cantidad ?? 0;

                        var producto = _uow.ProductoRepository.GetById(item.Item1.IdProducto.Value);

                        Guid idDetalleNuevo = Guid.NewGuid();

                        var ocDetalle = new OrdenCompraDetalle
                        {
                            IdOrdenCompraDetalle = idDetalleNuevo,
                            IdOrdenCompra = idOcNuevo,
                            IdProducto = item.Item1.IdProducto,
                            CantidadPedida = cantidadPedida,
                            CantidadRecibida = 0,
                            PrecioPactado = precioPactadoInicial,
                            Renglon = nroRenglonOc,

                            // Truncamos navegación de producto para que EF no intente re-insertarlo
                            IdProductoNavigation = null,
                            VinculoSolicitudOc = new List<VinculoSolicitudOc>()

                        };

                        costoTotalAcumulado += (cantidadPedida * precioPactadoInicial);

           
                        var vinculo = new VinculoSolicitudOc
                        {
                            IdVinculoSolicitudOc = Guid.NewGuid(),
                            IdOrdenCompraDetalle = idDetalleNuevo,
                            IdSolicitudPedidoDetalle = item.Item1.IdSolicitudPedidoDetalle,
                            CantidadAsignada = cantidadPedida,

                            // Aseguramos nulas las propiedades de objeto pesado
                            IdOrdenCompraDetalleNavigation = null,
                            IdSolicitudPedidoDetalleNavigation = null
                           
                        };

                        ocDetalle.VinculoSolicitudOc.Add(vinculo);

                        nuevaOc.OrdenCompraDetalle.Add(ocDetalle);
                        nroRenglonOc++;
                    }

                    nuevaOc.CostoTotal = costoTotalAcumulado;

                    //  Ahora el Add va a ejecutarse de manera limpia y sin conflictos de grafo
                    _uow.OrdenCompraRepository.Add(nuevaOc);
                }

                // 5. Cambiamos el estado de la Solicitud Madre
                sol.IdEstadoSolicitud = 2; // 2 = Aprobada (Procesada)
                _uow.SaveChanges();

                
            }
            catch (RohanComprasException) { throw; }
            catch (Exception ex)
            {
                throw new ComprasDomainException($"Error crítico al unificar la solicitud {idSolicitud} en borradores de Órdenes de Compra.", ex);
            }
        }

        #endregion

        #region Lecturas y Filtros Cruzados

        public OrdenCompraDTO ObtenerPorId(Guid idOc)
        {
            try
            {
                var entity = _uow.OrdenCompraRepository.GetById(idOc, incluirDetalles: true);
                return entity.ToDTO();
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException($"Error de base de datos al buscar la OC con ID {idOc}.", ex);
            }
        }

        public IEnumerable<OrdenCompraDTO> ListarHistorialOc(Guid idSucursal, Guid? idProveedor, int? idEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            // Validación de contexto regional
            if (idSucursal == Guid.Empty)
                throw new ComprasValidationException("Error de contexto: El identificador de la sucursal es obligatorio.");

            try
            {
                // 1. Le pedimos a la DAO los datos ya filtrados por Sucursal y con sus Includes resueltos
                var listaEntidades = _uow.OrdenCompraRepository.GetHistorialConDetalles(idSucursal, fechaDesde, fechaHasta);

                // 2. Aplicamos los filtros dinámicos comerciales en memoria sobre la lista regional
                if (idProveedor.HasValue && idProveedor != Guid.Empty)
                {
                    listaEntidades = listaEntidades.Where(o => o.IdProveedor == idProveedor);
                }

                if (idEstado.HasValue && idEstado > 0)
                {
                    listaEntidades = listaEntidades.Where(o => o.IdEstadoOc == idEstado);
                }

                // 3. Mapeamos de forma limpia a DTOs para la interfaz de usuario
                return listaEntidades.Select(o => o.ToDTO()).ToList();
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error en la capa de negocio al recuperar el historial filtrado de Órdenes de Compra.", ex);
            }
        }

        public IEnumerable<ProductoDTO> ListarProductosDeProveedor(Guid idProveedor)
        {
            try
            {
                // Filtro Inverso: Resuelve tu requerimiento de buscar productos asociados a un proveedor
                var productosEntities = _uow.CompraSolicitudQueryRepository.ObtenerProductosPorProveedor(idProveedor);

                return productosEntities.Select(p => new ProductoDTO
                {
                    Id = p.IdProducto,
                    CodigoSku = p.CodigoSku,
                    Nombre = p.Nombre,
                    CantidadPorBulto = p.CantidadPorBulto ?? 1
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error al listar los productos del proveedor seleccionado.", ex);
            }
        }

        #endregion

        #region Documentación Física (Bloc de Notas)

        public void ExportarOcABlocDeNotas(OrdenCompraDTO ocDto, string rutaDirectorio)
        {
            try
            {
                if (ocDto == null || ocDto.Detalles == null || !ocDto.Detalles.Any())
                    throw new Exception("El DTO de la orden de compra está vacío o incompleto.");

                // Validamos o creamos el directorio
                if (!Directory.Exists(rutaDirectorio))
                    Directory.CreateDirectory(rutaDirectorio);

                // Usamos el número de la OC del DTO
                string nombreArchivo = $"OC_{ocDto.NroOrdenCompra:D6}.txt";
                string rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);

                using (StreamWriter writer = new StreamWriter(rutaCompleta, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("=======================================================================");
                    writer.WriteLine($"        ORDEN DE COMPRA - SISTEMA DE GESTIÓN ROHAN (N° OC-{ocDto.NroOrdenCompra:D6})");
                    writer.WriteLine("=======================================================================");
                    writer.WriteLine($"Fecha de Emisión: {ocDto.FechaOc:dd/MM/yyyy HH:mm}");
                    writer.WriteLine($"Estado Actual   : Pendiente de Recepción");
                    writer.WriteLine("-----------------------------------------------------------------------");
                    writer.WriteLine("DATOS DEL PROVEEDOR:");
                    // Como usamos el DTO, estos datos ya vienen en texto plano, sin riesgo de nulos por EF
                    writer.WriteLine($"Razón Social: {ocDto.RazonSocialProveedor}");
                    writer.WriteLine($"CUIT        : {ocDto.CuitProveedor}");
                    writer.WriteLine("=======================================================================");
                    writer.WriteLine(string.Format("| {0,-4} | {1,-8} | {2,-30} | {3,-10} | {4,-10} |", "REN", "SKU", "PRODUCTO", "CANT.PED", "PREC.PACT"));
                    writer.WriteLine("-----------------------------------------------------------------------");

                    foreach (var det in ocDto.Detalles.OrderBy(d => d.Renglon))
                    {
                        string nombreProdCorto = det.ProductoNombre.Length > 30
                            ? det.ProductoNombre.Substring(0, 27) + "..."
                            : det.ProductoNombre;

                        writer.WriteLine(string.Format("| {0,-4} | {1,-8} | {2,-30} | {3,-10} | ${4,-9:F2} |",
                            det.Renglon,
                            det.CodigoSku,
                            nombreProdCorto,
                            det.CantidadPedida,
                            det.PrecioPactado));
                    }

                    writer.WriteLine("=======================================================================");
                    writer.WriteLine(string.Format("COSTO TOTAL DE LA ORDEN: ${0:F2}", ocDto.CostoTotal));
                    writer.WriteLine("=======================================================================");
                    writer.WriteLine("Documento oficial generado de forma automática por el departamento de Compras.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error de I/O al intentar escribir el archivo físico de la Orden de Compra.", ex);
            }
        }

        private void ValidarDto(OrdenCompraDTO dto)
        {
            if (dto == null)
                throw new ComprasValidationException("El objeto Orden de Compra no puede ser nulo.");

            if (_validator == null)
            {
                throw new ComprasDomainException("Error interno: El validador de Órdenes de Compra no fue inicializado correctamente.");
            }

            ValidationResult validationResult;

            try
            {
                // Ejecuta FluentValidation sobre el DTO maestro y sus detalles en cascada
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error interno de infraestructura al validar la Orden de Compra.", ex);
            }

            if (!validationResult.IsValid)
            {
                // Extraemos el primer error de la lista de FluentValidation de forma segura
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                                     ?? "Error de validación comercial desconocido.";

                // Arrojamos nuestra excepción específica de validación sintáctica
                throw new ComprasValidationException(primerError);
            }
        }

        public IEnumerable<SolicitudPedidoDTO> ObtenerSolicitudesPendientesPorSucursal(Guid idSucursal)
        {
            if (idSucursal == Guid.Empty)
                throw new ComprasValidationException("Error de contexto: El identificador de la sucursal actual no es válido.");

            try
            {
                // 1. Consultamos a la DAL usando el repositorio puente de compras (o el de solicitudes si lo unificaste ahí)
                // Este método ya lo tenés implementado y hace el .Where(s => s.IdEstadoSolicitud == 1 && s.IdSucursal == idSucursal)
                var solicitudesEntidades = _uow.CompraSolicitudQueryRepository.ObtenerSolicitudesPendientesPorSucursal(idSucursal);

                if (solicitudesEntidades == null)
                    return new List<SolicitudPedidoDTO>();

                // 2. Transmutación en cascada de Entidades físicas a DTOs ricos usando tu estructura de Mappers
                // Nota: Asegurate de usar el mapper de solicitudes que ya tenías operativo en la entrega anterior
                var listaDtos = solicitudesEntidades
                    .Select(solicitud => solicitud.ToDTO())
                    .ToList();

                return listaDtos;
            }
            catch (Exception ex)
            {
                // Si hay una caída de red o error en los Includes de EF, lo envolvemos en una excepción de infraestructura
                throw new ComprasDomainException($"Error crítico de infraestructura al intentar recuperar las solicitudes de la sucursal {idSucursal}.", ex);
            }
        }

        public IEnumerable<OrdenCompraDTO> ConsultarHistorial(Guid idSucursal, Guid? idProveedor, int? idEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            // 1. Validación 
            if (idSucursal == Guid.Empty)
                throw new ComprasValidationException("Error de contexto: El identificador de la sucursal es obligatorio para consultar el historial.");

            try
            {
                // 2. Consumimos el método especializado de la DAO (Trae los datos con Includes resueltos desde SQL)
                var listaEntidades = _uow.OrdenCompraRepository.GetHistorialConDetalles(idSucursal, fechaDesde, fechaHasta);

                // Si la base de datos por algún motivo devuelve nulo, devolvemos una lista vacía para no romper la UI
                if (listaEntidades == null)
                    return new List<OrdenCompraDTO>();

                // 3. Aplicamos filtros dinámicos comerciales en memoria sobre la colección regional

                // Filtro por Proveedor Específico
                if (idProveedor.HasValue && idProveedor.Value != Guid.Empty)
                {
                    listaEntidades = listaEntidades.Where(oc => oc.IdProveedor == idProveedor.Value);
                }

                // Filtro por Estado (Sincronizado con tu Enum/Tabla de soporte)
                // Evaluamos > 0 por si pasás un 0 desde el combo para significar "-- Todos --"
                if (idEstado.HasValue && idEstado.Value > 0)
                {
                    listaEntidades = listaEntidades.Where(oc => oc.IdEstadoOc == idEstado.Value);
                }

                // 4. Transformación atómica en cascada usando tu clase de extensión de mappers enriquecida
                // Esto ya arrastra los nombres de los productos, SKUs, nombres de usuario y costos calculados
                return listaEntidades
                    .Select(oc => oc.ToDTO())
                    .ToList();
            }
            catch (RohanComprasException)
            {
                // Re-lanzamos excepciones de reglas de negocio directas a la fachada/UI
                throw;
            }
            catch (Exception ex)
            {
                // Encapsulamos cualquier error de infraestructura imprevisto para no romper el hilo principal
                throw new ComprasDomainException($"Fallo crítico en la capa de negocio al procesar la auditoría del historial de Órdenes de Compra para la sucursal {idSucursal}.", ex);
            }
        }
  
        #endregion
    }
}
