using DAO.Interface.GestionAuditoria;
using Models;

namespace DAO.Implementations.SQLServer.GestionAuditoria
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        private readonly RohanContext _dbContext;
        public AuditoriaRepository
        (
            RohanContext dbContext            
        )
        {
            _dbContext = dbContext;
        }

        public void AddAuditoria(Auditoria auditoria)
        {
            _dbContext.Auditoria.Add(auditoria);
        }
      
        IEnumerable<Auditoria> IAuditoriaRepository.GetAll()
        {
            return _dbContext.Auditoria.OrderByDescending(x => x.Fecha).ToList();
        }
    }
}
