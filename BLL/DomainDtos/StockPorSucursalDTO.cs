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

        public int IdTipoMovimiento { get; set; }
        public string Observaciones { get; set; } = string.Empty;

        // ==== PROPIEDADES CALCULADAS INTELIGENTES PARA LA GRILLA ====
        public decimal ContenidoPorVenta { get; set; } // Lo que configuraste en el ABM (ej: 900 ml o 1 kg)

        // Nos dice cuántos bultos/envases enteros e impecables hay en estantería
        public int EnvasesEnteros
        {
            get
            {
                if (ContenidoPorVenta <= 0) return 0;
                return (int)(CantidadTotal / ContenidoPorVenta);
            }
        }

        // Nos dice el remanente suelto (lo que quedó en un envase abierto en producción)
        public int CantidadSuelta
        {
            get
            {
                if (ContenidoPorVenta <= 0) return 0;
                return (int)(CantidadTotal % ContenidoPorVenta);
            }
        }

        // Una hermosa cadena de texto formateada para que el panadero entienda al toque
        public string StockDetalladoVisual
        {
            get
            {
                if (ContenidoPorVenta <= 1)
                {
                    // Si el producto se vende de a 1 kg o 1 unidad, no hace falta desglosar
                    return $"{CantidadTotal} {UnidadMedidaNombre}";
                }

                return $"{EnvasesEnteros} u. cerradas (+ {CantidadSuelta} {UnidadMedidaNombre} sueltos)";
            }
        }


    }
}
