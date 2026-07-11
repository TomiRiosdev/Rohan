using DAO;
using DAO.Interface.GestionCompra;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Implementations.SQLServer.GestionCompra
{
    /// <summary>
    /// Repositorio de persistencia y gestión transaccional física para las Órdenes de Compra en SQL Server.
    /// </summary>
    public class OrdenCompraRepository : IOrdenCompraRepository
    {
        private readonly RohanContext _dbContext;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OrdenCompraRepository"/>.
        /// </summary>
        /// <param name="dbContext">Contexto de datos de Entity Framework.</param>
        public OrdenCompraRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Registra una nueva Orden de Compra (Borrador o Emitida) junto con sus renglones en la base de datos.
        /// </summary>
        public void Add(OrdenCompra entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbContext.OrdenCompra.Add(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error nativo en la DAL al registrar la Orden de Compra maestro-detalle.", ex);
            }
        }

        /// <summary>
        /// Recupera una Orden de Compra específica mediante su clave primaria con todo su grafo cargado.
        /// </summary>
        public OrdenCompra GetById(Guid idOc, bool incluirDetalles)
        {
            try
            {
                // 1. Iniciamos la consulta base
                var query = _dbContext.OrdenCompra.AsQueryable();

                // 2. Si pide detalles, arma los JOINs
                if (incluirDetalles)
                {
                    query = query
                        .Include(o => o.IdProveedorNavigation)
                        .Include(o => o.IdEstadoSolicitudNavigation)
                        .Include(o => o.OrdenCompraDetalle)
                            .ThenInclude(d => d.IdProductoNavigation);
                }

                // 3. Ejecutamos la consulta final
                return query.FirstOrDefault(o => o.IdOrdenCompra == idOc)!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error nativo en la DAL al buscar la Orden de Compra ID: {idOc}.", ex);
            }
        }

        /// <summary>
        /// Actualiza los cambios de estado, montos totales o modificaciones en los renglones de una orden existente.
        /// </summary>
        public void Update(OrdenCompra entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbContext.OrdenCompra.Update(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error nativo en la DAL al actualizar la Orden de Compra ID: {entity.IdOrdenCompra}.", ex);
            }
        }

        /// <summary>
        /// Consulta el identificador secuencial o numérico más alto registrado en el sistema.
        /// </summary>
        public int ObtenerUltimoNumeroOc()
        {
            try
            {
                return _dbContext.OrdenCompra.Max(o => (int?)o.NroSolicitud) ?? 0;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al calcular el correlativo secuencial de la Orden de Compra.", ex);
            }
        }

        /// <summary>
        /// Trae el historial rico de órdenes de compra filtrado estrictamente por el contexto de la sucursal activa.
        /// </summary>

        public IEnumerable<OrdenCompra> GetHistorialConDetalles(Guid idSucursal, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _dbContext.Set<OrdenCompra>()
                    .Include(oc => oc.IdProveedorNavigation)      
                    .Include(oc => oc.IdEstadoSolicitudNavigation)
                    .Include(oc => oc.OrdenCompraDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
                    .Where(oc => oc.IdSucursal == idSucursal)
                    .OrderByDescending(oc => oc.FechaOc)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al recuperar el historial de órdenes de compra de la sucursal {idSucursal}.", ex);
            }
        }

        /// <summary>
        /// Trae el historial rico de órdenes de compra filtrado por el contexto de la sucursal activa, proveedor y estado específico.
        /// <exception cref="InvalidOperationException"></exception>
        public IEnumerable<OrdenCompra> ConsultarHistorial(Guid idSucursal, Guid idProveedor, int idEstado)
        {
            try
            {
                return _dbContext.Set<OrdenCompra>()
                    .Include(oc => oc.IdProveedorNavigation)
                    .Include(oc => oc.IdEstadoSolicitudNavigation)
                    .Include(oc => oc.OrdenCompraDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
                    .Where(oc => oc.IdSucursal == idSucursal && oc.IdProveedor == idProveedor && oc.IdEstadoOc == idEstado)
                    .OrderByDescending(oc => oc.FechaOc)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al consultar el historial de órdenes de compra de la sucursal {idSucursal}, proveedor {idProveedor} y estado {idEstado}.", ex);
            }
        }

        /// <summary>
        /// Consulta si el proveedor tiene órdenes activas (Borrador o Emitida) en la base de datos.
        /// </summary>
        /// <param name="idProveedor">Identificador del proveedor.</param>
        /// <returns>True si el proveedor tiene órdenes activas, False en caso contrario.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool TieneOrdenesActivas(Guid idProveedor)
        {
            try
            {
                return _dbContext.OrdenCompra.Any(oc =>
                         oc.IdProveedor == idProveedor &&
                        (oc.IdEstadoOc == 1 || oc.IdEstadoOc == 2));
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Error en la DAL al consultar el historial de órdenes de compra del proveedor {idProveedor}.", ex);
            }
         
        }

        /// <summary>
        /// Elimina los renglones de detalle de una orden de compra específica de la base de datos.
        /// </summary>
        /// <param name="detalle"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveDetalle(IEnumerable<OrdenCompraDetalle> detalle)
        {
            if (detalle == null || !detalle.Any()) return;

            try
            {
                // Le indicamos al DbContext que estos registros deben ser borrados (DELETE en SQL)
                _dbContext.RemoveRange(detalle);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error nativo en la DAL al intentar eliminar los renglones de la Orden de Compra.", ex);
            }
        }
    }
}