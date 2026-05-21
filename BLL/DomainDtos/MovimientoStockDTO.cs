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
        public string? NumeroLote { get; set; }      // Traído por JOIN para auditoría visual
        public Guid IdTipoMovimiento { get; set; }
        public string? TipoMovimientoTexto { get; set; } // "Ingreso Manual", "Venta", "Merma"
        public int Cantidad { get; set; }             // Positivos para entradas, negativos para salidas
        public DateTime FechaMovimiento { get; set; }
        public string? Observaciones { get; set; }

        // Propiedades de contexto adicionales para las grillas de historial
        public Guid IdProducto { get; set; }
        public string? ProductoNombre { get; set; }
    }
}
