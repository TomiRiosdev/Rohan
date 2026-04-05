using BLL.GestiónProducto.Interface;
using BLL.DomainDtos;   

namespace BLL.GestiónProducto.Facade
{
    public class ProductoFacade
    {
        private readonly IProductoService _productoService;

        // Inyectamos la interfaz del servicio
        public ProductoFacade(IProductoService productoService)
        {
            _productoService = productoService;
        }

        #region Métodos de Lectura 

        public List<ProductoDTO> ListarProductosActivos()
        {
            return _productoService.GetHabilitados();
        }

        public List<ProductoDTO> ListarProductosBaja()
        {
            return _productoService.GetDeshabilitados();
        }

        public ProductoDTO BuscarPorId(Guid id)
        {
            return _productoService.GetById(id);
        }

     

        #endregion

        #region Métodos de Escritura

        public void AgregarProducto(ProductoDTO dto)
        {
           _productoService.AgregarProducto(dto); 
        }

        public void ModificarProducto(ProductoDTO dto)
        {
            if (dto.Id == Guid.Empty)
                throw new ArgumentException("El ID del producto es requerido para modificar.");
            _productoService.ModificarProducto(dto);
        }

        public void BajaLogica(Guid id)
        {
            _productoService.DeshabilitarProducto(id);
        }

        public void RehabilitarProducto(Guid id)
        {
            _productoService.HabilitarProducto(id);
        }

        #endregion
    }
}
