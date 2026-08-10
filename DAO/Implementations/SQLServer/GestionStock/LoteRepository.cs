using DAO.Interface.GestionStock;
using Models;
using System.Data.Entity;


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
      
        public void Update(Lote lote)
        {
            try
            {
                if (lote == null)
                    throw new ArgumentNullException(nameof(lote), "No se puede actualizar un objeto de lote nulo en la base de datos.");

                // 1. Buscamos la entidad original guardada en el contexto de Entity Framework
                var loteExistente = _dbContext.Lote.Find(lote.IdLote);

                if (loteExistente == null)
                    throw new InvalidOperationException($"No se encontró el lote con ID {lote.IdLote} para ser modificado.");

                // 2. Seteamos los valores actualizados que vienen de la BLL
                // (Principalmente la CantidadActual que cambia con las mermas o ventas)
                _dbContext.Entry(loteExistente).CurrentValues.SetValues(lote);
            }
            catch (Exception ex)
            {
                // Lanzamos una excepción detallada de infraestructura que será atrapada por la BLL
                throw new Exception("Falla crítica en el mapeo de persistencia al intentar actualizar el lote físico.", ex);
            }
        }

        public void Delete(Guid idLote)
        {
            try
            {
                if (idLote == Guid.Empty)
                    throw new ArgumentException("El identificador del lote provisto para la eliminación es inválido.");

                // 1. Buscamos el lote físicamente en SQL Server
                var loteDb = _dbContext.Lote.Find(idLote);

                if (loteDb != null)
                {
                    // 2. Lo removemos del set de datos
                    _dbContext.Lote.Remove(loteDb);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de infraestructura de datos al intentar eliminar el registro físico del lote [{idLote}].", ex);
            }
        }
    }
}
