using BLL.DomainDtos;

namespace BLL.GestiónProducto.Interface
{
    public interface IUnidadMedidaService
    {
        //Métodos CRUD
        void AgregarUnidadMedidad(UnidadMedidaDTO dto);
        void ModificarUnidadMedida(UnidadMedidaDTO dto);
        void DeshabilitarUnidadMedida(Guid id);
        void HabilitarUnidadMedida(Guid id);

        //Métodos de consulta
        UnidadMedidaDTO GetById(Guid id);
        List<UnidadMedidaDTO> GetHabilitados();

    }
}
