using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class SolicitudPedidoDTO
    {
        public Guid IdSolicitudPedido { get; set; }
        public int NroSolicitud { get; set; }
        public Guid? IdUsuario { get; set; }
        public string? UsuarioNombre { get; set; }
        public Guid? IdSucursal { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public string? EstadoNombre { get; set; }
        public List<SolicitudPedidoDetalleDTO> Detalles { get; set; } = new List<SolicitudPedidoDetalleDTO>();
    }
}
