using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónCompra.Exceptions;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Mapper;
using BLL.Infrastructure;
using DAO.Interface;
using Models;
using System;

namespace BLL.GestiónStock.Service
{
    public class TraspasoService : ITraspasoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IKardexService _kardex;

        public TraspasoService
        (
            IUnitOfWork unitOfWork,
            IKardexService kardexService
        )
        {
            _uow = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _kardex = kardexService ?? throw new ArgumentNullException(nameof(kardexService));
        }

        public void CancelarTraspaso(Guid idOrdenTraspaso, string usuarioNombre)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Confirma el envío físico de la mercadería desde el depósito hacia la sucursal de destino.
        /// Ejecuta el algoritmo FIFO para descontar el stock de los lotes disponibles, genera los 
        /// movimientos contables de egreso, resuelve la partición de renglones si se usan múltiples lotes, 
        /// y avanza el estado de la orden a "En Tránsito".
        /// </summary>
        /// <param name="idOrdenTraspaso">El identificador de la orden de traspaso en preparación.</param>
        /// <param name="usuarioNombre">El nombre del operador que confirma el envío (para auditoría en MovimientosStock).</param>
        /// <param name="detallesConfirmados">La lista de detalles (DTO) con las cantidades exactas validadas por el operario.</param>
        /// <exception cref="ComprasValidationException">Se lanza si los datos de entrada son nulos o inválidos.</exception>
        /// <exception cref="ReglaNegocioComprasException">Se lanza si no hay stock físico suficiente para cubrir el envío.</exception>
        public void ConfirmarEnvioTraspaso(Guid idOrdenTraspaso, string usuarioNombre, List<OrdenTraspasoDetalleDTO> detallesConfirmados)
        {
            if (idOrdenTraspaso == Guid.Empty || !detallesConfirmados.Any())
                throw new ComprasValidationException("Datos de confirmación inválidos.");

            try
            {
                var orden = _uow.OrdenTraspasoRepository.GetById(idOrdenTraspaso);

                if (orden == null || orden.IdEstado != 1)
                    throw new ReglaNegocioComprasException("El traspaso no existe o no está en estado Pendiente.");

                Guid idSucursalOrigen = orden.IdSucursalOrigen.Value;
                Guid idSucursalDestino = orden.IdSucursalDestino.Value;

                foreach (var detConfirmado in detallesConfirmados)
                {
                    if (detConfirmado.CantidadEnviada <= 0) continue;

                    var lotesDisponibles = _uow.LoteRepository.GetLotesActivosPorSucursal(idSucursalOrigen)
                        .Where(l => l.IdProducto == detConfirmado.IdProducto)
                        .OrderBy(l => l.FechaVencimiento ?? DateTime.MaxValue)
                        .ThenBy(l => l.FechaIngreso)
                        .ToList();

                    int cantidadARestar = detConfirmado.CantidadEnviada;

                    foreach (var lote in lotesDisponibles)
                    {
                        if (cantidadARestar == 0) break;

                        int cantidadATomarDelLote = Math.Min(cantidadARestar, lote.CantidadActual.Value);

                        lote.CantidadActual -= cantidadATomarDelLote;
                        cantidadARestar -= cantidadATomarDelLote; // Actualizamos lo que falta

                        _kardex.RegistrarMovimiento(
                             idSucursalOrigen,
                             lote,
                             TipoMovimientoEnum.EgresoPorTransferencia,
                             cantidadATomarDelLote,
                             $"Envío Traspaso N° {orden.NroTraspaso}",
                             usuarioNombre
                        );

                        var detalleDb = orden.OrdenTraspasoDetalle.FirstOrDefault(d => d.IdOrdenTraspasoDetalle == detConfirmado.IdOrdenTraspasoDetalle);

                        if (detalleDb.IdLoteOrigen == null)
                        {
                            detalleDb.IdLoteOrigen = lote.IdLote;
                            detalleDb.CantidadEnviada = cantidadATomarDelLote;
                        }
                        else
                        {
                            var nuevoDetalleRenglon = new OrdenTraspasoDetalle
                            {
                                IdOrdenTraspasoDetalle = Guid.NewGuid(),
                                IdOrdenTraspaso = orden.IdOrdenTraspaso,
                                IdProducto = detalleDb.IdProducto,
                                CantidadEnviada = cantidadATomarDelLote,
                                CantidadRecibida = 0,
                                Renglon = detalleDb.Renglon,
                                IdLoteOrigen = lote.IdLote
                            };
                            orden.OrdenTraspasoDetalle.Add(nuevoDetalleRenglon);
                        }
                    } 
                    if (cantidadARestar > 0)
                    {
                        var prod = _uow.ProductoRepository.GetById(detConfirmado.IdProducto);
                        throw new ReglaNegocioComprasException($"No hay stock físico suficiente para cubrir el envío de {prod.Nombre}. Faltan {cantidadARestar} unidades.");
                    }
                }

                // Según tu nueva regla, pasa a estado 5 (Preparación / Listo para ingresar)
                orden.IdEstado = 5;
                orden.FechaEmision = DateTime.Now;

                _uow.SaveChanges();
            }
            catch (ReglaNegocioComprasException) { throw; }
            catch (Exception ex)
            {
                throw new ComprasDomainException($"Error crítico al confirmar el envío del traspaso {idOrdenTraspaso}.", ex);
            }
        }

