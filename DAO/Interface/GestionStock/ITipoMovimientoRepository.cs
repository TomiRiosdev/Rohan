using Models;

namespace DAO.Interface.GestionStock
{
    public interface ITipoMovimientoRepository
    {
        IEnumerable<TipoMovimiento> GetAll();
    }
}
