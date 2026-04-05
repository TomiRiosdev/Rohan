using BLL.GestiónProducto.Interface;
using BLL.DomainDtos;

namespace BLL.GestiónProducto.Facade
{
    public class CategoriaFacade
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaFacade(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        #region Metodos Categoria
        public void AgregarCategoria(CategoriaDTO categoriaDto)
        {
            _categoriaService.AgregarCategoria(categoriaDto);
        }
        public void ModificarCategoria(CategoriaDTO categoriaDto)
        {
            _categoriaService.ModificarCategoria(categoriaDto);
        }
        public void DeshabilitarCategoria(Guid id)
        {
            _categoriaService.DeshabilitarCategoria(id);
        }
        public void HabilitarCategoria(Guid id)
        {
            _categoriaService.HabilitarCategoria(id);
        }
        public CategoriaDTO GetById(Guid id)
        {
            return _categoriaService.GetById(id);
        }

        public List<CategoriaDTO> GetHabilitados()
        {
            return _categoriaService.GetHabilitados();
        }
      
        #endregion
    }
}
