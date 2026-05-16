using BLL.DomainDtos;

namespace BLL.GestiónStock.Interface
{
    public interface IProductoProveedorService
    {
        void VincularProductoProveedor(ProductoProveedorDTO dto);
        void DesvincularProductoProveedor(Guid idProductoProveedor);
        IEnumerable<ProductoProveedorDTO> ObtenerProductosPorProveedor(Guid idProveedor);
        IEnumerable<ProductoProveedorDTO> ObtenerProveedoresPorProducto(Guid idProducto);
    }
}
