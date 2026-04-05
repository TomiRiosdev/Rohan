using BLL.DomainDtos;


namespace BLL.GestiónSucursal.Interface
{
    public interface ISucursalService
    {
        // Métodos CRUD
        void Agregar(SucursalDTO dto);
        void Modificar(SucursalDTO dto);
        void Deshabilitar(Guid id);
        void Habilitar(Guid id);

        // Métodos de consulta
        List<SucursalDTO> GetHabilitados();
        List<SucursalDTO> GetDeshabilitados();
        SucursalDTO GetById(Guid id);
        List<SucursalDTO> GetByNombre(string nombre);
    }
}
