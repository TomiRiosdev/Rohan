using DAO;
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
        public IProductoRepository ProductoRepository { get; private set;}
        public ICategoriaRepository CategoriaRepository { get; private set; }
        public IProveedorRepository ProveedorRepository { get; private set; }
        public ITipoProductoRepository TipoProductoRepository { get; private set; }
        public IUnidadMedidaRepository UnidadMedidaRepository { get; private set; }

        public UnitOfWork() 
        {
            _dbContext = new RohanDbContext();
            ProductoRepository = new ProductoRepository(_dbContext);
            CategoriaRepository = new CategoriaRepository(_dbContext);
            ProveedorRepository = new ProveedorRepository(_dbContext);
            TipoProductoRepository = new TipoProductoRepository(_dbContext);
            UnidadMedidaRepository = new UnidadMedidaRepository(_dbContext);
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
