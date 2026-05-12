using Service.DataAccess.Implementations;
using Service.DomainModel.Composite;
using System.Collections.Generic;
using System.Linq;


namespace Service.Logic
{
    public class PatenteService
    {
        private readonly PatenteRepository _patenteRepo;

        public PatenteService()
        {
            _patenteRepo = new PatenteRepository();
        }

        public List<Patente> ObtenerTodas()
        {
            return _patenteRepo.GetAll().ToList();
        }
    }
}
