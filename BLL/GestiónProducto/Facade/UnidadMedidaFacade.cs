using BLL.GestiónProducto.Interface;
using BLL.DomainDtos;


namespace BLL.GestiónProducto.Facade
{
    public class UnidadMedidaFacade
    {
        private readonly IUnidadMedidaService _unidadMedidaService;
        public UnidadMedidaFacade(IUnidadMedidaService unidadMedidaService)
        {
            _unidadMedidaService = unidadMedidaService;
        }

        #region Metodos CRUD

        public void AgregarUnidadMedidad(UnidadMedidaDTO dto)
        {
            _unidadMedidaService.AgregarUnidadMedidad(dto);
        }

        public void ModificarUnidadMedida(UnidadMedidaDTO dto)
        {
            _unidadMedidaService.ModificarUnidadMedida(dto);
        }

        public void DeshabilitarUnidadMedida(Guid id)
        {
            _unidadMedidaService.DeshabilitarUnidadMedida(id);
        }

        public void HabilitarUnidadMedida(Guid id)
        {
            _unidadMedidaService.HabilitarUnidadMedida(id);
        }

        public UnidadMedidaDTO GetById(Guid id)
        {
            return _unidadMedidaService.GetById(id);
        }
        public List<UnidadMedidaDTO> GetHabilitados()
        {
           return _unidadMedidaService.GetHabilitados();
        }

        #endregion

    }
}
