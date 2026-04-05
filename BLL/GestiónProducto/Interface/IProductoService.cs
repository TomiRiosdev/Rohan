using BLL.DomainDtos;


namespace BLL.GestiónProducto.Interface
{
    public interface IProductoService
    {
        // Métodos CRUD
        void AgregarProducto(ProductoDTO productoDto);
        void ModificarProducto(ProductoDTO productoDto);
        void DeshabilitarProducto(Guid id);
        void HabilitarProducto(Guid id);

        // Métodos de consulta
        List<ProductoDTO> GetHabilitados();
        List<ProductoDTO> GetDeshabilitados();
        ProductoDTO GetById(Guid id);
        ProductoDTO GetByCodigoSku(int codigoSku);
        List<ProductoDTO> GetByNombre(string nombre);

    }
}
