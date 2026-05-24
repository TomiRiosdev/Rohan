using Models;


namespace DAO.Interface.GestionStock
{
    public interface IStockRepository
    {
        StockPorSucursal GetByIds(Guid idSucursal, Guid idProducto);
        IEnumerable<StockPorSucursal> GetConsolidadoBySucursal(Guid idSucursal);
        void Add(StockPorSucursal entity);
        void AddLote(Lote entity);
    }
}
