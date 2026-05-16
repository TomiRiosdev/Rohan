using DAO.Interface;

namespace DAO.Implementations.SQLServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RohanContext _dbContext;

        // Las interfaces expuestas
        public IProductoRepository ProductoRepository { get; private set; }
        public ICategoriaRepository CategoriaRepository { get; private set; }
        public IProveedorRepository ProveedorRepository { get; private set; }
        public IUnidadMedidaRepository UnidadMedidaRepository { get; private set; }
        public ISucursalRepository SucursalRepository { get; private set; }
        public ITipoSucursalRepository TipoSucursalRepository { get; private set; }
        public IStockPorSucursalRepository StockPorSucursalRepository { get; private set; }
        public IProductoProveedorRepository ProductoProveedorRepository { get; private set; }


        // SOLUCIÓN: Recibe el contexto único administrado por el ServiceProvider
        public UnitOfWork(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            // Se comparte exactamente la misma instancia del contexto a todos los repositorios
            ProductoRepository = new ProductoRepository(_dbContext);
            CategoriaRepository = new CategoriaRepository(_dbContext);
            ProveedorRepository = new ProveedorRepository(_dbContext);
            UnidadMedidaRepository = new UnidadMedidaRepository(_dbContext);
            SucursalRepository = new SucursalRepository(_dbContext);
            TipoSucursalRepository = new TipoSucursalRepository(_dbContext);
            StockPorSucursalRepository = new StockPorSucursalRepository(_dbContext);
            ProductoProveedorRepository = new ProductoProveedorRepository(_dbContext);

        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error general en la Unidad de Trabajo al confirmar los cambios en la base de datos.", ex);
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
