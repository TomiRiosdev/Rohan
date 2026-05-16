using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class ProductoProveedorDTO
    {
        public Guid IdProductoProveedor { get; set; }
        public Guid IdProducto { get; set; }
        public string? ProductoNombre { get; set; } // Para mostrar en las grillas de proveedores
        public Guid IdProveedor { get; set; }
        public string? ProveedorNombre { get; set; } // Para mostrar en las grillas de productos
        public bool EsProveedorPrincipal { get; set; }
        public decimal UltimoPrecioCompra { get; set; }
    }
}
