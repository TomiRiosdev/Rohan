using AutoMapper;
using BLL.MappingProfiles;
using DAO;
using DAO.Implementations.SQLServer;
using DAO.Interface;
using ModelsDTO;

namespace BLL.Service
{
    public class ProductoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        // Constructor público para la Inyección de Dependencias
        public ProductoService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void AddProducto(ProductoDTO productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            producto.IdProducto = Guid.NewGuid(); // Asignar un nuevo GUID
            producto.Habilitado = true; // Asegurarse de que esté habilitado al crear
            _unitOfWork.ProductoRepository.Add(producto);
            _unitOfWork.SaveChanges();
        }
  
        public void UpdateProducto(ProductoDTO productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            _unitOfWork.ProductoRepository.Update(producto);
            _unitOfWork.SaveChanges();
        }

        public void DeleteProducto(Guid id)
        {
            _unitOfWork.ProductoRepository.Remove(id);
            _unitOfWork.SaveChanges();
        }

        public ProductoDTO GetProductoById(Guid id)
        {
            var producto = _unitOfWork.ProductoRepository.GetById(id);
            return _mapper.Map<ProductoDTO>(producto);
        }

        public IEnumerable<ProductoDTO> GetAllProductos()
        {
            var productos = _unitOfWork.ProductoRepository.GetAll();
            // **Convertir la Entidad de la DAO a DTO para devolver a la UI**
            return _mapper.Map<IEnumerable<ProductoDTO>>(productos);
        }

        public IEnumerable<ProductoDTO> GetProductosDeshabilitados()
        {
            var productos = _unitOfWork.ProductoRepository.GetAll()
                .Where(p => p.Habilitado); // Filtrar solo los habilitados
            return _mapper.Map<IEnumerable<ProductoDTO>>(productos);
        }

    }
}
