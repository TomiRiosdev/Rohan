using BLL.Service;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class CategoriaFacade
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaFacade()
        {
            _categoriaService = new CategoriaService();
        }

        #region Metodos Categoria
        public void AddCategoria(CategoriaDTO categoriaDTO)
        {
            _categoriaService.AddCategoriaProducto(categoriaDTO);
        }
        public void UpdateCategoria(CategoriaDTO categoriaDTO)
        {
            _categoriaService.UpdateCategoriaProducto(categoriaDTO);
        }
        public void DeleteCategoria(Guid id)
        {
            _categoriaService.DeleteCategoriaProducto(id);
        }
        public object GetCategoriaById(Guid id)
        {
            return _categoriaService.GetCategoriaProductoById(id);
        }
        public IEnumerable<object> GetAllCategorias()
        {
            return _categoriaService.GetAllCategoriaProductos();
        }
        #endregion
    }
}
