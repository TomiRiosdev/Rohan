using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónStock.Exceptions;
using BLL.GestiónStock.Interface;
using BLL.GestiónStock.Mapper;
using DAO.Interface;
using Models;

namespace BLL.GestiónStock
{
    public class KardexService : IKardexService
    {
        private readonly IUnitOfWork _uow;

        public KardexService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Registra la fila histórica de auditoría en la tabla de Movimientos de Stock (Kardex) 
        /// y realiza un proceso de inserción o actualización (Upsert) en el stock consolidado para garantizar 
        /// que el producto tenga un único registro físico por sucursal en la base de datos de Rohan.
        /// </summary>
        /// <param name="idSucursal">Guid de la sucursal operada.</param>
        /// <param name="idLote">Guid del lote de trazabilidad recién creado o afectado.</param>
        /// <param name="tipo">Enum que identifica si la operación es ingreso, venta o merma.</param>
        /// <param name="cantidad">Unidades individuales netas procesadas.</param>
        /// <param name="observaciones">Justificación o texto descriptivo del movimiento.</param>
        /// <exception cref="StockDomainException">Lanzada si falla el mapeo del tipo de movimiento o la transacción SQL Server aborta.</exception>
        public void RegistrarMovimiento(Guid idSucursal, Lote lote, TipoMovimientoEnum tipo, int cantidad, string observaciones)
        {
            try
            {
               
                // 1. Resolución robusta de tablas maestras contra el Enum de negocio
                var tipoMovimientoDb = ObtenerTipoMovimiento(tipo);

                if (lote == null)
                    throw new StockDomainException("Falla de Consistencia Logística: El objeto lote provisto es nulo.");

                // 2. Grabar Fila Histórica Imborrable en el Kardex (Auditoría)
                CrearHistorialMovimiento(idSucursal, lote.IdLote, tipoMovimientoDb.IdTipoMovimiento, cantidad, observaciones);

                // 3. Resolver Álgebra Logística: Suma o Resta según descripción del movimiento
                int cambioNetoFisico = CalcularCambioFisico(tipoMovimientoDb.Descripcion, cantidad);

                // 4. Lógica de Unicidad Industrial (Upsert Consolidado)
                UpsertStockConsolidado(idSucursal, lote.IdProducto.Value, cambioNetoFisico);

                // 5. IMPACTO ATÓMICO FINAL EN LA BASE DE DATOS SQL SERVER
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new StockDomainException($"Error crítico e irreversible en el motor transaccional del Kardex para el movimiento [{tipo}]. Operación abortada.", ex);
            }
        }

        #region Métodos Privados de Responsabilidad Única (SRP)

        private TipoMovimiento ObtenerTipoMovimiento(TipoMovimientoEnum tipo)
        {
            string tipoEnumStr = tipo.ToString().ToLower().Replace(" ", "");

            var tipoMovimientoDb = _uow.TipoMovimientoRepository.GetAll()
                .ToList() // Descarga en memoria para blindar búsquedas complejas de strings
                .FirstOrDefault(tm => tm.Descripcion.ToLower().Replace(" ", "").Contains(tipoEnumStr)
                                   || ((int)tipo == tm.IdTipoMovimiento));

            if (tipoMovimientoDb == null)
                throw new StockDomainException($"Falla de Configuración de Sistema: El tipo de movimiento '{tipo}' no está registrado en las tablas relacionales de la base de datos.");

            return tipoMovimientoDb;
        }

        private void CrearHistorialMovimiento(Guid idSucursal, Guid idLote, int idTipoMovimiento, int cantidad, string observaciones)
        {
            var movimiento = new MovimientosStock
            {
                IdMovimiento = Guid.NewGuid(),
                IdSucursal = idSucursal,
                IdLote = idLote,
                IdTipoMovimiento = idTipoMovimiento,
                Cantidad = cantidad,
                FechaMovimiento = DateTime.Now,
                Observaciones = observaciones
            };

            _uow.MovimientosStockRepository.Add(movimiento);
        }

