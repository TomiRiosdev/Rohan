using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class RecepcionMercaderiaDTO
    {
        public Guid IdOrdenCompraDetalle { get; set; }
        public Guid IdProducto { get; set; }
        public int CantidadRealRecibida { get; set; }
        public int UnidadesPorBulto { get; set; }
        public string Observaciones { get; set; }
    }
}
