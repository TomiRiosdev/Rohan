using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class SolicitudPedidoDetalleDTO
    {
        public Guid IdProducto { get; set; }
        public int CodigoSku { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public int CantidadBultosSolicitada { get; set; }
        public string PresentacionTipo { get; set; } = "Caja"; 
        public int UnidadesPorBulto { get; set; }
        public int Renglon { get; set; }

        // Propiedad calculada para la auditoría interna
        public int TotalUnidadesSueltas => CantidadBultosSolicitada * UnidadesPorBulto;
       
 
    }
}
