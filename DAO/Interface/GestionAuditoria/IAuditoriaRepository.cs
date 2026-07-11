using Models;

namespace DAO.Interface.GestionAuditoria
{
   public interface IAuditoriaRepository
   {
        void AddAuditoria(Auditoria auditoria);

        IEnumerable<Auditoria> GetAll();
    }
}
