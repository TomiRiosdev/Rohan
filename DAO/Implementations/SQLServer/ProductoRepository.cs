using DAO;
using DAO;
using DAO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAO.Implementations.SQLServer
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly RohanDbContext _dbContext;
        public ProductoRepository(RohanDbContext dbContext)
        {
              _dbContext = dbContext;
        }

        public Guid Add(Producto entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Producto entity)
        {
            throw new NotImplementedException();
        }

        public Producto GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetAllDesHabilitados()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetByNombre(string name)
        {
            throw new NotImplementedException();
        }

       
    }
}