        /// <summary>
        /// Genera una nueva Orden de Traspaso (cabecera y detalle) en estado "Preparación" a partir de una 
        /// Solicitud de Pedido pendiente. Funciona como una "orden de trabajo" para el depósito, 
        /// sin afectar aún el inventario físico ni comprometer lotes específicos.
        /// </summary>
        /// <param name="idSucursalOrigen">El identificador del depósito o sucursal que enviará la mercadería.</param>
        /// <param name="idSolicitud">El identificador de la solicitud de pedido original creada por la sucursal de destino.</param>
        /// <exception cref="ReglaNegocioComprasException">Se lanza si la solicitud no existe, ya fue procesada o no tiene renglones válidos.</exception>
        public void GenerarTraspasoDesdeSolicitud(Guid idSucursalOrigen, Guid idSolicitud)
        {
            if (idSucursalOrigen == Guid.Empty || idSolicitud == Guid.Empty)
                throw new ComprasValidationException("Error de contexto: Identificadores inválidos.");

            try
            {
                var sol = _uow.SolicitudPedidoRepository.GetById(idSolicitud);

                if (sol == null || sol.IdEstadoSolicitud != 1)
                    throw new ReglaNegocioComprasException("La solicitud no existe o ya fue procesada.");

                var renglonesAgrupados = sol.SolicitudPedidoDetalle
                    .Where(d => d.IdProducto != null && (d.Cantidad ?? 0) > 0)
                    .GroupBy(d => d.IdProducto)
                    .Select(g => new {
                        IdProducto = g.Key.Value,
                        CantidadBultosPedidos = g.Sum(x => x.Cantidad ?? 0) // Sumamos todos los renglones iguales
                    }).ToList();

                if (!renglonesAgrupados.Any())
                    throw new ReglaNegocioComprasException("La solicitud no contiene renglones válidos.");

                Guid idTraspasoNuevo = Guid.NewGuid();

                var nuevoTraspaso = new OrdenTraspaso
                {
                    IdOrdenTraspaso = idTraspasoNuevo,
                    IdSucursalOrigen = idSucursalOrigen,
                    IdSucursalDestino = sol.IdSucursal,
                    IdSolicitudPedido = sol.IdSolicitudPedido,
                    IdEstado = 1,
                    FechaEmision = DateTime.Now,
                    NroTraspaso = CodigoGenerador.GenerarNumeroOcUnicoNumerico(),
                    OrdenTraspasoDetalle = new List<OrdenTraspasoDetalle>()
                };

                int nroRenglon = 1;
                foreach (var item in renglonesAgrupados)
                {
                    var productoDb = _uow.ProductoRepository.GetById(item.IdProducto);
                    int multiplicador = productoDb?.CantidadPorBulto ?? 1;

                    var traspasoDetalle = new OrdenTraspasoDetalle
                    {
                        IdOrdenTraspasoDetalle = Guid.NewGuid(),
                        IdOrdenTraspaso = idTraspasoNuevo,
                        IdProducto = item.IdProducto,
                        CantidadEnviada = item.CantidadBultosPedidos * multiplicador,
                        CantidadRecibida = 0,
                        Renglon = nroRenglon,
                        IdLoteOrigen = null
                    };

                    nuevoTraspaso.OrdenTraspasoDetalle.Add(traspasoDetalle);
                    nroRenglon++;
                }

                _uow.OrdenTraspasoRepository.AddOrdenTraspaso(nuevoTraspaso);
                sol.IdEstadoSolicitud = 4;
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException($"Error al generar el traspaso.", ex);
            }
        }

        /// <summary>
        /// Recupera el listado de todas las órdenes de traspaso asignadas a un depósito que se encuentran 
        /// pendientes de armado (Estado: Preparación). Retorna los datos mapeados a DTOs listos para la UI.
        /// </summary>
        /// <param name="idSucursalOrigen">El identificador de la sucursal (depósito) que debe preparar los pedidos.</param>
        /// <returns>Una colección de <see cref="OrdenTraspasoDTO"/> con la información de cabecera y detalles para las grillas.</returns>
        public IEnumerable<OrdenTraspasoDTO> ObtenerTraspasosEnPreparacion(Guid idSucursalOrigen)
        {
            if (idSucursalOrigen == Guid.Empty)
                throw new ComprasValidationException("La sucursal de origen es inválida.");

            try
            {
                // 1. Obtenemos los traspasos
                var traspasos = _uow.OrdenTraspasoRepository.GetTraspasosPendientes(idSucursalOrigen);
                var traspasosDto = traspasos.ToDTOList().ToList();

                // 2. Traemos el inventario actual de ESTA sucursal (El Depósito)
                // Usamos AsNoTracking porque solo es lectura para la grilla
                var stockDelDeposito = _uow.StockPorSucursalRepository.GetAll()
                                           .Where(s => s.IdSucursal == idSucursalOrigen)
                                           .ToList();

                // 3. Inyectamos el Stock Actual a cada renglón del detalle
                foreach (var traspaso in traspasosDto)
                {
                    foreach (var detalle in traspaso.Detalles)
                    {
                        var stockProducto = stockDelDeposito.FirstOrDefault(s => s.IdProducto == detalle.IdProducto);

                        // Le asignamos el stock físico actual. Si no existe, es 0.
                        detalle.StockActual = stockProducto?.CantidadTotal ?? 0;
                    }
                }

                return traspasosDto;
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error al consultar el listado de traspasos pendientes.", ex);
            }
        }

