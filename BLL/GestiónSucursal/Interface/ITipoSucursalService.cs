using BLL.DomainDtos;

namespace BLL.GestiónSucursal.Interface
{
    public interface ITipoSucursalService
    {
        // Métodos CRUD 
        void AgregarTipoSucursal(TipoSucursalDTO dto);
        void ModificarTipoSucursal(TipoSucursalDTO dto);
        void DeshabilitarTipoSucursal(Guid id);
        void HabilitarTipoSucursal(Guid id);

        // Métodos de consulta
        TipoSucursalDTO GetById(Guid id);
        List<TipoSucursalDTO> GetHabilitados();

    }
}
