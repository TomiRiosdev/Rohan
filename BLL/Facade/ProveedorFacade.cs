using BLL.Service;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class ProveedorFacade
    {
        private readonly ProveedorService _proveedorService;
        public ProveedorFacade()
        {
            _proveedorService = new ProveedorService();
        }

        public void AddProveedor(ProveedorDTO proveedorDto)
        {
             _proveedorService.AddProveedor(proveedorDto);
        }
        public void UpdateProveedor(ProveedorDTO proveedorDto)
        {
            _proveedorService.UpdateProveedor(proveedorDto);
        }
        public void RemoveProveedor(Guid id)
        {
            _proveedorService.RemoveProveedor(id);
        }
        public ProveedorDTO GetProveedorById(Guid id)
        {
            return _proveedorService.GetProveedorById(id);
        }
        public IEnumerable<ProveedorDTO> GetAllProveedores()
        {
            return _proveedorService.GetAllProveedores();
        }
        public ProveedorDTO GetProveedorByNombre(string name)
        {
            return _proveedorService.GetProveedorByNombre(name);
        }

       public IEnumerable<ProveedorDTO> GetProveedoresHabilitados()
       {
          return _proveedorService.GetProveedoresHabilitados();
       }
    }
}
