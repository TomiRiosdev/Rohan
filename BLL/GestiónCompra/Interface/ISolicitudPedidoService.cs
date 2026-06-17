using BLL.DomainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónCompra.Interface
{
    public interface ISolicitudPedidoService
    {
        void CrearSolicitud(SolicitudPedidoDTO dto);
        SolicitudPedidoDTO ObtenerPorId(Guid idSolicitud);
        IEnumerable<SolicitudPedidoDTO> ObtenerHistorialPorSucursal(Guid idSucursal);
        List<SolicitudPedidoDetalleDTO> GenerarDetallesSugeridosBajoMinimo(Guid idSucursal);
        void ModificarEstadoSolicitud(Guid idSolicitud, int nuevoEstadoId);

    }
}
