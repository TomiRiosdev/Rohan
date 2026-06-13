using BLL.DomainDtos;
using BLL.GestiónProveedor.Interface;


namespace BLL.GestiónProveedor.Facade
{
    public class ProveedorFacade
    {
        private readonly IProveedorService _proveedorService;
        public ProveedorFacade
        (
            IProveedorService proveedorService
        )
        {
           _proveedorService = proveedorService;
        }

        #region METODOS DE LECTURA
        public ProveedorDTO GetById(Guid id) 
        { 
            return _proveedorService.GetById(id);
        }
        public List<ProveedorDTO> GetHabilitados() 
        {
            return _proveedorService.GetHabilitados();
        }
        public List<ProveedorDTO> GetDeshabilitados() 
        {
            return _proveedorService.GetDeshabilitados();
        }
        public List<ProveedorDTO> GetByNombre(string nombre)
        {
            return _proveedorService.GetByNombre(nombre);
        }
        #endregion

        #region METODOS CRUD

        public void AgregarProveedor(ProveedorDTO dto)
        {
            _proveedorService.AgregarProveedor(dto);
        }
        public void ModificarProveedor(ProveedorDTO dto)
        {  
            _proveedorService.ModificarProveedor(dto);
        }
        public void BajaLogica(Guid id)
        {
            _proveedorService.DeshabilitarProveedor(id);
        }
        public void RehabilitarProveedor(Guid id)
        {
            _proveedorService.HabilitarProveedor(id);
        }

        #endregion
    }
}
