using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class MovimientoStockDTO
    {
        public Guid IdMovimiento { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdLote { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public Guid IdProducto { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public int? CodigoSku { get; set; }

        // Auditoría e Historial (Lo que pide la UI)
        public int Cantidad { get; set; } // Unidades físicas impactadas
        public DateTime FechaMovimiento { get; set; }
        public string TipoMovimientoTexto { get; set; } = string.Empty; // Ej: "Ingreso Manual", "Egreso por Merma", "Ingreso por OC"
        public string Observaciones { get; set; } = string.Empty;
        public string UsuarioNombre { get; set; } = "Sistema"; // Quién lo hizo
        public string? DocumentoReferencia { get; set; } // Nro de Orden de Compra, Factura o Remito (Ej: "OC-4502")
        public string HoraMovimiento => FechaMovimiento.ToString("HH:mm:ss");
        public string FechaMovimientoCorta => FechaMovimiento.ToString("dd/MM/yyyy");
    }
}
