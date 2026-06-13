using BLL.DomainDtos;
using BLL.GestiónProveedor.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónProveedor.Facade
{
    public class ProductoProveedorFacade 
    {
        private readonly IProductoProveedorService _productoProveedorService;
        
        public ProductoProveedorFacade
        (
            IProductoProveedorService productoProveedorService
        )
        {
            _productoProveedorService = productoProveedorService ?? throw new ArgumentNullException(nameof(productoProveedorService));
        }

        public void VincularProductoAProveedor(ProductoProveedorDTO dto)
        {
            _productoProveedorService.VincularProductoAProveedor(dto);
        }
        public void DesvincularProductoDeProveedor(Guid idProducto, Guid idProveedor)
        {
            _productoProveedorService.DesvincularProductoDeProveedor(idProducto, idProveedor);
        }
        public IEnumerable<ProductoProveedorDTO> ListarProductosPorProveedor(Guid idProveedor)
        {
            return _productoProveedorService.ListarProductosPorProveedor(idProveedor);
        }
        public IEnumerable<ProductoProveedorDTO> ListarProveedoresPorProducto(Guid idProducto)
        {
            return _productoProveedorService.ListarProveedoresPorProducto(idProducto);
        }
        public void Dispose()
        {
            // Si el servicio implementa IDisposable, lo liberamos aquí
            if (_productoProveedorService is IDisposable disposableService)
            {
                disposableService.Dispose();
            }
        }
    }

}
