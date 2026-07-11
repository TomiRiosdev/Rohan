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
            _dbContext.Auditorias.Add(auditoria);
        }
      
        IEnumerable<Auditoria> IAuditoriaRepository.GetAll()
        {
            return _dbContext.Auditorias.OrderByDescending(x => x.Fecha).ToList();
        }
    }
}
