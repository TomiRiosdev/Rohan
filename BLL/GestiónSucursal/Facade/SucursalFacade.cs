using BLL.DomainDtos;
using BLL.GestiónSucursal.Interface;

namespace BLL.GestiónSucursal.Facade
{
    public class SucursalFacade
    {
        private readonly ISucursalService _sucursalService;
        public SucursalFacade
        (
            ISucursalService sucursalService
        )
        {
            _sucursalService = sucursalService ?? throw new ArgumentNullException(nameof(sucursalService));
        }

        #region METODOS CRUD

        public void AgregarSucursal(SucursalDTO dto)
        {
           _sucursalService.Agregar(dto);
        }
    
        public void BajaLogica(Guid id)
        {
           _sucursalService.Deshabilitar(id);
        }

        public void ModificarSucursal(SucursalDTO dto)
        {
            _sucursalService.Modificar(dto);
        }

        public void HabilitarSucursal(Guid id)
        {
            _sucursalService.Habilitar(id);
        }

        #endregion
        #region METODOS DE LECTURA
        
        public SucursalDTO GetById(Guid id)
        {
            return _sucursalService.GetById(id);
        }
      
        public List<SucursalDTO> GetHabilitados()
        {
            return _sucursalService.GetHabilitados();
        }
        
        public List<SucursalDTO> GetDeshabilitados()
        {
            return _sucursalService.GetDeshabilitados();
        }
    
        public List<SucursalDTO> GetByNombre(string nombre)
        {
            return _sucursalService.GetByNombre(nombre);
        }

        public Guid ObtenerIdDepositoCentral()
        {
            return _sucursalService.ObtenerIdDepositoCentral();
        }
       
        #endregion


    }
}
