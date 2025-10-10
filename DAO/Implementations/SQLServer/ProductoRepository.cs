using DAO.DomainModel;
using DAO.EntityFramework;
using DAO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementations.SQLServer
{
    public class ProductoRepository :GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(RohanDbContext dbContext) { }
        public IEnumerable<Producto> GetByNombre(string name)
        {
            // Implementación del método específico: búsqueda por nombre
            return _dbSet.Where(p => p.Nombre.Contains(name)).ToList();
        }

        public IEnumerable<Producto> GetAllHabilitados()
        {
            // Implementación para el 'soft delete': solo devuelve los habilitados
            // Asumiendo que tu modelo 'Producto' tiene una propiedad 'Habilitado'
            return _dbSet.Where(p => p.Habilitado).ToList();
        }
    }
}
