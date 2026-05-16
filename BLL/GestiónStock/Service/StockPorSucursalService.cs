using BLL.DomainDtos;
using BLL.GestiónStock.Exceptions;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Mapper;
using DAO.Interface;
using FluentValidation;
using FluentValidation.Results;
using Models;
using Service.Facade;


namespace BLL.GestiónStock.Service
{
    public class StockPorSucursalService : IStockPorSucursalService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<StockPorSucursalDTO> _validator;

        public StockPorSucursalService(IUnitOfWork uow, IValidator<StockPorSucursalDTO> validator)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        #region Métodos Públicos
        public void RegistrarStockManual(StockPorSucursalDTO stockDto)
        {
            try
            {
                // 1. Validaciones sintácticas utilizando tu método estándar
                ValidarDto(stockDto);

                // 2. Control de Contexto desde la capa Service
                Guid idSucursalContexto = SessionManager.Current.IdSucursalActual
                    ?? throw new StockPorSucursalServiceException("Error de seguridad: No se detectó una sucursal activa en la sesión.");

                // 3. Validar existencia del producto en el catálogo
                var producto = _uow.ProductoRepository.GetById(stockDto.IdProducto);
                if (producto == null || !producto.Habilitado == false)
                    throw new StockPorSucursalServiceException("El producto seleccionado no existe o está deshabilitado.");

                // 4. Regla de Consistencia de negocio
                if (stockDto.CantidadTotal > stockDto.StockMaximo)
                    throw new StockPorSucursalServiceException($"Operación rechazada: La cantidad ({stockDto.CantidadTotal}) supera el Stock Máximo ({stockDto.StockMaximo}).");

                // 5. Buscar si existe el registro consolidado para determinar el flujo (Insert/Update)
                var stockExistente = _uow.StockPorSucursalRepository.GetByIds(idSucursalContexto, stockDto.IdProducto);

                int cantidadAnterior = 0;
                int cantidadFisicaLote = stockDto.CantidadTotal;

                if (stockExistente == null)
                {
                    // Flujo Nuevo: El producto no tiene historial en este local
                    var nuevaEntity = stockDto.ToEntity();
                    nuevaEntity.IdStockPorSucursal = Guid.NewGuid();
                    nuevaEntity.IdSucursal = idSucursalContexto; // Forzamos el contexto seguro

                    _uow.StockPorSucursalRepository.Add(nuevaEntity);
                }
                else
                {
                    // Flujo Existente: EF se encarga del tracking al modificar sus propiedades
                    cantidadAnterior = stockExistente.CantidadTotal ?? 0;
                    cantidadFisicaLote = stockDto.CantidadTotal - cantidadAnterior;

                    stockExistente.UpdateEntity(stockDto);
                }

                // 6. Creación obligatoria del Lote para trazabilidad física
                var nuevoLote = new Lote
                {
                    IdLote = Guid.NewGuid(),
                    IdProducto = stockDto.IdProducto,
                    IdSucursal = idSucursalContexto,
                    CantidadInicial = cantidadFisicaLote,
                    CantidadActual = cantidadFisicaLote,
                    CostoUnitario = stockDto.CostoUnitario,
                    FechaIngreso = DateTime.Now,
                    NumeroLote = string.IsNullOrWhiteSpace(stockDto.NumeroLote)
                        ? $"MAN-{DateTime.Now:yyyyMMddHHmmss}"
                        : stockDto.NumeroLote
                };

                _uow.StockPorSucursalRepository.AddLote(nuevoLote);

                // 7. Persistencia Única Transaccional
                _uow.SaveChanges();
            }
            catch (StockPorSucursalServiceException)
            {
                throw; // Re-lanzamos errores controlados de negocio
            }
            catch (Exception ex)
            {
                throw new StockPorSucursalServiceException("Error interno al intentar procesar la actualización de stock.", ex);
            }
        }

        public IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursalActual()
        {
            try
            {
                // Obtenemos de forma aislada e incorruptible el contexto del operador
                Guid idSucursalContexto = SessionManager.Current.IdSucursalActual
                    ?? throw new StockPorSucursalServiceException("No se puede cargar el inventario: Falta el contexto de la sucursal activa.");

                var entidades = _uow.StockPorSucursalRepository.GetConsolidadoBySucursal(idSucursalContexto);

                // Transformamos la lista técnica de EF a DTOs planos para consumo seguro de los formularios
                return entidades.ToDTOList();
            }
            catch (StockPorSucursalServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new StockPorSucursalServiceException("Error crítico al recuperar el inventario consolidado del local.", ex);
            }
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
