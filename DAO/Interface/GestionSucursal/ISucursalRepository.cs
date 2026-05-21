using Models;

namespace DAO.Interface.GestionSucursal
{
    public interface ISucursalRepository : IGenericRepository<Sucursal>
    {
        IEnumerable<Sucursal> GetByNombre(string name);
        IEnumerable<Sucursal> GetAllDesHabilitados(); // Para el soft delete
        bool ExistsByName(string nombre);
        bool ExistsByNameExceptId(string nombre, Guid idExcluir);
       
    }
}
