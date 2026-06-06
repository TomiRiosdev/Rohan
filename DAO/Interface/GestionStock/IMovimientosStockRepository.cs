using Models;

namespace DAO.Interface.GestionStock
{
    public interface IMovimientosStockRepository
    {
        void Add(MovimientosStock movimiento);
        IEnumerable<MovimientosStock> GetHistorial(Guid idSucursal, DateTime desde, DateTime hasta);
        IEnumerable<MovimientosStock> GetByLote(Guid idLote);
    }
}
