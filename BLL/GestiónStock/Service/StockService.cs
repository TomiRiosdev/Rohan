using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónStock.Exceptions;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Mapper;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;
using Models;


namespace BLL.GestiónStock.Service
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<StockPorSucursalDTO> _validator;
        private readonly IKardexService _kardex; 

        public StockService
        (
            IUnitOfWork uow,
            IValidator<StockPorSucursalDTO> validator,
            IKardexService kardex
        )
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _kardex = kardex ?? throw new ArgumentNullException(nameof(kardex));
        }

        #region Métodos Públicos
        public void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal)
        {
            try
            {
                // 1. Validaciones de FluentValidation
                var validacion = _validator.Validate(stockDto);
                if (!validacion.IsValid) throw new ValidationException(validacion.Errors);

                if (idSucursal == Guid.Empty) throw new ArgumentException("Contexto de sucursal inválido.");

                // 2. Control de techo operativo (No superar el máximo)
                if (stockDto.CantidadTotal > stockDto.StockMaximo)
                    throw new Exception($"Operación abortada: La cantidad ingresada supera el límite máximo permitido ({stockDto.StockMaximo}) para este local.");

                // 3. Buscar si el producto ya tiene un registro consolidado en esta sucursal
                var stockDb = _uow.StockPorSucursalRepository.GetByIds(idSucursal, stockDto.IdProducto);

                int cantidadFisicaNueva = stockDto.CantidadTotal;

                if (stockDb == null)
                {
                    // 1. Convertimos el DTO a Entidad pura usando tu método sin argumentos
                    var nuevaEntity = stockDto.ToEntity();

                    // 2. Le asignamos la sucursal de contexto obligatoria de forma explícita
                    nuevaEntity.IdSucursal = idSucursal;

                    // 3. Lo pasamos al repositorio de la DAL
                    _uow.StockPorSucursalRepository.Add(nuevaEntity);
                }

                // 4. Creación física del Lote de resguardo basado en tu entidad Lote
                var nuevoLote = new Lote
                {
                    IdLote = Guid.NewGuid(),
                    IdProducto = stockDto.IdProducto,
                    IdSucursal = idSucursal,
                    CantidadInicial = cantidadFisicaNueva,
                    CantidadActual = cantidadFisicaNueva,
                    CostoUnitario = 0, // Ajuste manual arranca en costo cero o base
                    FechaIngreso = DateTime.Now,
                    NumeroLote = $"MAN-{DateTime.Now:yyyyMMddHHmmss}" // Código secuencial alfanumérico
                };

                _uow.LoteRepository.Add(nuevoLote);

                // 5. INVOCAMOS AL KARDEX (Auditoría cruzada gracias al patrón SRP)
                _kardex.RegistrarMovimiento(
                    idSucursal,
                    nuevoLote.IdLote,
                    TipoMovimientoEnum.IngresoManual,
                    cantidadFisicaNueva,
                    $"Ajuste manual de stock en góndola/depósito. Lote: {nuevoLote.NumeroLote}"
                );

                // 6. El Unit of Work impacta de forma atómica en SQL Server
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error crítico en BLL al procesar el ajuste manual de inventario.", ex);
            }
        }

        public void RegistrarStockPorOc(Guid idProducto, int cantidadComprada, decimal costoPactado, string nroRemitoOc, Guid idSucursal)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal)
        {
            if (idSucursal == Guid.Empty) return Enumerable.Empty<StockPorSucursalDTO>();

            var lista = _uow.StockPorSucursalRepository.GetConsolidadoBySucursal(idSucursal);
            // Mapeamos usando tu StockMapper de extensión
            return lista.Select(s => s.ToDTO());
        }

        #endregion

        #region Métodos Privados
        private void ValidarDto(StockPorSucursalDTO dto)
        {
            if (dto == null)
                throw new StockPorSucursalServiceException("El objeto stock no puede ser nulo.");

            if (_validator == null)
                throw new StockPorSucursalServiceException("Error interno: El validador de stock no fue inyectado correctamente.");

            ValidationResult validationResult;

            try
            {
                validationResult = _validator.Validate(dto);
            }
            catch (Exception ex)
            {
                throw new StockPorSucursalServiceException("Error interno al validar el stock.", ex);
            }

            if (!validationResult.IsValid)
            {
                var primerError = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                                  ?? "Error de validación desconocido.";

                throw new StockPorSucursalServiceException(primerError);
            }
        }

        #endregion
    }
}
