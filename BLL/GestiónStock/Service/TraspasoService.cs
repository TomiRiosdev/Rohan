using BLL.GestiónCompra.Exceptions;
using BLL.GestiónStock.Interface;
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
        public void GenerarTraspasoDesdeSolicitud(Guid idSucursalOrigen, Guid idSolicitud)
        {
            // Validaciones 
            if (idSucursalOrigenDeposito == Guid.Empty || idSolicitud == Guid.Empty)
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
                    IdSucursalOrigen = idSucursalOrigenDeposito, // Quien envía (Depósito)
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

    }
}
