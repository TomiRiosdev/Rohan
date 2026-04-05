using BLL.DomainDtos;
using BLL.GestiónSucursal.Interface;

namespace BLL.GestiónSucursal.Facade
{
    public class TipoSucursalFacade
    {
        private readonly ITipoSucursalService _tipoSucursalService;

        public TipoSucursalFacade
        (
            ITipoSucursalService tipoSucursalService
        )
        {
            _tipoSucursalService = tipoSucursalService ?? throw new ArgumentNullException(nameof(tipoSucursalService));
        }

        public void Agregar(TipoSucursalDTO dto)
        {
            _tipoSucursalService.AgregarTipoSucursal(dto);
        }
        public void getById(Guid id)
        {
            _tipoSucursalService.GetById(id);
        }
         public List<TipoSucursalDTO> GetHabilitados()
        {
            return _tipoSucursalService.GetHabilitados();
        }
     
        public void Modificar(TipoSucursalDTO dto)
        {
            _tipoSucursalService.ModificarTipoSucursal(dto);
        }
    }
}
