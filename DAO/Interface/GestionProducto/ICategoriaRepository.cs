using Models;

namespace DAO.Interface.GestionProducto
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Categoria GetByNombre(string name);
        bool ExistsByName(string nombre);
    }
}
