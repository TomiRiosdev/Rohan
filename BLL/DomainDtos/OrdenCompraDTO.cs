using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class OrdenCompraDTO
    {
        // Llaves de Identidad y Auditoría
        public Guid IdOrdenCompra { get; set; }
        public Guid? IdProveedor { get; set; }
        public Guid? IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;

        // Información Comercial del Comprobante
        public int NroOrdenCompra { get; set; } 
        public int? NroSolicitudReferencia { get; set; } 
        public DateTime FechaOc { get; set; }

        // Control de Estados
        public int IdEstadoOc { get; set; }
        public string EstadoDescripcion { get; set; } = "Pendiente";

        // Datos del Proveedor para el Bloc de Notas
        public string RazonSocialProveedor { get; set; } = string.Empty;
        public string CuitProveedor { get; set; } = string.Empty;
        public decimal CostoTotal => Detalles.Sum(d => d.SubTotal);

        // Lista En Cascada de los Renglones de la OC
        public List<OrdenCompraDetalleDTO> Detalles { get; set; } = new List<OrdenCompraDetalleDTO>();
    }
}
