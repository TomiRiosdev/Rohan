using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class OrdenTraspasoDTO
    {
        public Guid IdOrdenTraspaso { get; set; }
        public int NroTraspaso { get; set; }

        public Guid IdSucursalOrigen { get; set; }
        public string SucursalOrigenNombre { get; set; }

        public Guid IdSucursalDestino { get; set; }
        public string SucursalDestinoNombre { get; set; }

        public Guid IdSolicitudPedido { get; set; }

        public int IdEstado { get; set; }
        public string EstadoTexto { get; set; }

        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaRecepcion { get; set; }

        public string Observaciones { get; set; }

        // Lista de detalles ya mapeada
        public List<OrdenTraspasoDetalleDTO> Detalles { get; set; } = new List<OrdenTraspasoDetalleDTO>();
    }
}
