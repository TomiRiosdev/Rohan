using Models;

namespace DAO.Interface.GestionProducto
{
    public interface IUnidadMedidaRepository : IGenericRepository<UnidadMedida>
    {
        UnidadMedida GetByNombre(string name);
        bool ExistsByName(string nombre);
    }
}
