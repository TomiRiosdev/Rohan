using BLL.DomainDtos;
using BLL.GestiónProveedor.Exceptions;
using BLL.GestiónProveedor.Interface;
using BLL.GestiónProveedor.Mapper;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;

namespace BLL.GestiónProveedor.Service
{
   public class ProveedorService : IProveedorService
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProveedorDTO> _validator;
        //private readonly IBitacoraService _bitacora;
        public ProveedorService
        (
            IUnitOfWork unitOfWork,
            IValidator<ProveedorDTO> validator
         // IBitacoraService bitacora
        )
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            //_bitacora = bitacora;
        }

        #region Metodos CRUD

        public void AgregarProveedor(ProveedorDTO proveedorDto)
        {
            try
            {
                ValidarDto(proveedorDto);

                // 3. Validar duplicados
                if (_unitOfWork.ProveedorRepository.ExistsByName(proveedorDto.Nombre))
                    throw new ProveedorServiceException("Ya existe un proveedor registrado con ese nombre.");

                var entity = ProveedorMapper.ToEntity(proveedorDto);

                entity.IdProveedor = Guid.NewGuid();
                entity.Habilitado = true;
                // entity.FechaCreacion = DateTime.Now;

                // 3. Persistencia
                _unitOfWork.ProveedorRepository.Add(entity);
                _unitOfWork.SaveChanges();
            }
            catch (ProveedorServiceException)
            {
                throw; // Re-lanzar excepciones de validación o negocio sin envolverlas
            }
            catch (Exception ex)
            {
               throw new ProveedorServiceException("Error interno al intentar agregar el proveedor.", ex);

            }
        }

        public void DeshabilitarProveedor(Guid id)
        {
            try
            {
                var entity = _unitOfWork.ProveedorRepository.GetById(id);

                if (entity == null)
                    throw new ProveedorServiceException("El proveedor que intenta deshabilitar no existe.");

                entity.Habilitado = false;
                // entity.FechaModificacion = DateTime.Now;

                _unitOfWork.ProveedorRepository.Update(entity);
                _unitOfWork.SaveChanges();

                // Bitacora de Modificación
            }
            catch (Exception ex)
            {
                throw new ProveedorServiceException("Error interno al intentar deshabilitar el proveedor.", ex);
            }
        }

        public void HabilitarProveedor(Guid id)
        {
            try
            {
                var entity = _unitOfWork.ProveedorRepository.GetById(id);
                if(entity == null)
                    throw new ProveedorServiceException("El proveedor que intenta habilitar no existe.");
                entity.Habilitado = true;
                // entity.FechaModificacion = DateTime.Now;

                _unitOfWork.ProveedorRepository.Update(entity);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new ProveedorServiceException("Error interno al intentar habilitar el proveedor.", ex);
            }
        }

        public void ModificarProveedor(ProveedorDTO proveedorDto)
        {
            try
            {
                if(proveedorDto.Id == Guid.Empty)
                    throw new ProveedorServiceException("El ID del proveedor no puede ser vacío.");

                ValidarDto(proveedorDto);

                var entity = _unitOfWork.ProveedorRepository.GetById(proveedorDto.Id);
                if (entity == null)
                    throw new ProveedorServiceException("El proveedor que intenta modificar no existe.");

                // Validar duplicados (excluyendo el actual)
                if (_unitOfWork.ProveedorRepository.GetAll().Any(p => p.Nombre.ToLower() == proveedorDto.Nombre.ToLower() && p.IdProveedor != proveedorDto.Id))
                    throw new ProveedorServiceException("Ya existe otro proveedor registrado con ese nombre.");

                // Mapear cambios
                entity.Nombre = proveedorDto.Nombre;
                entity.RazonSocial = proveedorDto.RazonSocial;
                entity.Email = proveedorDto.Email;
                entity.Telefono = proveedorDto.Telefono;
                entity.Cuit = proveedorDto.Cuit;
                // entity.FechaModificacion = DateTime.Now;

                _unitOfWork.ProveedorRepository.Update(entity);
                _unitOfWork.SaveChanges();

            }
            catch (ProveedorServiceException)
            {
                throw; 
            }
            catch (Exception ex)
            {

                throw new ProveedorServiceException("Error interno al intentar habilitar el proveedor.", ex);
            }
        }

        #endregion

        #region Metodos de Consulta

        public ProveedorDTO GetById(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new ProveedorServiceException("ID inválido.");
                var entity = _unitOfWork.ProveedorRepository.GetById(id);
                if (entity == null) throw new ProveedorServiceException("Producto no encontrado.");
                return ProveedorMapper.ToDTO(entity);
            }
            catch (Exception ex)
            {

                throw new ProveedorServiceException("Error interno", ex);
            }
        }

        public List<ProveedorDTO> GetByNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre)) return GetHabilitados();
                var entities = _unitOfWork.ProveedorRepository.GetAll()
                    .Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
                return entities.Select(p => ProveedorMapper.ToDTO(p)).ToList();
            }
            catch (Exception ex)
            {

                throw new ProveedorServiceException("Error interno", ex);
            }
        }

        public List<ProveedorDTO> GetDeshabilitados()
        {
            try
            {
                var entities = _unitOfWork.ProveedorRepository.GetAllDesHabilitados().Where(p => p.Habilitado == false);
                return entities.Select(p => ProveedorMapper.ToDTO(p)).ToList();
            }
            catch (Exception ex)
            {

                throw new ProveedorServiceException("Error interno", ex);
            }
        }

        public List<ProveedorDTO> GetHabilitados()
        {
            try
            {
                var entities = _unitOfWork.ProveedorRepository.GetAll();
                return entities.Select(p => ProveedorMapper.ToDTO(p)).ToList();
            }
            catch (Exception ex)
            {

                throw new ProveedorServiceException("Error interno", ex);
            }
        }

        #endregion

        #region Metodos de Validación Privados
           
        private void ValidarDto(ProveedorDTO dto)
        {
            if (dto == null)
                throw new ProveedorServiceException("El objeto proveedor no puede ser nulo.");

            if (_validator == null)
            {
                throw new ProveedorServiceException("Error interno: El validador no fue inyectado correctamente.");
            }

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {
                throw new ProveedorServiceException("Error interno al validar el proveedor.", ex);
            }

            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                               ?? "Error de validación desconocido.";   
                throw new ProveedorServiceException(primerError);
            }
        }

        #endregion
    }
}