        // Recupera el listado de todas las órdenes de traspaso asignadas a un depósito que se encuentran 
        // en tránsito (Estado: 5). Retorna los datos mapeados a DTOs listos para la UI.
        public IEnumerable<OrdenTraspasoDTO> ObtenerTraspasosEnTransito(Guid idSucursalDestino)
        {
            if (idSucursalDestino == Guid.Empty)
                throw new ComprasValidationException("La sucursal de destino es inválida.");

            try
            {
                // 1. Obtenemos los traspasos dirigidos a este local (Estado 5)
                var traspasos = _uow.OrdenTraspasoRepository.GetTraspasosEnviados(idSucursalDestino);
                var traspasosDto = traspasos.ToDTOList().ToList();
                var stockDelLocalDestino = _uow.StockPorSucursalRepository.GetAll()
                                           .Where(s => s.IdSucursal == idSucursalDestino)
                                           .ToList();

                // 3. Inyectamos el Stock Actual a cada renglón del detalle
                foreach (var traspaso in traspasosDto)
                {
                    foreach (var detalle in traspaso.Detalles)
                    {
                        var stockProducto = stockDelLocalDestino.FirstOrDefault(s => s.IdProducto == detalle.IdProducto);

                        // Le asignamos el stock físico actual que tiene el local. Si no existe, es 0.
                        detalle.StockActual = stockProducto?.CantidadTotal ?? 0;
                    }
                }

                return traspasosDto;
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error al consultar el listado de traspasos entrantes.", ex);
            }
        }

        // Ingreso de mercadería en la sucursal de destino
        public void RecibirTraspasoEnDestino(Guid idOrdenTraspaso, Guid idSucursalDestino, string usuarioNombre)
        {
            try
            {
                var orden = _uow.OrdenTraspasoRepository.GetById(idOrdenTraspaso);

                if (orden == null || orden.IdEstado != 5)
                    throw new ReglaNegocioComprasException("El traspaso no existe o no se encuentra en tránsito.");

                if (orden.IdSucursalDestino != idSucursalDestino)
                    throw new ReglaNegocioComprasException("Esta orden no pertenece a su sucursal.");

                foreach (var detalle in orden.OrdenTraspasoDetalle)
                {
                    if (detalle.CantidadEnviada <= 0) continue;

                    // 1. BÚSQUEDA DEL LOTE PADRE PARA HEREDAR TRAZABILIDAD
                    DateTime? fechaVencimientoHeredada = null;
                    decimal costoHeredado = 0;

                    if (detalle.IdLoteOrigen.HasValue)
                    {
                        var loteOrigen = _uow.LoteRepository.GetById(detalle.IdLoteOrigen.Value);
                        if (loteOrigen != null)
                        {
                            fechaVencimientoHeredada = loteOrigen.FechaVencimiento;
                            costoHeredado = loteOrigen.CostoUnitario ??0;
                        }
                    }

                    // 2. Creación del nuevo Lote clonado
                    var nuevoLote = new Lote
                    {
                        IdLote = Guid.NewGuid(),
                        IdProducto = detalle.IdProducto,
                        IdSucursal = idSucursalDestino,
                        CantidadInicial = detalle.CantidadEnviada,
                        CantidadActual = detalle.CantidadEnviada,
                        FechaIngreso = DateTime.Now,
                        NumeroLote = $"TR-{orden.NroTraspaso}-{DateTime.Now:MMdd}",
                        FechaVencimiento = fechaVencimientoHeredada,
                        CostoUnitario = costoHeredado
                    };

                    _uow.LoteRepository.Add(nuevoLote);

                    // 3. Ingresar la mercadería por el Kardex (Usando el NUEVO ENUM)
                    _kardex.RegistrarMovimiento(
                        idSucursalDestino,
                        nuevoLote,
                        TipoMovimientoEnum.IngresoPorTransferencia, 
                        detalle.CantidadEnviada ?? 0,
                        $"Recepción de Traspaso N° {orden.NroTraspaso}",
                        usuarioNombre
                    );

                    detalle.CantidadRecibida = detalle.CantidadEnviada;
                }

                orden.IdEstado = 4; // Finalizado
                orden.FechaRecepcion = DateTime.Now;

                _uow.SaveChanges();
            }
            catch (ReglaNegocioComprasException) { throw; }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error al intentar ingresar la mercadería a la sucursal de destino.", ex);
            }
        }
    }
}
