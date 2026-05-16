using BLL.DomainDtos;

namespace BLL.GestiónStock.Interface
{
    public interface IStockPorSucursalService
    {
        void RegistrarStockManual(StockPorSucursalDTO stockDto);
        IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursalActual();
    }
}
