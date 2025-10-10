using DAO.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        // Método específico para Producto
        IEnumerable<Producto> GetByNombre(string name);
        IEnumerable<Producto> GetAllHabilitados(); // Para el soft delete
    }
}
