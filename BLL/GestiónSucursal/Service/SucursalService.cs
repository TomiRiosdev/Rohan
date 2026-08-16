using BLL.DomainDtos;
using BLL.GestiónSucursal.Exceptions;
using BLL.GestiónSucursal.Interface;
using BLL.GestiónSucursal.Mapper;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;


namespace BLL.GestiónSucursal.Service
{
    public class SucursalService : ISucursalService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<SucursalDTO> _validator;
        public SucursalService
        (
            IValidator<SucursalDTO> validator,
            IUnitOfWork uow
        )
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        #region METODOS CRUD
        public void Agregar(SucursalDTO dto)
        {
            try
            {
                // Validacion de datos de entrada
                ValidarDto(dto);

                // Validar que el tipo de sucursal exista
                if(_uow.TipoSucursalRepository.GetById(dto.IdTipoSucursal) == null)
                    throw new SucursalServiceException("El tipo de sucursal especificado no existe.");

                // Validar que no exista una sucursal con el mismo nombre
                if(_uow.SucursalRepository.ExistsByName(dto.Nombre))
                    throw new SucursalServiceException("Ya existe una sucursal con el mismo nombre.");

                // Mapear DTO a entidad
                var entity = SucursalMapper.ToEntity(dto);

                entity.IdSucursal = Guid.NewGuid(); // Asignar un nuevo ID
                entity.Habilitado = true; 
                
                _uow.SucursalRepository.Add(entity);
                _uow.SaveChanges();

            }
            catch (SucursalServiceException)
            {
                throw; // Re-lanzar excepciones de validación específicas
            }
            catch (Exception ex)
            {

                throw new SucursalServiceException("Error interno al agregar la sucursal.", ex);
            }
        }

        public void Deshabilitar(Guid id)
        {
            try
            {
                var entity = _uow.SucursalRepository.GetById(id);
                if (entity == null)
                    throw new SucursalServiceException("No se encontró la sucursal especificada.");
               
                entity.Habilitado = false;

                _uow.SucursalRepository.Update(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new SucursalServiceException("Error interno al deshabilitar la sucursal.", ex);
            }
        }
        
        public void Habilitar(Guid id)
        {
            try
            {
                var entity = _uow.SucursalRepository.GetById(id);
                if (entity == null)
                    throw new SucursalServiceException("No se encontró la sucursal especificada.");
                entity.Habilitado = true;
                _uow.SucursalRepository.Update(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new SucursalServiceException("Error interno al habilitar la sucursal.", ex);
            }
        }

        public void Modificar(SucursalDTO dto)
        {
            try
            {
                if(dto.Id == Guid.Empty)
                    throw new SucursalServiceException("El ID de la sucursal no puede ser vacío.");
                
                ValidarDto(dto);

                var entity = _uow.SucursalRepository.GetById(dto.Id);
                if (entity == null)
                    throw new SucursalServiceException("No se encontró la sucursal especificada.");

                if(_uow.TipoSucursalRepository.GetById(dto.IdTipoSucursal) == null)
                    throw new SucursalServiceException("El tipo de sucursal especificado no existe.");

                if(_uow.SucursalRepository.ExistsByNameExceptId(dto.Nombre, dto.Id))
                    throw new SucursalServiceException("Ya existe otra sucursal con el mismo nombre.");
            
               entity.Nombre = dto.Nombre;
               entity.Email = dto.Email;
               entity.Direccion = dto.Direccion;
               entity.CodigoPostal = dto.CodigoPostal;
               entity.Telefono = dto.Telefono;
               entity.IdTipoSucursal = dto.IdTipoSucursal;
               entity.Localidad = dto.Localidad;
               _uow.SucursalRepository.Update(entity);
               _uow.SaveChanges();
            }
            catch (SucursalServiceException)
            {
                throw; // Re-lanzar excepciones de validación específicas
            }
            catch (Exception ex)
            {

                throw new SucursalServiceException("Error interno al modificar la sucursal.", ex);
            }    
        }

        #endregion

        #region METODOS DE CONSULTA
        public SucursalDTO GetById(Guid id)
        {
            try
            {
                if(id == Guid.Empty) 
                    throw new SucursalServiceException("El ID de la sucursal no puede ser vacío.");
                var entity = _uow.SucursalRepository.GetById(id);
                if(entity == null)
                    throw new SucursalServiceException("No se encontró la sucursal especificada.");
                return SucursalMapper.ToDTO(entity);

            }
            catch (Exception ex)
            {

                throw new SucursalServiceException("Error interno al obtener la sucursal por ID.", ex);
            }
        }

        public List<SucursalDTO> GetByNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    return GetDeshabilitados();
                var entities = _uow.SucursalRepository.GetByNombre(nombre)
                    .Where(s => s.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                return entities.Select(SucursalMapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {

                throw new SucursalServiceException("Error interno al obtener las sucursales por nombre.", ex);
            }    
        }

        public List<SucursalDTO> GetDeshabilitados()
        {
            try
            {
                var entities = _uow.SucursalRepository.GetAllDesHabilitados()
                    .Where(s => s.Habilitado == false);
                return entities.Select(SucursalMapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {

                throw new SucursalServiceException("Error interno al obtener las sucursales deshabilitadas.", ex);
            }
        }

        public List<SucursalDTO> GetHabilitados()
        {
            try
            {
                var entities = _uow.SucursalRepository.GetAll()
                    .Where(s => s.Habilitado == true);
                return entities.Select(SucursalMapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new SucursalServiceException("Error interno al obtener las sucursales habilitadas.", ex);
            }
        }

        public Guid ObtenerIdDepositoCentral()
        {
            try
            {
                // 1. Buscamos el ID del Tipo de Sucursal usando su Descripción exacta
                var tipoDeposito = _uow.TipoSucursalRepository.GetAll()
                    .FirstOrDefault(t => t.Descripcion != null &&
                                         t.Descripcion.Equals("Depósito y Stock", StringComparison.OrdinalIgnoreCase));

                if (tipoDeposito == null)
                    throw new SucursalServiceException("Error de Configuración: No existe el tipo de sucursal 'Depósito y Stock' en el sistema.");

                // 2. Buscamos la sucursal habilitada que tenga asignado ese Tipo
                var depositoCentral = _uow.SucursalRepository.GetAll()
                    .FirstOrDefault(s => s.IdTipoSucursal == tipoDeposito.IdTipoSucursal && s.Habilitado == true);

                if (depositoCentral == null)
                    throw new SucursalServiceException("No hay ninguna sucursal habilitada configurada como Depósito Central.");

                // 3. Retornamos el Guid de la sucursal (Rohan HQ en tu caso)
                return depositoCentral.IdSucursal;
            }
            catch (SucursalServiceException)
            {
                throw; // Relanzamos si es nuestra excepción controlada
            }
            catch (Exception ex)
            {
                throw new SucursalServiceException("Error interno al intentar localizar el depósito central.", ex);
            }
        }

        #endregion

        #region Validaciones

        public void ValidarDto(SucursalDTO dto)
        {
            if (dto == null)
                throw new SucursalServiceException("El objeto sucursal no puede ser nulo.");

            if (_validator == null)
            {
                throw new SucursalServiceException("Error interno: El validador no fue inyectado correctamente.");
            }

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {

                throw new SucursalServiceException("Error interno al validar la sucursal.", ex);
            }

            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                               ?? "Error de validación desconocido.";

                throw new SucursalServiceException(primerError);
            }
        }

        #endregion
    }
}