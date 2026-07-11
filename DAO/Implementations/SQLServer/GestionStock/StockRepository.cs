using DAO.Interface.GestionStock;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAO.Implementations.SQLServer.GestionStock
{
    public class StockRepository : IStockRepository
    {
        private readonly RohanContext _dbContext;

        public StockRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "El contexto no puede ser nulo.");
        }

        // 1. BUSCAR UN REGISTRO ESPECÍFICO
        public StockPorSucursal GetByIds(Guid idSucursal, Guid idProducto)
        {
            try
            {
                // Busca la primera fila que coincida con ambos IDs. Si no hay nada, devuelve null.
                return _dbContext.StockPorSucursal
                        .FirstOrDefault(s => s.IdSucursal == idSucursal && s.IdProducto == idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda de stock para Producto {idProducto} en Sucursal {idSucursal}.", ex);
            }
        }

        // 2. LEER TODO EL STOCK DE LA SUCURSAL
        public IEnumerable<StockPorSucursal> GetConsolidadoBySucursal(Guid idSucursal)
        {
            try
            {
                return _dbContext.StockPorSucursal
                    .Include(s => s.IdProductoNavigation)
                        .ThenInclude(p => p.IdCategoriaNavigation)
                    .Include(s => s.IdProductoNavigation)
                        .ThenInclude(p => p.IdUnidadMedidaNavigation)
                    .Where(s => s.IdSucursal == idSucursal &&
                               (s.IdProductoNavigation.Habilitado == true || s.CantidadTotal > 0))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: No se pudo cargar el stock consolidado de la sucursal {idSucursal}.", ex);
            }
        }

        public void Add(StockPorSucursal entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbContext.StockPorSucursal.Add(entity);
        }

        public void AddLote(Lote entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbContext.Lote.Add(entity);
        }

        public void Update(StockPorSucursal entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbContext.StockPorSucursal.Update(entity);
        }

        public IQueryable<StockPorSucursal> GetAll()
        {
            return _dbContext.StockPorSucursal.AsQueryable();
        }
    }
}
