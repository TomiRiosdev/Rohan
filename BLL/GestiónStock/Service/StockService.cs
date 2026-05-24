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
                ValidarDto(stockDto);

                // 1. Validación Sintáctica con tu nueva excepción tipada
                var validacion = _validator.Validate(stockDto);
                if (!validacion.IsValid)
                {
                    var primerError = validacion.Errors.First().ErrorMessage;
                    throw new StockValidationException(primerError);
                }

                if (idSucursal == Guid.Empty)
                    throw new StockDomainException("El contexto de la sucursal provisto es inválido.");

                // 2. Control de Regla de Negocio usando la excepción de Techo Operativo
                if (stockDto.CantidadTotal > stockDto.StockMaximo)
                    throw new TechoOperativoException(stockDto.StockMaximo | 0, stockDto.CantidadTotal);

                // 3. Persistencia del Consolidado
                var stockDb = _uow.StockPorSucursalRepository.GetByIds(idSucursal, stockDto.IdProducto);
                int cantidadFisicaNueva = stockDto.CantidadTotal;

                if (stockDb == null)
                {
                    var nuevaEntity = stockDto.ToEntity();
                    nuevaEntity.IdSucursal = idSucursal;
                    _uow.StockPorSucursalRepository.Add(nuevaEntity);
                }
                else
                {
                    stockDb.CantidadTotal += cantidadFisicaNueva;
                }

                // 4. Creación del Lote usando su repositorio específico formalizado de la DAL
                var nuevoLote = new Lote
                {
                    IdLote = Guid.NewGuid(),
                    IdProducto = stockDto.IdProducto,
                    IdSucursal = idSucursal,
                    CantidadInicial = cantidadFisicaNueva,
                    CantidadActual = cantidadFisicaNueva,
                    CostoUnitario = 0,
                    FechaIngreso = DateTime.Now,
                    NumeroLote = $"MAN-{DateTime.Now:yyyyMMddHHmmss}"
                };
                _uow.LoteRepository.Add(nuevoLote);

                // 5. Auditoría en el Kardex (Mantenemos SRP de Servicios)
                _kardex.RegistrarMovimiento(
                    idSucursal,
                    nuevoLote.IdLote,
                    TipoMovimientoEnum.IngresoManual,
                    cantidadFisicaNueva,
                    $"Ajuste manual de stock. Lote: {nuevoLote.NumeroLote}"
                );

                // Transaccionalidad Atómica
                _uow.SaveChanges();
            }
            catch (StockDomainException)
            {
                throw; 
            }
            catch (Exception ex)
            {
                throw new StockDomainException("Error crítico interno al procesar el ajuste de inventario en el servidor.", ex);
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
        private void ValidarDto(StockPorSucursalDTO stockDto)
        {
            if (stockDto == null)
                throw new StockValidationException("La estructura de los datos de stock no puede ser nula.");

            var validacion = _validator.Validate(stockDto);

            if (!validacion.IsValid)
            {
                var primerError = validacion.Errors.First().ErrorMessage;
                throw new StockValidationException(primerError);
            }
        }

        #endregion
    }
}
