using Models;


namespace DAO.Interface.GestionProveedor
{
    public interface IProductoProveedorRepository
    {
        void Add(ProductoProveedor entity);
        void Delete(Guid idProducto, Guid idProveedor);
        IEnumerable<ProductoProveedor> GetByProveedor(Guid idProveedor);
        IEnumerable<ProductoProveedor> GetByProducto(Guid idProducto);
        bool ExisteRelacion(Guid idProducto, Guid idProveedor);
        void UpdatePrecioUnitario(Guid idProducto, Guid idProveedor, decimal nuevoPrecioUnitario);
        void AgregarProveedorPrincipal(Guid idProducto, Guid idProveedor);


    }
}
