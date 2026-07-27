using DAO.Interface.GestionStock;
using Models;
using System;


namespace DAO.Implementations.SQLServer.GestionStock
{
    public class OrdenTraspasoRepository : IOrdenTraspasoRepository
    {
        private readonly RohanContext _dbContext;
      
        public OrdenTraspasoRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AddOrdenTraspaso(OrdenTraspaso entity)
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity));
            _dbContext.OrdenTraspasos.Add(entity);
              
        }

        public OrdenTraspaso GetById(Guid id)
        {
            try
            { 
                return _dbContext.OrdenTraspasos.FirstOrDefault(o => o.IdOrdenTraspaso == id);
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los traspaso por Id.", ex);
            }

        }

        public IEnumerable<OrdenTraspaso> GetTraspasosPendientes(Guid idSucursalOrigen)
        {
            try
            {
                return _dbContext.OrdenTraspasos
                    .Where(o => o.IdSucursalOrigen == idSucursalOrigen && o.IdEstado == 1) // Assuming 1 is the pending state
                    .ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los traspasos pendientes.", ex);
            }
        }
    }
}
