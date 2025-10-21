using BLL.Service;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class UnidadMedidaFacade
    {
        private readonly UnidadMedidaService _unidadMedidaService;
        public UnidadMedidaFacade()
        {
            _unidadMedidaService = new UnidadMedidaService();
        }

        public void Add(UnidadMedidaDTO unidadMedida)
        {
            _unidadMedidaService.Add(unidadMedida);
        }
        public void Update(UnidadMedidaDTO unidadMedida)
        {
            _unidadMedidaService.Update(unidadMedida);
        }
        public void Remove(Guid id)
        {
            _unidadMedidaService.Remove(id);
        }
        public IEnumerable<UnidadMedidaDTO> GetAll()
        {
            return _unidadMedidaService.GetAll();
        }
        public UnidadMedidaDTO GetById(Guid id)
        {
            return _unidadMedidaService.GetById(id);
        }
        public IEnumerable<UnidadMedidaDTO> GetAllDesHabilitados()
        {
            return _unidadMedidaService.GetDeshabilitados();
        }
        public void GetByNombre(string nombre)
        {
            _unidadMedidaService.GetByNombre(nombre);
        }   
    }
}
