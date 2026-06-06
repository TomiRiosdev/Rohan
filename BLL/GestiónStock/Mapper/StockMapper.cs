using BLL.DomainDtos;
using BLL.Enum;
using Models;


namespace BLL.GestiónStock.Mapper
{
    public static class StockMapper
    {
        public static StockPorSucursalDTO ToDTO(this StockPorSucursal stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock), "La entidad StockPorSucursal no puede ser null.");

            return new StockPorSucursalDTO
            {
                IdStockPorSucursal = stock.IdStockPorSucursal,
                IdSucursal = stock.IdSucursal,
                IdProducto = stock.IdProducto,
                CantidadTotal = stock.CantidadTotal ?? 0,
                StockMinimo = stock.StockMinimo ?? 0,
                StockMaximo = stock.StockMaximo ?? 0,

                ProductoNombre = stock.IdProductoNavigation?.Nombre ?? "Producto no identificado",
                CodigoSku = (int?)stock.IdProductoNavigation?.CodigoSku,
                IdCategoria = stock.IdProductoNavigation?.IdCategoria ?? Guid.Empty,
                IdUnidadMedida = stock.IdProductoNavigation?.IdUnidadMedida ?? Guid.Empty,

                CategoriaNombre = stock.IdProductoNavigation?.IdCategoriaNavigation?.Descripcion ?? "Sin categoría",
                UnidadMedidaNombre = stock.IdProductoNavigation?.IdUnidadMedidaNavigation?.Descripcion ?? "Sin unidad",

                // 🚀 CRUCIAL: Mapeamos los factores logísticos del producto hacia el DTO de stock
                CantidadPorBulto = stock.IdProductoNavigation?.CantidadPorBulto ?? 1,
                ContenidoPorVenta = stock.IdProductoNavigation?.ContenidoPorVenta ?? 1,
                IdTipoEnvase = stock.IdProductoNavigation?.IdTipoEnvase ?? 0,
                TipoEnvaseNombre = stock.IdProductoNavigation?.IdTipoEnvase <= 0 ? "Sin especificar" :
                (System.Enum.IsDefined(typeof(TipoEnvaseEnum), stock.IdProductoNavigation.IdTipoEnvase)
                ? ((TipoEnvaseEnum)stock.IdProductoNavigation.IdTipoEnvase).ToString()
                : "Desconocido")

            };
        }

        public static StockPorSucursal ToEntity(this StockPorSucursalDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO StockPorSucursal no puede ser null.");

            return new StockPorSucursal
            {
                IdStockPorSucursal = dto.IdStockPorSucursal,
                IdSucursal = dto.IdSucursal,
                IdProducto = dto.IdProducto,
                CantidadTotal = dto.CantidadTotal,
                StockMinimo = dto.StockMinimo,
                StockMaximo = dto.StockMaximo
            };
        }

        public static IEnumerable<StockPorSucursalDTO> ToDTOList(this IEnumerable<StockPorSucursal> stockList)
        {
            return stockList?.Select(ToDTO) ?? Enumerable.Empty<StockPorSucursalDTO>();
        }

        public static void UpdateEntity(this StockPorSucursal entity, StockPorSucursalDTO dto)
        {
            if (entity == null || dto == null) return;

            // El IdSucursal y el IdProducto NO se modifican porque actúan como identificadores de contexto
            entity.CantidadTotal = dto.CantidadTotal;
            entity.StockMinimo = dto.StockMinimo;
            entity.StockMaximo = dto.StockMaximo;
        }
    }
}
