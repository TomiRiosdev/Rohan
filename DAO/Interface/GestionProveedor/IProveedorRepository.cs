using Models;

namespace DAO.Interface.GestionProveedor
{
    public interface IProveedorRepository : IGenericRepository<Proveedor>
    {
        IEnumerable<Proveedor>GetByNombre(string name);
        IEnumerable<Proveedor> GetAllDesHabilitados(); // Para el soft delete
        bool ExistsByName(string nombre);
    }
}
