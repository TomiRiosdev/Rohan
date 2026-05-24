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

        public int CantidadTotal { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }
        public bool Habilitado { get; set; }

        // Datos requeridos para la creación del Lote asociado en cargas manuales
        public decimal CostoUnitario { get; set; }
        public string? NumeroLote { get; set; }

        // ==== PROPIEDADES ENRIQUECIDAS PARA FILTRADO EN UI ===

        public string? ProductoNombre { get; set; }
        public int? CodigoSku { get; set; }

        //para la VISTA(DataGridView)
        public string CategoriaNombre { get; set; }
        public string UnidadMedidaNombre { get; set; }

        // Para la LÓGICA (Validar y Guardar)
        public Guid IdCategoria { get; set; }
        public Guid IdUnidadMedida { get; set; }
    }
}
