using Models;

namespace DAO.Interface
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        // Método específico para Producto
        IEnumerable<Producto> GetByNombre(string name);
        IEnumerable<Producto> GetAllDesHabilitados(); // Para el soft delete
        bool ExistsByName(string nombre);
        bool ExistsByNameExceptId(string nombre, Guid idExcluir);
        bool ExistsByCodigoSku(int codigoSku);
        bool ExistsByCodigoSkuExceptId(int codigoSku, Guid idExcluir);

    }
}
