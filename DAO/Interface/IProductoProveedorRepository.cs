using Models;


namespace DAO.Interface
{
    public interface IProductoProveedorRepository
    {
        void Add(ProductoProveedor entity);
        IEnumerable<ProductoProveedor> GetByProveedor(Guid idProveedor);
        IEnumerable<ProductoProveedor> GetByProducto(Guid idProducto);
        void Remove(Guid idProductoProveedor);
    }
}
