using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface IUnidadMedidaRepository : IGenericRepository<UnidadMedida>
    {
        UnidadMedida GetByNombre(string name);
        IEnumerable<UnidadMedida> GetAllDesHabilitados(); // Para el soft delete
    }
}
