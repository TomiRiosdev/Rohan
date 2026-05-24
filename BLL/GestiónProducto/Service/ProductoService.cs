using BLL.Bitacora.Interface;
using BLL.GestiónProducto.Exceptions;
using BLL.GestiónProducto.Interface;
using BLL.GestiónProducto.Mapper;
using BLL.DomainDtos;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;

namespace BLL.GestiónProducto.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ProductoDTO> _validator;
        //private readonly IBitacoraService _bitacora;

        public ProductoService
        (
            IUnitOfWork uow,
            IValidator<ProductoDTO> validator 
            // IBitacoraService bitacora)
        )
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            //_bitacora = bitacora;
        }

        #region Metodos CRUD
        public void AgregarProducto(ProductoDTO productoDto)
        {  
           try
           {
                // 1.Validaciones de datos básicos
                ValidarDto(productoDto);

                // 2. Validaciones de existencia  de Categoria y Unidad de Medida
                if (_uow.CategoriaRepository.GetById(productoDto.IdCategoria) == null)
                    throw new ProductoServiceException("La Categoría seleccionada no existe.");

                if (_uow.UnidadMedidaRepository.GetById(productoDto.IdUnidadMedida) == null)
                    throw new ProductoServiceException("La Unidad de Medida seleccionada no existe en el sistema.");

                // 3. Validar duplicados por Nombre 
                if (_uow.ProductoRepository.ExistsByName(productoDto.Nombre))
                    throw new ProductoServiceException("Ya existe un producto registrado con ese nombre.");

                // 4. Validar duplicados por Código SKU (si se ingresó)
                if (productoDto.CodigoSku.HasValue &&
                    _uow.ProductoRepository.ExistsByCodigoSku(productoDto.CodigoSku.Value))
                    throw new ProductoServiceException("Ya existe un producto con ese Código SKU.");

                // 4. Mapeo y persistencia
                var entity = ProductoMapper.ToEntity(productoDto);

                entity.IdProducto = Guid.NewGuid();
                entity.FechaCreacion = DateTime.Now;
                entity.Habilitado = true;
                // entity.UsuarioCreacion = "UsuarioActual"; // 

                // 5. Impacto en DB
                _uow.ProductoRepository.Add(entity);
                _uow.SaveChanges();

                //  Bitácora de creación
                //  _bitacora.Registrar.IdProducto, "Producto creado", $"Se registró el producto: {entity.Nombre}");
            }
            catch (ProductoServiceException ex)
            {        
                throw;   // Re-lanzamos para que llegue al formulario
            }
            catch (Exception ex)
            {   
                throw new ProductoServiceException("Error interno al intentar crear el producto. Contacte al administrador.", ex);
            }
        }

        public void DeshabilitarProducto(Guid id)
        {
            try
            {
                var entity = _uow.ProductoRepository.GetById(id);
                if (entity == null)
                    throw new ProductoServiceException("Producto no encontrado.");

                entity.Habilitado = false;
                //  entity.FechaModificacion = DateTime.UtcNow;

                _uow.ProductoRepository.Update(entity);
                _uow.SaveChanges();

                // Bitácora de creación
                // _bitacora.Registrar(id, "Producto deshabilitado", $"Se deshabilitó el producto: {entity.Nombre}");
            }
            catch (Exception ex)
            {

                throw new ProductoServiceException("Error interno al intentar deshabilitar un producto", ex);
            }
        }

        public void HabilitarProducto(Guid id)
        {
            try
            {

                var entity = _uow.ProductoRepository.GetById(id);
                if (entity == null)
                    throw new ProductoServiceException("Producto no encontrado.");

                entity.Habilitado = true;
                // entity.FechaModificacion = DateTime.UtcNow;

                _uow.ProductoRepository.Update(entity);
                _uow.SaveChanges();

                // Bitácora de creación
                // _bitacora.Registrar(id, "Producto Habilitado", $"Se habilitó el producto: {entity.Nombre}");
            }
            catch (Exception ex)
            {

                throw new ProductoServiceException("Error interno al intentar habilitar un producto", ex);
            }
        }

        public void ModificarProducto(ProductoDTO productoDto)
        {
            try
            {

                if (productoDto.Id == Guid.Empty)
                    throw new ProductoServiceException("El ID del producto es requerido.");

                ValidarDto(productoDto);

                var entity = _uow.ProductoRepository.GetById(productoDto.Id);
                if (entity == null)
                    throw new ProductoServiceException("Producto no encontrado.");

                // Validar existencia de relaciones
                if (_uow.CategoriaRepository.GetById(productoDto.IdCategoria) == null)
                    throw new ProductoServiceException("Categoría inválida.");

                if (_uow.UnidadMedidaRepository.GetById(productoDto.IdUnidadMedida) == null)
                    throw new ProductoServiceException("Unidad de medida inválida.");

                // Validar duplicado excluyendo el producto actual
                 if (_uow.ProductoRepository.ExistsByNameExceptId(productoDto.Nombre, productoDto.Id))
                      throw new ProductoServiceException("Ya existe otro producto con ese nombre.");

                entity.Nombre = productoDto.Nombre;
                entity.IdCategoria = productoDto.IdCategoria;
                entity.IdUnidadMedida = productoDto.IdUnidadMedida;
                entity.CodigoSku = productoDto.CodigoSku;
                entity.Descripcion = productoDto.Descripcion;
                // entity.FechaModificacion = DateTime.UtcNow;
                // entity.UsuarioModificacion = "UsuarioActual";

                _uow.ProductoRepository.Update(entity);
                _uow.SaveChanges();

                // Bitácora de creación
                // _bitacora.Registrar(entity.IdProducto, "Producto modificado", $"Se actualizó el producto: {entity.Nombre}");
            }
            catch (ProductoServiceException)
            {
                throw;   // Re-lanzamos para que llegue al formulario
            }
            catch (Exception ex)
            {

                throw new ProductoServiceException("Error interno al intentar modificar un producto", ex);
            }
        }

        #endregion

        #region Metodos de Consulta

        public ProductoDTO GetById(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ProductoServiceException("ID inválido.");
                var entity = _uow.ProductoRepository.GetById(id);
                if (entity == null) throw new ProductoServiceException("Producto no encontrado.");
                return ProductoMapper.ToDTO(entity);
            }
            catch (Exception ex)
            {

                throw new ProductoServiceException("Error interno", ex);
            }
        }

        public List<ProductoDTO> GetByNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre)) return GetHabilitados();
                var entities = _uow.ProductoRepository.GetAll()
                    .Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
                return entities.Select(p => ProductoMapper.ToDTO(p)).ToList();
            }
            catch (Exception ex)
            {

                throw new ProductoServiceException("Error interno", ex);
            }
        }

        public List<ProductoDTO> GetDeshabilitados()
        {
            try
            {
                var entities = _uow.ProductoRepository.GetAllDesHabilitados().Where(p => p.Habilitado == false);
                return entities.Select(p => ProductoMapper.ToDTO(p)).ToList();
            }
            catch (Exception ex)
            {

                throw new ProductoServiceException("Error interno", ex);
            }
        }

        public List<ProductoDTO> GetHabilitados()
        {
            try
            {
                var entities = _uow.ProductoRepository.GetAll();
                return entities.Select(p => ProductoMapper.ToDTO(p)).ToList();
            }
            catch (Exception ex)
            {

                throw new ProductoServiceException("Error interno", ex);
            }
        }

        public ProductoDTO GetByCodigo(int sku)
        {
            try
            {
                if (sku <= 0) throw new ProductoServiceException("El código debe ser un número positivo.");

                // Buscamos en el repo usando el entero
                var entity = _uow.ProductoRepository.GetAll()
                    .FirstOrDefault(p => p.CodigoSku == sku);

                if (entity == null)
                    throw new ProductoServiceException($"No se encontró el producto con código: {sku}");

                return ProductoMapper.ToDTO(entity);
            }
            catch (Exception ex)
            {

                throw new ProductoServiceException("Error interno", ex);
            }
        }
        #endregion

        #region Métodos de Validación Privados

        private void ValidarDto(ProductoDTO dto)
        {
            if (dto == null)
                throw new ProductoServiceException("El objeto producto no puede ser nulo.");

            if (_validator == null)
            {
                throw new ProductoServiceException("Error interno: El validador no fue inyectado correctamente.");
            }

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {
               
                throw new ProductoServiceException("Error interno al validar el producto.", ex);
            }
            
            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                               ?? "Error de validación desconocido.";
 
                throw new ProductoServiceException(primerError);
            }         
        }

        public ProductoDTO GetByCodigoSku(int codigoSku)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
