using BLL.GestiónProducto.Exceptions;
using BLL.GestiónProducto.Interface;
using BLL.GestiónProducto.Mapper;
using BLL.DomainDtos;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;


namespace BLL.GestiónProducto.Service
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<UnidadMedidaDTO> _validator;

        public UnidadMedidaService
        (
            IUnitOfWork uow,
            IValidator<UnidadMedidaDTO> validator
        )
        {
            _uow = uow;
            _validator = validator;
        }

        public void AgregarUnidadMedidad(UnidadMedidaDTO dto)
        {
            ValidarDto(dto);

           if (_uow.UnidadMedidaRepository.ExistsByName(dto.Descripcion))
                throw new UnidadMedidaServiceException("Ya existe una Unidad de Medida con nombre.");

            var entity = UnidadMedidaMapper.ToEntity(dto);
            entity.IdUnidadMedida = Guid.NewGuid();
            // entity.FechaCreacion = DateTime.UtcNow;
            // entity.Habilitado = true;

            _uow.UnidadMedidaRepository.Add(entity);
            _uow.SaveChanges();
        }

        public void DeshabilitarUnidadMedida(Guid id)
        {
            throw new NotImplementedException();  // IMPLEMENTAR CUANDO SE MODIFIQUE LA ENTIDAD PARA INCLUIR EL CAMPO HABILITADO
        }

        public UnidadMedidaDTO GetById(Guid id)
        {
            throw new NotImplementedException(); // IMPLEMENTAR CUANDO SE MODIFIQUE LA ENTIDAD PARA INCLUIR EL CAMPO HABILITADO
        }

        public List<UnidadMedidaDTO> GetHabilitados()
        {
            return _uow.UnidadMedidaRepository.GetAll()
                //.Where(c => c.Habilitado) // DESCOMENTAR CUANDO SE MODIFIQUE LA ENTIDAD PARA INCLUIR EL CAMPO HABILITADO
                .Select(UnidadMedidaMapper.ToDTO)
                .ToList();
        }

        public void HabilitarUnidadMedida(Guid id)
        {
            throw new NotImplementedException();  // IMPLEMENTAR CUANDO SE MODIFIQUE LA ENTIDAD PARA INCLUIR EL CAMPO HABILITADO
        }

        public void ModificarUnidadMedida(UnidadMedidaDTO dto)
        {
            if (dto.Id == Guid.Empty)
                throw new UnidadMedidaServiceException("El ID de la unidad medida es obligatorio.");

            ValidarDto(dto);

            var entity = _uow.UnidadMedidaRepository.GetById(dto.Id);
            if (entity == null)
                throw new UnidadMedidaServiceException("Unidad medida no encontrada.");

            if (_uow.UnidadMedidaRepository.GetAll().Any(c => c.Descripcion == dto.Descripcion))
                throw new UnidadMedidaServiceException("Ya existe una Unidad de Medida con ese nombre.");

            UnidadMedidaMapper.UpdateEntity(entity, dto);
            //entity.FechaModificacion = DateTime.UtcNow;

            _uow.UnidadMedidaRepository.Update(entity);
            _uow.SaveChanges();
        }


        #region Validación

        private void ValidarDto(UnidadMedidaDTO dto)
        {
            if (dto == null)
                throw new UnidadMedidaServiceException("El objeto Unidad de Medida no puede ser nulo.");

            if (_validator == null)
            {
                throw new UnidadMedidaServiceException("Error interno: El validador no fue inyectado correctamente.");
            }

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {

                throw new UnidadMedidaServiceException("Error interno al validar la unidad de medida.", ex);
            }

            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                               ?? "Error de validación desconocido.";

                throw new UnidadMedidaServiceException(primerError);
            }
        }

        #endregion
    }
}
