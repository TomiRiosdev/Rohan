using BLL.GestiónProducto.Exceptions;
using BLL.GestiónProducto.Interface;
using BLL.GestiónProducto.Mapper;
using BLL.DomainDtos;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;

namespace BLL.GestiónProducto.Service
{
    public class CategoriaService :  ICategoriaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CategoriaDTO> _validator;

        public CategoriaService
        (
            IUnitOfWork uok,
            IValidator<CategoriaDTO> validator
        )
        {
            _uow = uok;
            _validator = validator;
        }

        public void AgregarCategoria(CategoriaDTO dto)
        {
            try
            {
                ValidarDto(dto);

                if (_uow.CategoriaRepository.ExistsByName(dto.Descripcion))
                    throw new CategoriaServiceException("Ya existe una categoría con ese nombre.");

                var entity = CategoriaMapper.ToEntity(dto);
                entity.IdCategoria = Guid.NewGuid();
                // entity.FechaCreacion = DateTime.UtcNow;
                // entity.Habilitado = true;

                _uow.CategoriaRepository.Add(entity);
                _uow.SaveChanges();
            }
            catch (CategoriaServiceException)
            {
                throw; // RE-LANZAR EXCEPCIONES DE SERVICIO SIN ENVOLVERLAS EN OTRA EXCEPCIÓN
            }
            catch (Exception ex)
            {

                throw new CategoriaServiceException("Error interno al agregar la categoría.", ex);
            }
        }
  
        public void DeshabilitarCategoria(Guid id)
        {
            throw new NotImplementedException(); // IMPLEMENTAR CUANDO SE MODIFIQUE LA ENTIDAD PARA INCLUIR EL CAMPO HABILITADO
        }

        public CategoriaDTO GetById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new CategoriaServiceException("ID inválido.");

                var entity = _uow.CategoriaRepository.GetById(id);
                if (entity == null)
                    throw new CategoriaServiceException("Categoría no encontrada.");

                return CategoriaMapper.ToDTO(entity);
            }
            catch (Exception ex)
            {

                throw new CategoriaServiceException("Error interno", ex);
            }
        }

        public List<CategoriaDTO> GetHabilitados()
        {
            return _uow.CategoriaRepository.GetAll()
               //.Where(c => c.Habilitado) // DESCOMENTAR CUANDO SE MODIFIQUE LA ENTIDAD PARA INCLUIR EL CAMPO HABILITADO
               .Select(CategoriaMapper.ToDTO)
               .ToList();
        }

        public void HabilitarCategoria(Guid id)
        {
            throw new NotImplementedException(); // IMPLEMENTAR CUANDO SE MODIFIQUE LA ENTIDAD PARA INCLUIR EL CAMPO HABILITADO
        }

        public void ModificarCategoria(CategoriaDTO dto)
        {
            try
            {
                if (dto.Id == Guid.Empty)
                    throw new CategoriaServiceException("El ID de la categoría es obligatorio.");

                ValidarDto(dto);

                var entity = _uow.CategoriaRepository.GetById(dto.Id);
                if (entity == null)
                    throw new CategoriaServiceException("Categoría no encontrada.");

                if (_uow.CategoriaRepository.GetAll().Any(c => c.Descripcion == dto.Descripcion))
                    throw new CategoriaServiceException("Ya existe una categoría con ese nombre.");

                CategoriaMapper.UpdateEntity(entity, dto);
                //entity.FechaModificacion = DateTime.UtcNow;

                _uow.CategoriaRepository.Update(entity);
                _uow.SaveChanges();
            }
            catch(CategoriaServiceException)
            {
                throw; // RE-LANZAR EXCEPCIONES DE SERVICIO SIN ENVOLVERLAS EN OTRA EXCEPCIÓN
            }
            catch (Exception ex)
            {

                throw new CategoriaServiceException("Error interno al modificar la categoría.", ex);
            }
        }

        #region Validación

        private void ValidarDto(CategoriaDTO dto)
        {
            if (dto == null)
                throw new CategoriaServiceException("El objeto Ctegoria no puede ser nulo.");

            if (_validator == null)
            {
                throw new CategoriaServiceException("Error interno: El validador no fue inyectado correctamente.");
            }

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {
               
                throw new CategoriaServiceException("Error interno al validar el producto.", ex);
            }
            
            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                               ?? "Error de validación desconocido.";
 
                throw new CategoriaServiceException(primerError);
            }
        }

        #endregion
    }
}
