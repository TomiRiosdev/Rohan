using BLL.DomainDtos;
using BLL.GestiónProducto.Exceptions;
using BLL.GestiónProducto.Mapper;
using BLL.GestiónSucursal.Exceptions;
using BLL.GestiónSucursal.Interface;
using BLL.GestiónSucursal.Mapper;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;
using Models;


namespace BLL.GestiónSucursal.Service
{
    public class TipoSucursalService : ITipoSucursalService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<TipoSucursalDTO> _validator;

        public TipoSucursalService
        (
            IUnitOfWork uow, 
            IValidator<TipoSucursalDTO> validator
        )
        {
            _uow = uow;
            _validator = validator;
        }


        public void AgregarTipoSucursal(TipoSucursalDTO dto)
        {
            try
            {
                ValidarDto(dto);
                if(_uow.TipoSucursalRepository.ExistsByName(dto.Descripcion))
                    throw new TipoSucursalException($"Ya existe un tipo de sucursal con la descripción '{dto.Descripcion}'.");
                var entitie = TipoSucursalMapper.ToEntity(dto);
                entitie.IdTipoSucursal = Guid.NewGuid();

                _uow.TipoSucursalRepository.Add(entitie);
                _uow.SaveChanges();

            }
            catch(TipoSucursalException)
            {
                return; 
            }
            catch (Exception ex)
            {

                throw new TipoSucursalException("Error al agregar el tipo de sucursal.", ex);
            }
        }

        public void DeshabilitarTipoSucursal(Guid id)
        {
            throw new NotImplementedException();
        }
      
        public void HabilitarTipoSucursal(Guid id)
        {
            throw new NotImplementedException();
        }
      
        public void ModificarTipoSucursal(TipoSucursalDTO dto)
        {
            try
            {
                if (dto.Id == Guid.Empty)
                    throw new TipoSucursalException("El ID del tipo de sucursal es obligatorio.");

                ValidarDto(dto);

                var entity = _uow.TipoSucursalRepository.GetById(dto.Id);
                if (entity == null)
                    throw new TipoSucursalException("Tipo de sucursal no encontrada.");

                if (_uow.TipoSucursalRepository.ExistsByName(dto.Descripcion))
                    throw new TipoSucursalException("Ya existe un tipo de sucursal con ese nombre.");

                entity.Descripcion = dto.Descripcion; 

                _uow.TipoSucursalRepository.Update(entity);
                _uow.SaveChanges();
            }
            catch (TipoSucursalException)
            {
                return;
            }
            catch (Exception)
            {

                throw new TipoSucursalException("Error al modificar el tipo de sucursal.");
            }
        }

        public TipoSucursalDTO GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new TipoSucursalException("ID inválido.");

            var entity = _uow.TipoSucursalRepository.GetById(id);
            if (entity == null)
                throw new TipoSucursalException("Categoría no encontrada.");

            return TipoSucursalMapper.ToDTO(entity);
        }

        public List<TipoSucursalDTO> GetHabilitados()
        {
            return _uow.TipoSucursalRepository.GetAll()
                   .Select(TipoSucursalMapper.ToDTO)
                   .ToList();
        }

        #region Validaciones

        public void ValidarDto(TipoSucursalDTO dto)
        {
            if (dto == null)
                throw new TipoSucursalException("El objeto tipo sucursal no puede ser nulo.");

            if (_validator == null)
            {
                throw new TipoSucursalException("Error interno: El validador no fue inyectado correctamente.");
            }

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {

                throw new TipoSucursalException("Error interno al validar el tipo de sucursal.", ex);
            }

            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                               ?? "Error de validación desconocido.";

                throw new TipoSucursalException(primerError);
            }
        }

        #endregion
    }
}
