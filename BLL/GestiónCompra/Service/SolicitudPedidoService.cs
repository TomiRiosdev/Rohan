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
                ValidarDto(dto);

                if (dto.IdSucursal == null || dto.IdSucursal == Guid.Empty)
                    throw new SolicitudPedidoServiceException("Error: La solicitud debe incluir una sucursal de origen válida.");
                if (dto.IdUsuario == null || dto.IdUsuario == Guid.Empty)
                    throw new SolicitudPedidoServiceException("Error: La solicitud debe incluir el usuario emisor.");

                int idEstadoInicial = (int)EstadoSolicitudEnum.Pendiente;

                var entity = dto.ToEntity();  
                entity.IdSolicitudPedido = Guid.NewGuid();
                entity.FechaSolicitud = DateTime.Now;
                entity.IdEstadoSolicitud = idEstadoInicial; 

       
                int contadorRenglon = 1;
                foreach (var detalle in entity.SolicitudPedidoDetalle)
                {
                    detalle.IdSolicitudPedido = entity.IdSolicitudPedido;
                    detalle.Renglon = contadorRenglon;
                    contadorRenglon++;                 
                }

                _uow.SolicitudPedidoRepository.Add(entity);
                _uow.SaveChanges();
            }
            catch (SolicitudPedidoServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error interno al intentar registrar la Solicitud de Pedido.", ex);
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
                if (idSucursal == Guid.Empty)
                    throw new SolicitudPedidoServiceException("No se puede cargar el historial: El identificador de la sucursal es inválido.");

                // Consumimos la DAL
                var historial = _uow.SolicitudPedidoRepository.GetBySucursal(idSucursal);

                // Convertimos la lista de entidades a DTOs usando LINQ directo sobre tu Mapper
                return historial.Select(s => s.ToDTO()).ToList();
            }
            catch (SolicitudPedidoServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error crítico al recuperar el listado histórico de solicitudes.", ex);
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
