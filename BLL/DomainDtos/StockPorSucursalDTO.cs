using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class StockPorSucursalDTO
    {
        public Guid IdStockPorSucursal { get; set; }
        public Guid IdSucursal { get; set; }
        public Guid IdProducto { get; set; }

        // Propiedad aplanada para mostrar en el DataGridView sin exponer la entidad Producto
        public string? ProductoNombre { get; set; }

        public int CantidadTotal { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }
        public bool Habilitado { get; set; }

        // Datos requeridos para la creación del Lote asociado en cargas manuales
        public decimal CostoUnitario { get; set; }
        public string? NumeroLote { get; set; }
    }
}
