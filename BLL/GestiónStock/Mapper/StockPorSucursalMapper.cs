using BLL.DomainDtos;
using Models;


namespace BLL.GestiónStock.Mapper
{
    public static class StockPorSucursalMapper
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

                // Mapeo seguro utilizando el operador condicional nulo para evitar excepciones si la navegación no se cargó
                ProductoNombre = stock.IdProductoNavigation?.Nombre ?? "Producto no identificado",

                CantidadTotal = stock.CantidadTotal ?? 0,
                StockMinimo = stock.StockMinimo ?? 0,
                StockMaximo = stock.StockMaximo ?? 0
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
