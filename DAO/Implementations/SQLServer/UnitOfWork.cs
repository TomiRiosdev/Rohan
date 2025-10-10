using DAO.EntityFramework;
using DAO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementations.SQLServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RohanDbContext _dbContext;
        private IProductoRepository _productoRepository;
        private ICategoriaRepository _categoriaRepository;
        // El constructor recibe el DbContext a través de Inyección de Dependencias
        public UnitOfWork()
        {
            _dbContext = new RohanDbContext();
        }

        // Implementación de la propiedad de ProductoRepository
        public IProductoRepository ProductoRepository
        {
            get
            {
                // Solo crea la instancia si es nula (Lazy Loading local)
                if (_productoRepository == null)
                {
                    _productoRepository = new ProductoRepository(_dbContext);
                }
                return _productoRepository;
            }
        }

        // Implementación de la propiedad de CategoriaRepository
        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                if (_categoriaRepository == null)
                {
                    _categoriaRepository = new CategoriaRepository(_dbContext);
                }
                return _categoriaRepository;
            }
        }

        public void SaveChanges()
        {
            // Centraliza la llamada a SaveChanges()
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            // Libera la memoria del DbContext cuando ya no se usa
            _dbContext.Dispose();
        }
    }
}
