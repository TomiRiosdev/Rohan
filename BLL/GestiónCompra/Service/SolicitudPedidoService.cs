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
                if (dto.Detalles == null || !dto.Detalles.Any())
                    throw new SolicitudPedidoServiceException("No se puede generar una solicitud de pedido sin renglones de productos.");
                
         

                Guid idSolicitudNuevo = Guid.NewGuid(); 

                dto.IdSolicitudPedido = idSolicitudNuevo;
                dto.FechaSolicitud = DateTime.Now;
                dto.IdEstadoSolicitud = 1;
                int ultimoNro = _uow.SolicitudPedidoRepository.GetNextNroSolicitud(dto.IdSucursal.Value);
                dto.NroSolicitud = ultimoNro + 1;
              
                var entity = SolicitudPedidoMapper.ToEntity(dto);

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

        public List<SolicitudPedidoDetalleDTO> GenerarDetallesSugeridosBajoMinimo(Guid idSucursal)
        {
            try
            {
                // 1. Consumimos el consolidado de stock que ya tenés programado en tu módulo de Stock
                var stockConsolidado = _uow.StockPorSucursalRepository.GetConsolidadoBySucursal(idSucursal);

                var detallesSugeridos = new List<SolicitudPedidoDetalleDTO>();
                int renglonContador = 1;

                // 2. Filtramos únicamente los productos en quiebre o bajo el mínimo operativo
                var productosBajoMinimo = stockConsolidado
                        .Where(s => s.CantidadTotal <= s.StockMinimo && s.StockMaximo > s.CantidadTotal)
                        .GroupBy(s => s.IdProducto) 
                        .Select(g => g.First())    
                        .ToList();

                foreach (var prod in productosBajoMinimo)
                {
                    // Calcular faltantes en unidades sueltas
                    int unidadesFaltantes = (prod.StockMaximo ?? 0) - (prod.CantidadTotal ?? 0);
                    int unidadesPorBulto = prod.IdProductoNavigation?.CantidadPorBulto ?? 1;

                    // Convertimos las unidades sueltas que faltan a Bultos Cerrados (redondeando hacia arriba)
                    int bultosAPedir = (int)Math.Ceiling((double)unidadesFaltantes / unidadesPorBulto);

                    if (bultosAPedir > 0)
                    {
                        detallesSugeridos.Add(new SolicitudPedidoDetalleDTO
                        {
                            IdProducto = prod.IdProducto,
                            CodigoSku = prod.IdProductoNavigation?.CodigoSku ?? 0,
                            ProductoNombre = prod.IdProductoNavigation?.Nombre ?? "Materia Prima",
                            UnidadesPorBulto = unidadesPorBulto,
                            CantidadBultosSolicitada = bultosAPedir,
                            PresentacionTipo = "Caja",
                            Renglon = renglonContador
                        });

                        renglonContador++;
                    }
                }

                return detallesSugeridos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al precalcular la sugerencia automática de pedido.", ex);
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
