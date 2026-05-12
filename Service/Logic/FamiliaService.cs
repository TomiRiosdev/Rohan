using Service.DateAccess.Implementations;
using Service.DomainModel.Composite;
using System.Collections.Generic;
using System.Linq;

namespace Service.Logic
{
    public class FamiliaService
    {
 
        private readonly FamiliaRepository _familiaRepo;

        public FamiliaService()
        {
            _familiaRepo = new FamiliaRepository();
        }

        public List<Familia> ObtenerTodas()
        {
            
            return _familiaRepo.GetAll().ToList();
        }
    }
}
