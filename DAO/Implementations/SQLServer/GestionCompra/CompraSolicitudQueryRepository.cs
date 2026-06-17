using DAO;
using DAO.Interface.GestionCompra;
using Microsoft.EntityFrameworkCore;
using Models; // Ajustá según tu namespace real de entidades
using System;


namespace Implementations.SQLServer.GestionCompra
{
    /// <summary>
    /// Repositorio especializado en consultas optimizadas de lectura (ReadOnly) 
    /// y filtros cruzados entre el catálogo de productos, proveedores y solicitudes.
    /// </summary>
    public class CompraSolicitudQueryRepository : ICompraSolicitudQueryRepository
    {
        private readonly RohanContext _dbContext;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CompraSolicitudQueryRepository"/>.
        /// </summary>
        /// <param name="dbContext">Contexto de datos de Entity Framework.</param>
        public CompraSolicitudQueryRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Evalúa en la base de datos de forma veloz si existen solicitudes en estado "Pendiente" (IdEstado = 1) 
        /// para una sucursal específica, optimizando las alertas visuales de la UI.
        /// </summary>
        public bool HaySolicitudesPendientesPorSucursal(Guid idSucursal)
        {
            try
            {
                return _dbContext.SolicitudPedido.Any(s => s.IdSucursal == idSucursal && s.IdEstadoSolicitud == 1);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al verificar alertas de solicitudes para la sucursal {idSucursal}.", ex);
            }
        }

        /// <summary>
        /// Obtiene el catálogo de artículos que comercializa un proveedor específico. 
        /// Se utiliza para poblar los combos en la carga manual de Órdenes de Compra.
        /// </summary>
        public IEnumerable<Producto> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            try
            {
                return _dbContext.Set<ProductoProveedor>()
                    .Where(pp => pp.IdProveedor == idProveedor)
                    .Include(pp => pp.IdProductoNavigation)
                    .Select(pp => pp.IdProductoNavigation)
                    .Where(p => p != null)
                    .AsNoTracking()
                    .ToList()!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al recuperar el catálogo de productos asociados al proveedor {idProveedor}.", ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de proveedores capaces de abastecer un producto determinado.
        /// </summary>
        public IEnumerable<Proveedor> ObtenerProveedoresPorProducto(Guid idProducto)
        {
            try
            {
                return _dbContext.Set<ProductoProveedor>()
                    .Where(pp => pp.IdProducto == idProducto)
                    .Include(pp => pp.IdProveedorNavigation)
                    .Select(pp => pp.IdProveedorNavigation)
                    .Where(p => p != null)
                    .AsNoTracking()
                    .ToList()!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al recuperar el listado de proveedores alternativos para el producto {idProducto}.", ex);
            }
        }

        /// <summary>
        /// Consulta la tabla intermedia ProductoProveedor para extraer el costo de compra actual 
        /// y validar el Proveedor marcado como prioritario ("Principal") para el motor de compras automáticas.
        /// </summary>
        public ProductoProveedor ObtenerRelacionProveedorPrincipal(Guid idProducto)
        {
            try
            {
                return _dbContext.Set<ProductoProveedor>()
                    .Include(pp => pp.IdProveedorNavigation)
                    .FirstOrDefault(pp => pp.IdProducto == idProducto && pp.EsProveedorPrincipal == true)!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al recuperar el proveedor principal para el producto {idProducto}.", ex);
            }
        }

        /// <summary>
        /// Recupera el listado completo de Solicitudes de Pedido con estado "Pendiente" (IdEstado = 1) 
        /// pertenecientes a una sucursal, cargando sus renglones para la Mesa de Entradas.
        /// </summary>
        public IEnumerable<SolicitudPedido> ObtenerSolicitudesPendientesPorSucursal(Guid idSucursal)
        {
            try
            {
                return _dbContext.SolicitudPedido
                    .Include(s => s.IdEstadoSolicitudNavigation)
                    .Include(s => s.SolicitudPedidoDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
                    .Where(s => s.IdEstadoSolicitud == 1 && s.IdSucursal == idSucursal)
                    .OrderBy(s => s.FechaSolicitud)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al recuperar el listado de solicitudes pendientes para la sucursal {idSucursal}.", ex);
            }
        }
    }
}
