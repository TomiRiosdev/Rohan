using BLL.DomainDtos;
using BLL.GestiónCompra.Interface;
using System;


namespace BLL.GestiónCompra.Facade
{
   public class SolicitudPedidoFacade
   {
       private readonly ISolicitudPedidoService _service;

       public SolicitudPedidoFacade
       (
           ISolicitudPedidoService service
       )
       {
           _service = service ?? throw new ArgumentNullException(nameof(service));
       }

        public void CrearSolicitud(SolicitudPedidoDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            _service.CrearSolicitud(dto);
        }
        public  SolicitudPedidoDTO ObtenerPorId(Guid idSolicitud)
        {
            if (idSolicitud == Guid.Empty) throw new ArgumentNullException(nameof(idSolicitud));
            return _service.ObtenerPorId(idSolicitud);
        }
        public IEnumerable<SolicitudPedidoDTO> ObtenerHistorialPorSucursal(Guid idSucursal)
        {
            if (idSucursal == Guid.Empty) throw new ArgumentNullException(nameof(idSucursal));
            return _service.ObtenerHistorialPorSucursal(idSucursal);
        }
        public List<SolicitudPedidoDetalleDTO> GenerarDetallesSugeridosBajoMinimo(Guid idSucursal)
        {
            if (idSucursal == Guid.Empty) throw new ArgumentNullException(nameof(idSucursal));
            return _service.GenerarDetallesSugeridosBajoMinimo(idSucursal);
        }

        public void CambiarEstado(Guid idSolicitud, int nuevoEstadoId)
        {
            try
            {
                _service.ModificarEstadoSolicitud(idSolicitud, nuevoEstadoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo cambiar el estado de la Solicitud de Pedido ID: {idSolicitud}: {ex.Message}", ex);
            }
        }
    }
}
