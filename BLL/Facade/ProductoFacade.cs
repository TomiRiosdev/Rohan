using BLL.Service;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using ModelsDTO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class ProductoFacade
    {
        private readonly ProductoService _productoService;

        public ProductoFacade()
        {
            _productoService = new ProductoService();
        }

        public void AddProducto(ProductoDTO productoDto)
        {
            _productoService.AddProducto(productoDto);
        }
        
        public void UpdateProducto(ProductoDTO productoDto)
        {
            _productoService.UpdateProducto(productoDto);
        }

        public void DeleteProducto(Guid id)
        {
            _productoService.DeleteProducto(id);
        }

        public ProductoDTO GetProductoById(Guid id)
        {
            return _productoService.GetProductoById(id);
        }

        public IEnumerable<ProductoDTO> GetAllProductosHabilitados()
        {
            return _productoService.GetAllProductos();
        }

        public IEnumerable<ProductoDTO> GetProductosDeshabilitados()
        {
            return _productoService.GetProductosDeshabilitados();
        }

      
    }
}
