using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónCompra.Exceptions;
using BLL.GestiónCompra.Interface;
using BLL.GestiónCompra.Mapper;
using BLL.GestiónStock.Mapper;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;


namespace BLL.GestiónCompra.Service
{
    public class SolicitudPedidoService : ISolicitudPedidoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<SolicitudPedidoDTO> _validator;

        public SolicitudPedidoService
        (
            IUnitOfWork uow, 
            IValidator<SolicitudPedidoDTO> validator
        )
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        #region Métodos Públicos

        public void CrearSolicitud(SolicitudPedidoDTO dto)
        {
            try
            {
                // 1. Validaciones sintácticas (FluentValidation)
                ValidarDto(dto);

                // 2. Validaciones de contexto sobre los datos que ya vienen inyectados en el DTO desde la UI
                if (dto.IdSucursal == Guid.Empty)
                    throw new SolicitudPedidoServiceException("Error: La solicitud debe incluir una sucursal de origen válida.");
                if (dto.IdUsuario == Guid.Empty)
                    throw new SolicitudPedidoServiceException("Error: La solicitud debe incluir el usuario emisor.");

                // 3. Resolución automática del Estado Inicial (Enum)
                string estadoInicialStr = EstadoSolicitudEnum.Pendiente.ToString();
                var estadoDb = _uow.SolicitudPedidoRepository.Estados.GetByDescripcion(estadoInicialStr);

                if (estadoDb == null)
                    throw new SolicitudPedidoServiceException("Error de configuración: El estado inicial 'Pendiente' no existe.");

                // 4. Mapeo del DTO hacia la Entidad
                var entity = dto.ToEntity();

                // 5. Asignamos los datos validados
                entity.IdSolicitudPedido = Guid.NewGuid();
                entity.FechaSolicitud = DateTime.Now;
                entity.IdEstadoSolicitud = estadoDb.IdEstadoSolicitud;

                // 6. Asignación secuencial de los Renglones de los detalles...
                int contadorRenglon = 1;
                foreach (var detalle in entity.SolicitudPedidoDetalles)
                {
                    detalle.IdSolicitudPedido = entity.IdSolicitudPedido;
                    detalle.Renglon = contadorRenglon;
                    contadorRenglon++;

                    var prodDb = _uow.ProductoRepository.GetById(detalle.IdProducto ?? Guid.Empty);
                    if (prodDb == null || prodDb.Habilitado == false)
                        throw new SolicitudPedidoServiceException($"El producto en el renglón {detalle.Renglon} no es válido.");
                }

                _uow.SolicitudPedidoRepository.Add(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new SolicitudPedidoServiceException("Error interno al intentar registrar la Solicitud de Pedido.", ex);
            }
        }

        public SolicitudPedidoDTO ObtenerPorId(Guid idSolicitud)
        {
            try
            {
                var entity = _uow.SolicitudPedidoRepository.GetById(idSolicitud);
                if (entity == null)
                    throw new SolicitudPedidoServiceException("No se encontró la solicitud de pedido especificada.");

                return entity.ToDTO();
            }
            catch (SolicitudPedidoServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SolicitudPedidoServiceException("Error al recuperar el documento de solicitud.", ex);
            }
        }

        public IEnumerable<SolicitudPedidoDTO> ObtenerHistorialPorSucursal(Guid idSucursal)
        {
            try
            {
                // Validación de contexto sobre el parámetro que viene desde la UI
                if (idSucursal == Guid.Empty)
                    throw new SolicitudPedidoServiceException("No se puede cargar el historial: El identificador de la sucursal es inválido.");

                // Consumimos la DAL pasando directamente nuestro parámetro
                var historial = _uow.SolicitudPedidoRepository.GetBySucursal(idSucursal);
                return historial.ToDTOList();
            }
            catch (SolicitudPedidoServiceException)
            {
                throw; // Re-lanzamos el error controlado de negocio
            }
            catch (Exception ex)
            {
                throw new SolicitudPedidoServiceException("Error crítico al recuperar el listado histórico de solicitudes.", ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void ValidarDto(SolicitudPedidoDTO dto)
        {
            if (dto == null)
                throw new SolicitudPedidoServiceException("La estructura de la solicitud no puede ser nula.");

            if (_validator == null)
                throw new SolicitudPedidoServiceException("Error interno: El validador correspondiente no fue inyectado.");

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {
                throw new SolicitudPedidoServiceException("Error interno al procesar las validaciones del documento.", ex);
            }

            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                                  ?? "Error de validación desconocido.";

                throw new SolicitudPedidoServiceException(primerError);
            }
        }

        #endregion
    }
}
