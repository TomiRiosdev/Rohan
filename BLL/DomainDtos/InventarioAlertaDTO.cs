using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class InventarioAlertaDTO
    {
        public Guid IdProducto { get; set; }
        public string ProductoNombre { get; set; } = null!;
        public string TipoAlerta { get; set; } = null!;      // "STOCK BAJO", "VENCIDO", "PROXIMO A VENCER"
        public string DetalleMensaje { get; set; } = null!; // Explicación legible del problema
        public int CantidadAfectada { get; set; }
        public string? NumeroLote { get; set; }             // Nulo si es por Stock Bajo general
        public DateTime? FechaVencimiento { get; set; }     // Solo aplica si el lote tiene fecha
    }
}
