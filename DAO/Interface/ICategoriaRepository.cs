using Models;

namespace DAO.Interface
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Categoria GetByNombre(string name);
        bool ExistsByName(string nombre);
    }
}
