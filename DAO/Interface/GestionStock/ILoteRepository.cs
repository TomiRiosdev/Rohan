using Models;

namespace DAO.Interface.GestionStock
{
    public interface ILoteRepository
    {
        Lote GetById(Guid idLote);
        void Add(Lote lote);
        IEnumerable<Lote> GetLotesActivosPorSucursal(Guid idSucursal);
        void Update(Lote lote);
        void Delete(Guid idLote);
    }
}
