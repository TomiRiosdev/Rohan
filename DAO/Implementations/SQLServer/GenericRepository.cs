using DAO.EntityFramework;
using DAO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementations.SQLServer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly RohanDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository()
        {
            _context = new RohanDbContext();
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            // En EF Core, Update marca el estado de la entidad como Modified
            _dbSet.Update(entity);
        }

        public void Remove(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            // Devuelve todos los elementos
            return _dbSet.ToList();
        }

    }
}
