using DAO.Interface;

namespace DAO.Implementations.SQLServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RohanContext _dbContext;
        public IProductoRepository ProductoRepository { get; private set;}
        public ICategoriaRepository CategoriaRepository { get; private set; }
        public IProveedorRepository ProveedorRepository { get; private set; }
        public IUnidadMedidaRepository UnidadMedidaRepository { get; private set; }
        public ISucursalRepository SucursalRepository { get; private set; }
        public ITipoSucursalRepository TipoSucursalRepository { get; private set; }

        public UnitOfWork() 
        {
            _dbContext = new RohanContext();
            ProductoRepository = new ProductoRepository(_dbContext);
            CategoriaRepository = new CategoriaRepository(_dbContext);
            ProveedorRepository = new ProveedorRepository(_dbContext);
            UnidadMedidaRepository = new UnidadMedidaRepository(_dbContext);
            SucursalRepository = new SucursalRepository(_dbContext);
            TipoSucursalRepository = new TipoSucursalRepository(_dbContext);
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
