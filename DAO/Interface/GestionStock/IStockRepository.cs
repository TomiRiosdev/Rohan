using Models;

namespace DAO.Interface.GestionStock
{
    public interface IStockRepository
    {
        void Add(StockPorSucursal stockPorSucursal);
        void Update(StockPorSucursal stockPorSucursal);
        StockPorSucursal GetByIds(Guid idSucursal, Guid idProducto);
        IEnumerable<StockPorSucursal> GetConsolidadoBySucursal(Guid idSucursal);
        IQueryable<StockPorSucursal> GetAll();
    }
}
