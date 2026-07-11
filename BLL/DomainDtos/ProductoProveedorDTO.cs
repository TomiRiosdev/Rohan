using BLL.Enum;
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
        public decimal PrecioUnitario { get; set; }

        // Propiedades del Producto 
        public int CodigoSku { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;

        // Propiedades del Proveedor 
        public string ProveedorRazonSocial { get; set; } = string.Empty;
        public string ProveedorCuit { get; set; } = string.Empty;

        // Atributo Comercial de Control
        public bool EsProveedorPrincipal { get; set; }

        // Agregados logísticos traídos desde Producto
        public int CantidadPorBulto { get; set; }
        public int? ContenidoPorVenta { get; set; }
        public string UnidadMedidaNombre { get; set; } = string.Empty;

        // Copias la misma propiedad calculada que ya tienes en ProductoDTO
        public int? IdTipoEnvase { get; set; }
        public string TipoEnvaseNombre
        {
            get
            {
                if (IdTipoEnvase == null || IdTipoEnvase <= 0) return "Sin especificar";
                return System.Enum.IsDefined(typeof(TipoEnvaseEnum), IdTipoEnvase)
                    ? ((TipoEnvaseEnum)IdTipoEnvase).ToString()
                    : "Desconocido";
            }
        }

    }
}
