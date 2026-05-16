using Models;


namespace DAO.Interface
{
    public interface IStockPorSucursalRepository
    {
        StockPorSucursal GetByIds(Guid idSucursal, Guid idProducto);
        IEnumerable<StockPorSucursal> GetConsolidadoBySucursal(Guid idSucursal);
        void Add(StockPorSucursal entity);
        void AddLote(Lote entity);
    }
}
