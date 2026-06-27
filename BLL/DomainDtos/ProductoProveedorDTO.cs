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
        // IDs de Clave Primaria Compuesta
        public Guid IdProducto { get; set; }
        public Guid IdProveedor { get; set; }

        // Propiedades del Producto 
        public int CodigoSku { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;

        // Propiedades del Proveedor 
        public string ProveedorRazonSocial { get; set; } = string.Empty;
        public string ProveedorCuit { get; set; } = string.Empty;

        // Atributo Comercial de Control
        public bool EsProveedorPrincipal { get; set; }
    }
}
