using Implementations.SQLServer.GestionCompra;
using DAO.Implementations.SQLServer.GestionProducto;
using DAO.Implementations.SQLServer.GestionProveedor;
using DAO.Implementations.SQLServer.GestionStock;
using DAO.Implementations.SQLServer.GestionSucursal;
using DAO.Interface;
using DAO.Interface.GestionCompra;
using DAO.Interface.GestionProducto;
using DAO.Interface.GestionProveedor;
using DAO.Interface.GestionStock;
using DAO.Interface.GestionSucursal;
using DAO.Interface.GestionAuditoria;
using DAO.Implementations.SQLServer.GestionAuditoria;

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
        public IStockRepository StockPorSucursalRepository { get; private set; }
        public IProductoProveedorRepository ProductoProveedorRepository { get; private set; }
        public ISolicitudPedidoRepository SolicitudPedidoRepository { get; private set; }
        public IMovimientosStockRepository MovimientosStockRepository { get; private set; }
        public ILoteRepository LoteRepository { get; private set; }
        public ITipoMovimientoRepository TipoMovimientoRepository { get; private set; }
        public IOrdenCompraRepository OrdenCompraRepository { get; private set; }
        public ICompraSolicitudQueryRepository CompraSolicitudQueryRepository { get; private set; }
        public IAuditoriaRepository AuditoriaRepository { get; private set; }


        // Recibe el contexto único administrado por el ServiceProvider
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
            StockPorSucursalRepository = new StockRepository(_dbContext);
            ProductoProveedorRepository = new ProductoProveedorRepository(_dbContext);
            SolicitudPedidoRepository = new SolicitudPedidoRepository(_dbContext);
            MovimientosStockRepository = new MovimientosStockRepository(_dbContext);
            LoteRepository = new LoteRepository(_dbContext);
            TipoMovimientoRepository = new TipoMovimientoRepository(_dbContext);
            OrdenCompraRepository = new OrdenCompraRepository(_dbContext);
            CompraSolicitudQueryRepository = new CompraSolicitudQueryRepository(_dbContext);
            AuditoriaRepository = new AuditoriaRepository(_dbContext);

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
