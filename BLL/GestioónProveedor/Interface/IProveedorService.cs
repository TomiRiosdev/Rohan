using BLL.DomainDtos;


namespace BLL.GestiónProveedor.Interface
{
   public interface IProveedorService
   {
        // Métodos CRUD
        void AgregarProveedor(ProveedorDTO proveedorDto);
        void ModificarProveedor(ProveedorDTO proveedorDto);
        void DeshabilitarProveedor(Guid id);
        void HabilitarProveedor(Guid id);

        // Métodos de consulta
        ProveedorDTO GetById(Guid id);
        List<ProveedorDTO> GetHabilitados();
        List<ProveedorDTO> GetDeshabilitados();
        List<ProveedorDTO> GetByNombre(string nombre);

   }
}
