using DAO.Interface.GestionCompra;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace DAO.Implementations.SQLServer.GestionCompra
{
    public class CompraSolicitudQueryRepository : ICompraSolicitudQueryRepository
    {
        private readonly RohanContext _dbContext;
        public CompraSolicitudQueryRepository
        (
            RohanContext dbContext
        )
        {
            _dbContext = dbContext;
        }
        public bool HaySolicitudesPendientes()
        {
            try
            {
                // Estado 1 = Pendiente
                return _dbContext.SolicitudPedido.Any(s => s.IdEstadoSolicitud == 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la DAL al verificar alertas de solicitudes pendientes.", ex);
            }
        }

        public IEnumerable<Producto> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            try
            {
                return _dbContext.Set<ProductoProveedor>() 
                    .Where(pp => pp.IdProveedor == idProveedor)
                    .Include(pp => pp.IdProductoNavigation)
                    .Select(pp => pp.IdProductoNavigation)
                    .Where(p => p != null) // Limpieza de nulos por precaución
                    .ToList()!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al recuperar el catálogo de productos asociados al proveedor {idProveedor}.", ex);
            }
        }

        public IEnumerable<Proveedor> ObtenerProveedoresPorProducto(Guid idProducto)
        {
            try
            {
                return _dbContext.Set<ProductoProveedor>()
                    .Where(pp => pp.IdProducto == idProducto)
                    .Include(pp => pp.IdProveedorNavigation)
                    .Select(pp => pp.IdProveedorNavigation)
                    .Where(p => p != null)
                    .ToList()!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al recuperar el listado de proveedores alternativos para el producto {idProducto}.", ex);
            }
        }

        public IEnumerable<SolicitudPedido> ObtenerSolicitudesPendientes()
        {
            try
            {
                // Trae el maestro-detalle de las solicitudes estancadas en stock listas para procesar en compras
                return _dbContext.SolicitudPedido
                    .Include(s => s.IdEstadoSolicitudNavigation)
                    .Include(s => s.SolicitudPedidoDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
                    .Where(s => s.IdEstadoSolicitud == 1)
                    .OrderBy(s => s.FechaSolicitud)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la DAL al recuperar el listado de solicitudes pendientes.", ex);
            }
        }
    }
}
