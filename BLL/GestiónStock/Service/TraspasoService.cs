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

        public TraspasoService
        (
            IUnitOfWork unitOfWork
        )
        {
            _uow = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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
                // 1. Obtener la Orden (Trackeada por EF para poder modificarla)
                var orden = _uow.OrdenTraspasoRepository.GetById(idOrdenTraspaso);

                if (orden == null || orden.IdEstado != 5) // 5 = Preparacion
                    throw new ReglaNegocioComprasException("El traspaso no existe o no está en estado de preparación.");

                Guid idSucursalOrigen = orden.IdSucursalOrigen.Value;
                Guid idSucursalDestino = orden.IdSucursalDestino.Value;

                // 2. Procesar cada detalle confirmado por el usuario en la grilla
                foreach (var detConfirmado in detallesConfirmados)
                {
                    if (detConfirmado.CantidadEnviada <= 0) continue; // Si mandó 0, lo salteamos

                    // 3. Buscar lotes usando tu método existente y aplicando el filtro FIFO
                    var lotesDisponibles = _uow.LoteRepository.GetLotesActivosPorSucursal(idSucursalOrigen)
                        .Where(l => l.IdProducto == detConfirmado.IdProducto)
                        .OrderBy(l => l.FechaVencimiento ?? DateTime.MaxValue) // FIFO: Primero los que vencen antes
                        .ThenBy(l => l.FechaIngreso)
                        .ToList();

                    int cantidadARestar = detConfirmado.CantidadEnviada;

                    // 3. Algoritmo FIFO de Descuento
                    foreach (var lote in lotesDisponibles)
                    {
                        if (cantidadARestar == 0) break; // Ya restamos todo lo necesario

                        int cantidadATomarDelLote = Math.Min(cantidadARestar, lote.CantidadActual.Value);

                        // A) Descontar del Lote
                        lote.CantidadActual -= cantidadATomarDelLote;

                        // B) Generar el Movimiento de Stock (EGRESO del Depósito)
                        var movimientoEgreso = new MovimientosStock
                        {
                            IdMovimiento = Guid.NewGuid(),
                            IdSucursal = idSucursalOrigen,
                            IdTipoMovimiento = (int)TipoMovimientoEnum.Transferencia,
                            IdLote = lote.IdLote,
                            IdSucursalOrigen = idSucursalOrigen,
                            IdSucursalDestino = idSucursalDestino,
                            Cantidad = -cantidadATomarDelLote, // Negativo porque sale
                            FechaMovimiento = DateTime.Now,
                            UsuarioNombre = usuarioNombre,
                            Observaciones = $"Envío Traspaso N° {orden.NroTraspaso}"
                        };
                        _uow.MovimientosStockRepository.Add(movimientoEgreso); // Asegúrate de tener este repo en IUnitOfWork

                        // C) Registrar la trazabilidad del Lote en el Detalle del Traspaso
                        // NOTA ARQUITECTÓNICA: Si un producto se sacó de 2 lotes distintos, el detalle original
                        // se "parte" en dos. Para simplificar, actualizaremos el registro original si es 1 lote,
                        // o crearemos uno nuevo si necesitamos sacar de múltiples lotes.

                        var detalleDb = orden.OrdenTraspasoDetalle.FirstOrDefault(d => d.IdOrdenTraspasoDetalle == detConfirmado.IdOrdenTraspasoDetalle);

                        if (detalleDb.IdLoteOrigen == null)
                        {
                            // Es el primer lote del que sacamos, actualizamos el registro existente
                            detalleDb.IdLoteOrigen = lote.IdLote;
                            detalleDb.CantidadEnviada = cantidadATomarDelLote;
                        }
                        else
                        {
                            // Ya usamos un lote para este detalle, necesitamos clonar la fila para el segundo lote
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

                        // Si después de recorrer todos los lotes aún falta cantidad, es que el stock real era menor
                        // al que el operario intentó enviar (inconsistencia).
                        if (cantidadARestar > 0)
                        {
                            var prod = _uow.ProductoRepository.GetById(detConfirmado.IdProducto);
                            throw new ReglaNegocioComprasException($"No hay stock físico suficiente para cubrir el envío de {prod.Nombre}. Faltan {cantidadARestar} unidades.");
                        }
                    }

                    // 4. Cambiar el estado de la Orden a "En Tránsito"
                    orden.IdEstado = 6; // 6 = Transito
                    orden.FechaEmision = DateTime.Now;
                    // orden.IdUsuarioEmisior = SessionManager.Current.UsuarioLogueado.IdUsuario;

                    // 5. Commit de toda la transacción (Lotes descontados + Movimientos + Estado Orden)
                    _uow.SaveChanges();
                }
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
            // Validaciones 
            if (idSucursalOrigen == Guid.Empty || idSolicitud == Guid.Empty)
                throw new ComprasValidationException("Error de contexto: Identificadores inválidos.");

            try
            {
                // 1. Buscamos la solicitud original
                var sol = _uow.SolicitudPedidoRepository.GetById(idSolicitud);

                if (sol == null)
                    throw new ReglaNegocioComprasException("La solicitud no existe.");

                if (sol.IdEstadoSolicitud != 1) // 1 = Pendiente
                    throw new ReglaNegocioComprasException("Esta solicitud ya fue procesada.");

                // 2. Extraemos renglones válidos
                var renglonesValidos = sol.SolicitudPedidoDetalle
                    .Where(d => d.IdProducto != null && (d.Cantidad ?? 0) > 0)
                    .ToList();

                if (!renglonesValidos.Any())
                    throw new ReglaNegocioComprasException("La solicitud no contiene renglones válidos.");

                // 3. Generamos la Cabecera del Traspaso
                Guid idTraspasoNuevo = Guid.NewGuid();

                var nuevoTraspaso = new OrdenTraspaso
                {
                    IdOrdenTraspaso = idTraspasoNuevo,
                    IdSucursalOrigen = idSucursalOrigen, // Quien envía (Depósito)
                    IdSucursalDestino = sol.IdSucursal,          // Quien pidió (Local)
                    IdSolicitudPedido = sol.IdSolicitudPedido,
                    IdEstado = 5,                                // 5 = Preparacion
                    FechaEmision = DateTime.Now,
                    NroTraspaso = CodigoGenerador.GenerarNumeroOcUnicoNumerico(),

                    // Navegaciones en null para evitar que EF intente re-crearlos
                    IdEstadoSolicitudNavigation = null,
                    IdSolicitudPedidoNavigation = null,
                    IdSucursalDestinoNavigation = null,
                    IdSucursalOrigenNavigation = null,
                    OrdenTraspasoDetalle = new List<OrdenTraspasoDetalle>()
                };

                // 4. Generamos los Detalles
                int nroRenglon = 1;
                foreach (var item in renglonesValidos)
                {
                    var traspasoDetalle = new OrdenTraspasoDetalle
                    {
                        IdOrdenTraspasoDetalle = Guid.NewGuid(),
                        IdOrdenTraspaso = idTraspasoNuevo,
                        IdProducto = item.IdProducto,
                        CantidadEnviada = item.Cantidad, // Inicialmente seteamos lo mismo que pidieron
                        CantidadRecibida = 0,
                        Renglon = nroRenglon,

                        // CRÍTICO: Dejamos IdLoteOrigen en NULL por ahora.
                        // Se completará cuando el operario confirme el envío físico y apliquemos FIFO.
                        IdLoteOrigen = null,

                        IdProductoNavigation = null,
                        IdLoteOrigenNavigation = null
                    };

                    nuevoTraspaso.OrdenTraspasoDetalle.Add(traspasoDetalle);
                    nroRenglon++;
                }

                // 5. Guardamos el Traspaso
                _uow.OrdenTraspasoRepository.AddOrdenTraspaso(nuevoTraspaso);

                // 6. Cambiamos el estado de la Solicitud Madre
                sol.IdEstadoSolicitud = 5; // 5 = Preparacion (o el estado que prefieras para indicar que ya se tomó)

                // 7. Commit a la base de datos
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException($"Error al generar el traspaso para la solicitud {idSolicitud}.", ex);
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
                var traspasos = _uow.OrdenTraspasoRepository.GetTraspasosPendientes(idSucursalOrigen);
                return traspasos.ToDTOList(); // Usa el mapper que armamos antes
            }
            catch (Exception ex)
            {
                throw new ComprasDomainException("Error al consultar el listado de traspasos pendientes.", ex);
            }
        }
    }
}
