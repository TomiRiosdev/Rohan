using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class VinculoSolicitudOcDTO
    {
        public Guid IdVinculoSolicitudOc { get; set; }
        public Guid IdOrdenCompraDetalle { get; set; }
        public Guid IdSolicitudPedidoDetalle { get; set; }
        public int CantidadAsignada { get; set; }
    }
}
