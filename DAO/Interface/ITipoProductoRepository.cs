using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface ITipoProductoRepository : IGenericRepository<TipoProducto>
    {
        TipoProducto GetByNombre(string name);
        IEnumerable<TipoProducto> GetAllDesHabilitados(); // Para el soft delete
    }
}
