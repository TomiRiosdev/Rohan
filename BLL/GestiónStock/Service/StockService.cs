using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónStock.Exceptions;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Mapper;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.GestiónStock
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

        /// <summary>
        /// Procesa un ajuste de inventario manual desde la UI. 
        /// Valida las reglas de negocio iniciales, calcula el equivalente en unidades individuales netas basándose 
        /// en el factor de bultos del maestro de productos, crea el lote físico y delega la auditoría al KardexService.
        /// </summary>
        /// <param name="stockDto">Datos de transferencia capturados en el formulario manual.</param>
        /// <param name="idSucursal">Identificador de la sucursal activa de la sesión.</param>
        /// <exception cref="StockValidationException">Lanzada si los datos del DTO fallan las reglas de FluentValidation.</exception>
        /// <exception cref="TechoOperativoException">Lanzada si el ingreso supera el límite máximo permitido para el producto.</exception>
        /// <exception cref="StockDomainException">Lanzada ante fallas críticas en el servidor o infraestructura de datos.</exception>
        public void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal)
        {
            try
            {
                // 1. Ejecutar Validaciones de Estructura e Interfaz
                ValidarDto(stockDto);

                if (idSucursal == Guid.Empty)
                    throw new StockValidationException("El contexto de la sucursal provisto es inválido o no se encuentra activo.");

                // 2. Normalización Logística: Traducir Bultos Cerrados a Unidades Sueltas Mínimas
                int unidadesIndividualesNetas = 0;
                if (stockDto.EsIngresoPorBulto)
                {
                    unidadesIndividualesNetas = stockDto.CantidadTotal * stockDto.CantidadPorBulto;
                }
                else
                {
                    unidadesIndividualesNetas = stockDto.CantidadTotal;
                }

                TipoMovimientoEnum tipoMovimiento = (TipoMovimientoEnum)stockDto.IdTipoMovimiento;

                // 3. Control Analítico de Regla de Negocio: Techo Operativo (Máximos de Resguardo)
                if (tipoMovimiento == TipoMovimientoEnum.IngresoManual && unidadesIndividualesNetas > stockDto.StockMaximo)
                {
                    throw new TechoOperativoException(stockDto.StockMaximo, unidadesIndividualesNetas);
                }

                // 4. Persistencia de la Trazabilidad: Creación del Lote Físico de Mercadería
                var nuevoLote = new Lote
                {
                    IdLote = Guid.NewGuid(),
                    IdProducto = stockDto.IdProducto,
                    IdSucursal = idSucursal,
                    CantidadInicial = unidadesIndividualesNetas,
                    CantidadActual = unidadesIndividualesNetas > 0 ? unidadesIndividualesNetas : 0,
                    CostoUnitario = stockDto.CostoUnitario,
                    FechaIngreso = DateTime.Now,
                    NumeroLote = string.IsNullOrEmpty(stockDto.NumeroLote)
                        ? $"MAN-{DateTime.Now:yyyyMMddHHmmss}"
                        : stockDto.NumeroLote
                };
                _uow.LoteRepository.Add(nuevoLote);

                // 5. Orquestación: Enviar el Lote y las Unidades Netas Calculadas al Motor de Auditoría
                string comentarioFinal = string.IsNullOrEmpty(stockDto.Observaciones)
                    ? $"Ajuste manual de stock ({tipoMovimiento}). Lote: {nuevoLote.NumeroLote}"
                    : stockDto.Observaciones;

                _kardex.RegistrarMovimiento(idSucursal, nuevoLote, tipoMovimiento, unidadesIndividualesNetas, comentarioFinal);

                // Nota: El SaveChanges se ejecuta al final de toda la cadena adentro del KardexService para mantener la atomicidad.
            }
            catch (RohanStockException)
            {
                throw; // Relanzamos nuestras excepciones controladas directas a la UI
            }
            catch (Exception ex)
            {
                throw new StockDomainException("Error crítico interno al orquestar el ajuste de inventario en el servidor.", ex);
            }
        }

        /// <summary>
        /// Registra el ingreso masivo de mercadería asociado a un documento comercial (Orden de Compra / Remito).
        /// </summary>
        public void RegistrarStockPorOc(Guid idProducto, int cantidadComprada, decimal costoPactado, string nroRemitoOc, Guid idSucursal)
        {
            throw new NotImplementedException("Próximo módulo: Se implementará en la integración con el subsistema de compras.");
        }

        /// <summary>
        /// Consulta la base de datos para recuperar la foto actual consolidada del inventario físico de una sucursal.
        /// Mapea los registros a DTOs enriquecidos con propiedades calculadas para el DataGridView.
        /// </summary>
        /// <param name="idSucursal">Guid de la sucursal a consultar.</param>
        /// <returns>Colección de DTOs listos para enlazar a la interfaz gráfica.</returns>
        public IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal)
        {
            try
            {
                if (idSucursal == Guid.Empty) return Enumerable.Empty<StockPorSucursalDTO>();

                var lista = _uow.StockPorSucursalRepository.GetConsolidadoBySucursal(idSucursal);

                // Mapeo utilizando tu método de extensión de forma fluida
                return lista.Select(s => s.ToDTO());
            }
            catch (Exception ex)
            {
                throw new StockDomainException("No se pudo recuperar el consolidado de stock debido a una falla en la consulta de persistencia.", ex);
            }
        }

        #region Métodos Privados Auxiliares

        private void ValidarDto(StockPorSucursalDTO stockDto)
        {
            if (stockDto == null)
                throw new StockValidationException("La estructura de los datos de transferencia de stock es nula.");

            ValidationResult validacion = _validator.Validate(stockDto);
            if (!validacion.IsValid)
            {
                var primerError = validacion.Errors.First().ErrorMessage;
                throw new StockValidationException(primerError);
            }
        }

        #endregion
    }
}
