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
        public Guid IdUsuario { get; set; }
        public string? UsuarioNombre { get; set; } // Para auditoría visual en pantalla
        public Guid IdSucursal { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public Guid IdEstadoSolicitud { get; set; }
        public string? EstadoDescripcion { get; set; } // "Pendiente", "Aprobada", etc.

        // Lista de renglones hijos incrustada en el documento principal
        public List<SolicitudPedidoDetalleDTO> Detalles { get; set; } = new List<SolicitudPedidoDetalleDTO>();
    }
}
