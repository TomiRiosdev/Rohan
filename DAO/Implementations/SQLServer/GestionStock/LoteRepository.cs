using DAO.Interface.GestionStock;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DAO.Implementations.SQLServer.GestionStock
{
    public class LoteRepository : ILoteRepository
    {
        private readonly RohanContext _dbContext;

        public LoteRepository
        (
            RohanContext dbContext
        )
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(Lote entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbContext.Lote.Add(entity);
        }

        public Lote GetById(Guid idLote)
        {
            return _dbContext.Lote
                .Include(l => l.IdProductoNavigation)
                .FirstOrDefault(l => l.IdLote == idLote);
        }

        public IEnumerable<Lote> GetLotesActivosPorSucursal(Guid idSucursal)
        {
            try
            {
                return _dbContext.Lote
                    .Include(l => l.IdProductoNavigation) 
                    .Where(l => l.IdSucursal == idSucursal && l.CantidadActual > 0)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: No se pudieron recuperar los lotes activos de la sucursal {idSucursal}.", ex);
            }
        }
    }
}
