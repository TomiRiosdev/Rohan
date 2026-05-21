using DAO.Interface.GestionStock;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DAO.Implementations.SQLServer.GestionStock
{
    public class MovimientosStockRepository : IMovimientosStockRepository
    {
        private readonly RohanContext _dbContext;

        public MovimientosStockRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(MovimientosStock entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbContext.MovimientosStock.Add(entity);
        }

        public IEnumerable<MovimientosStock> GetHistorial(Guid idSucursal, DateTime desde, DateTime hasta)
        {
            try
            {
                // Ajustamos los rangos horarios de las fechas para no omitir registros por minutos
                DateTime fechaDesde = desde.Date;
                DateTime fechaHasta = hasta.Date.AddDays(1).AddTicks(-1);

                return _dbContext.MovimientosStock
                    .Include(m => m.IdTipoMovimientoNavigation) // Trae la descripción del tipo de movimiento
                    .Include(m => m.IdLoteNavigation)           // Trae el objeto Lote completo
                        .ThenInclude(l => l.IdProductoNavigation) // Del lote, anida el objeto Producto de forma segura
                    .Where(m => m.IdSucursal == idSucursal
                             && m.FechaMovimiento >= fechaDesde
                             && m.FechaMovimiento <= fechaHasta)
                    .OrderByDescending(m => m.FechaMovimiento)  // Historial ordenado de lo más reciente a lo antiguo
                    .AsNoTracking()                             // Optimiza rendimiento para grillas de lectura
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: No se pudo recuperar el historial de Kardex para la sucursal {idSucursal}.", ex);
            }
        }

        public IEnumerable<MovimientosStock> GetByLote(Guid idLote)
        {
            try
            {
                if (idLote == Guid.Empty) return Enumerable.Empty<MovimientosStock>();

                return _dbContext.MovimientosStock
                    .Include(m => m.IdTipoMovimientoNavigation)
                    .Where(m => m.IdLote == idLote)
                    .OrderBy(m => m.FechaMovimiento)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: No se pudieron obtener los movimientos asociados al lote {idLote}.", ex);
            }
        }
    }
}
