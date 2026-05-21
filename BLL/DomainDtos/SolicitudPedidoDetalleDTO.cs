using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class SolicitudPedidoDetalleDTO
    {
        public Guid IdSolicitudPedido { get; set; }
        public Guid IdProducto { get; set; }

        // Propiedad aplanada para mostrar directo el nombre del artículo en las grillas de la UI
        public string? ProductoNombre { get; set; }

        public int Renglon { get; set; }
        public int Cantidad { get; set; }
    }
}
