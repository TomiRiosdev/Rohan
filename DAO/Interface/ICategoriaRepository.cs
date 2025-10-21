using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface ICategoriaRepository : IGenericRepository<CategoriaProducto>
    {
        // Método específico para la lógica de negocio (ej. buscar para validación de unicidad)
        CategoriaProducto GetByNombre(string name);
        IEnumerable<CategoriaProducto> GetAllDesHabilitados(); // Para el soft delete
    }
}
