using Models;

namespace DAO.Interface
{
    public interface IUnidadMedidaRepository : IGenericRepository<UnidadMedida>
    {
        UnidadMedida GetByNombre(string name);
        bool ExistsByName(string nombre);
    }
}
