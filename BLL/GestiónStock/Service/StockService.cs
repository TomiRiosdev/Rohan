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

                // 1. Validación Sintáctica con FluentValidation
                var validacion = _validator.Validate(stockDto);
                if (!validacion.IsValid)
                {
                    var primerError = validacion.Errors.First().ErrorMessage;
                    throw new StockValidationException(primerError);
                }

                if (idSucursal == Guid.Empty)
                    throw new StockDomainException("El contexto de la sucursal provisto es inválido.");
 
                TipoMovimientoEnum tipoMovimiento = (TipoMovimientoEnum)stockDto.IdTipoMovimiento;

                // Determinar el impacto matemático en el stock consolidado de la sucursal
                // Si es merma, egreso, o venta, resta. Si es ingreso, suma.
                int factorImpacto = 1;
                if (tipoMovimiento == TipoMovimientoEnum.EgresoPorMerma)
                {
                    factorImpacto = -1;
                }

                int cantidadFisicaNueva = stockDto.CantidadTotal;
                int impactoStockConsolidado = cantidadFisicaNueva * factorImpacto;

                // 2. Control de Regla de Negocio (Techo Operativo) - Corregido el operador pipe |
                if (tipoMovimiento == TipoMovimientoEnum.IngresoManual && stockDto.CantidadTotal > stockDto.StockMaximo)
                    throw new TechoOperativoException(stockDto.StockMaximo, stockDto.CantidadTotal);

                // 3. Persistencia del Consolidado
                var stockDb = _uow.StockPorSucursalRepository.GetByIds(idSucursal, stockDto.IdProducto);

                if (stockDb == null)
                {
                    var nuevaEntity = stockDto.ToEntity();
                    nuevaEntity.IdSucursal = idSucursal;
                    // Si arranca de cero y es una merma, permitimos que quede en negativo o tirás excepción según tu negocio
                    nuevaEntity.CantidadTotal = impactoStockConsolidado;
                    _uow.StockPorSucursalRepository.Add(nuevaEntity);
                }
                else
                {
                    // Sólido: Suma si es ingreso, resta si es egreso/merma
                    stockDb.CantidadTotal += impactoStockConsolidado;
                }

                // 4. Creación del Lote formalizado
                var nuevoLote = new Lote
                {
                    IdLote = Guid.NewGuid(),
                    IdProducto = stockDto.IdProducto,
                    IdSucursal = idSucursal,
                    CantidadInicial = cantidadFisicaNueva,
                    CantidadActual = cantidadFisicaNueva,
                    CostoUnitario = stockDto.CostoUnitario, // Aprovechamos el campo real del DTO si lo tiene
                    FechaIngreso = DateTime.Now,
                    NumeroLote = string.IsNullOrEmpty(stockDto.NumeroLote)
                        ? $"MAN-{DateTime.Now:yyyyMMddHHmmss}"
                        : stockDto.NumeroLote
                };
                _uow.LoteRepository.Add(nuevoLote);

                // 5. Auditoría en el Kardex dinámica (Corregida con datos de la UI)
                // Pasamos el Enum de forma limpia, o el entero, según espere tu método RegistrarMovimiento
                string comentarioFinal = string.IsNullOrEmpty(stockDto.Observaciones)
                    ? $"Ajuste manual de stock ({tipoMovimiento}). Lote: {nuevoLote.NumeroLote}"
                    : stockDto.Observaciones;

                _kardex.RegistrarMovimiento(
                    idSucursal,
                    nuevoLote.IdLote,
                    tipoMovimiento, // <-- Dinámico según el combo
                    cantidadFisicaNueva,
                    comentarioFinal // <-- Captura el texto de la caja de comentarios
                );

                // Transaccionalidad Atómica en SQL Server
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
