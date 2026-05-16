using BLL.DomainDtos;
using BLL.GestiónStock.Exceptions;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Mapper;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;


namespace BLL.GestiónStock.Service
{
    public class ProductoProveedorService : IProductoProveedorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ProductoProveedorDTO> _validator;

        public ProductoProveedorService(IUnitOfWork uow, IValidator<ProductoProveedorDTO> validator)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        #region Métodos Públicos

        public void VincularProductoProveedor(ProductoProveedorDTO dto)
        {
            try
            {
                // 1. Validaciones sintácticas básicas (FluentValidation)
                ValidarDto(dto);

                // 2. Validar existencia de las entidades en la DB
                var producto = _uow.ProductoRepository.GetById(dto.IdProducto);
                if (producto == null || !producto.Habilitado == false)
                    throw new ProductoProveedorServiceException("El producto seleccionado no existe o está deshabilitado.");

                var proveedor = _uow.ProveedorRepository.GetById(dto.IdProveedor);
                if (proveedor == null || !proveedor.Habilitado == false)
                    throw new ProductoProveedorServiceException("El proveedor seleccionado no existe o está deshabilitado.");

                // 3. Regla de Negocio: Validar duplicados de la relación en esta sucursal/catálogo
                var relacionesExistentes = _uow.ProductoProveedorRepository.GetByProveedor(dto.IdProveedor);
                bool yaExisteVínculo = relacionesExistentes.Any(r => r.IdProducto == dto.IdProducto);

                if (yaExisteVínculo)
                    throw new ProductoProveedorServiceException("Operación inválida: Este producto ya se encuentra vinculado al proveedor seleccionado.");

                // 4. Regla de Negocio Compleja: Control de Proveedor Principal
                // Si el DTO viene marcado como Principal, tenemos que desmarcar el principal anterior de ese producto
                if (dto.EsProveedorPrincipal)
                    NormalizarProveedorPrincipal(dto.IdProducto);

                // 5. Mapeo y encolado en memoria RAM
                var entity = dto.ToEntity();
                _uow.ProductoProveedorRepository.Add(entity);

                // 6. Confirmación de la transacción mediante Unit of Work
                _uow.SaveChanges();
            }
            catch (ProductoProveedorServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProductoProveedorServiceException("Error de negocio: No se pudo registrar la vinculación del producto con el proveedor.", ex);
            }
        }

        public void DesvincularProductoProveedor(Guid idProductoProveedor)
        {
            try
            {
                if (idProductoProveedor == Guid.Empty)
                    throw new ProductoProveedorServiceException("El ID de relación proporcionado no es válido.");

                // Ejecuta la remoción en memoria
                _uow.ProductoProveedorRepository.Remove(idProductoProveedor);

                // Impacta físicamente en la DB
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ProductoProveedorServiceException("Error de negocio: No se pudo eliminar la vinculación comercial.", ex);
            }
        }

        public IEnumerable<ProductoProveedorDTO> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            try
            {
                var entidades = _uow.ProductoProveedorRepository.GetByProveedor(idProveedor);
                return entidades.ToDTOList();
            }
            catch (Exception ex)
            {
                throw new ProductoProveedorServiceException("Error al recuperar el catálogo del proveedor.", ex);
            }
        }

        public IEnumerable<ProductoProveedorDTO> ObtenerProveedoresPorProducto(Guid idProducto)
        {
            try
            {
                var entidades = _uow.ProductoProveedorRepository.GetByProducto(idProducto);
                return entidades.ToDTOList();
            }
            catch (Exception ex)
            {
                throw new ProductoProveedorServiceException("Error al recuperar la lista de proveedores del producto.", ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void ValidarDto(ProductoProveedorDTO dto)
        {
            if (dto == null)
                throw new ProductoProveedorServiceException("El objeto de relación Producto-Proveedor no puede ser nulo.");

            if (_validator == null)
                throw new ProductoProveedorServiceException("Error interno: El validador correspondiente no fue inyectado.");

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {
                throw new ProductoProveedorServiceException("Error interno al procesar la validación de la relación.", ex);
            }

            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                                  ?? "Error de validación desconocido.";

                throw new ProductoProveedorServiceException(primerError);
            }
        }

        private void NormalizarProveedorPrincipal(Guid idProducto)
        {
            // Buscamos todas las relaciones existentes para ese producto específico
            var proveedoresDelProducto = _uow.ProductoProveedorRepository.GetByProducto(idProducto);

            // Si hay alguna relación que estaba marcada como principal, la desmarcamos
            foreach (var relacion in proveedoresDelProducto)
            {
                if (relacion.EsProveedorPrincipal == true)
                {
                    relacion.EsProveedorPrincipal = false;
                    // Al ser objetos traídos por EF con Tracking activo, modificarlos acá
                    // hace que queden listos para enviarse como UPDATE cuando se ejecute el SaveChanges() global.
                }
            }
        }

        #endregion
    }
}
