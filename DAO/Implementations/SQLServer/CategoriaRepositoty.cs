using DAO.DomainModel;
using DAO.EntityFramework;
using DAO.Interface;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementations.SQLServer
{
    public class CategoriaRepository : GenericRepository<CategoriaProducto>, ICategoriaRepository
    {
        // El constructor simplemente llama al constructor base
        public CategoriaRepository(RohanDbContext dbContext) { }

        // Implementación del método específico: buscar por nombre exacto
        public CategoriaProducto GetByNombre(string name)
        {
            // Usamos _dbSet, que heredamos del GenericRepository
            return _dbSet.FirstOrDefault(c => c.Nombre == name);
        }

        // NOTA: Los métodos Add, Update, Remove, GetById, GetAll
        // ya están disponibles automáticamente por heredar de GenericRepository<CategoriaProducto>.
    }
}