        private int CalcularCambioFisico(string descripcionTipo, int cantidad)
        {
            string textoDesc = descripcionTipo.ToLower();
            int multiplicadorEfectivo = 1;

            // Si el texto descriptivo mapea una salida, invertimos el signo algebraico
            if (textoDesc.Contains("egreso") || textoDesc.Contains("merma") || textoDesc.Contains("rotura") || textoDesc.Contains("venta"))
            {
                multiplicadorEfectivo = -1;
            }

            return cantidad * multiplicadorEfectivo;
        }

        private void UpsertStockConsolidado(Guid idSucursal, Guid idProducto, int cambioNetoFisico)
        {
            // Buscamos si el panificado ya tiene asignada una fila única en este depósito
            var stockExistente = _uow.StockPorSucursalRepository.GetAll()
                .FirstOrDefault(s => s.IdSucursal == idSucursal && s.IdProducto == idProducto);

            if (stockExistente != null)
            {
                // CASO A: UPDATE -> El producto ya existe, acumulamos sobre su CantidadTotal única
                stockExistente.CantidadTotal += cambioNetoFisico;

                // Blindaje absoluto de seguridad operativa: El stock físico real no puede ser menor a cero
                if (stockExistente.CantidadTotal < 0) stockExistente.CantidadTotal = 0;

                _uow.StockPorSucursalRepository.Update(stockExistente);
            }
            else
            {
                // CASO B: INSERT -> Primera vez que este producto pisa esta sucursal, creamos su celda relacional
                var nuevoStock = new StockPorSucursal
                {
                    IdStockPorSucursal = Guid.NewGuid(),
                    IdSucursal = idSucursal,
                    IdProducto = idProducto,
                    CantidadTotal = cambioNetoFisico < 0 ? 0 : cambioNetoFisico,
                    StockMinimo = 0,    // Nace libre para parametrización individual en tu pop-up
                    StockMaximo = 10, // Techo estándar base
                    
                };

                _uow.StockPorSucursalRepository.Add(nuevoStock);
            }
        }

        /// <summary>
        /// Recupera el historial cronológico de auditoría (Kardex) para una sucursal específica
        /// dentro de un rango de fechas. Traduce las entidades de persistencia a DTOs enriquecidos para la UI.
        /// </summary>
        /// <param name="idSucursal">Guid de la sucursal activa de la sesión.</param>
        /// <param name="desde">Fecha de inicio del filtro analítico.</param>
        /// <param name="hasta">Fecha de fin del filtro analítico.</param>
        /// <returns>Colección de movimientos listos para ser renderizados en la grilla de auditoría.</returns>
        /// <exception cref="StockDomainException">Lanzada si ocurre una falla en la consulta física contra SQL Server.</exception>
        public IEnumerable<MovimientoStockDTO> ObtenerHistorial(Guid idSucursal, DateTime desde, DateTime hasta)
        {
            try
            {
                // 1. Validación de frontera de datos
                if (idSucursal == Guid.Empty)
                    throw new ArgumentException("El identificador de la sucursal provisto es inválido.");

                // Ajuste preventivo: Nos aseguramos de que 'hasta' cubra las 23:59:59 de ese día por si el operario cargó algo a la tarde
                DateTime hastaFinDeDia = hasta.Date.AddDays(1).AddTicks(-1);

                // 2. Consulta directa a la DAL a través de la Unidad de Trabajo
                var entidades = _uow.MovimientosStockRepository.GetHistorial(idSucursal, desde, hastaFinDeDia);

                // 3. Transformación fluida usando el método de extensión de tu MovimientoStockMapper
                return entidades.ToDTOList();
            }
            catch (ArgumentException ex)
            {
                throw new StockDomainException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new StockDomainException("Error crítico de infraestructura al intentar consultar el libro contable de stock (Kardex).", ex);
            }
        }

        #endregion
    }
}

