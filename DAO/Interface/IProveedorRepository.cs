using System;
using System.Collections.Generic;
using System.Linq;
using DAO;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface IProveedorRepository : IGenericRepository<Proveedore>
    {
        Proveedore GetByNombre(string name);
        IEnumerable<Proveedore> GetAllDesHabilitados(); // Para el soft delete
    }
}
