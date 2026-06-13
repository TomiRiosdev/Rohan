using DAO.Interface.GestionCompra;
using Microsoft.EntityFrameworkCore;
using Models;
using System;




namespace DAO.Implementations.SQLServer.GestionCompra
{
    public class OrdenCompraRepository : IOrdenCompraRepository
    {
        private readonly RohanContext _dbContext;
        public OrdenCompraRepository
        (
            RohanContext dbContext
        )
        {
            _dbContext = dbContext;
        }
        public void Add(OrdenCompra entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                // Al agregar la cabecera, EF Core analiza 'OrdenCompraDetalle' y los pone en estado Added en cascada
                _dbContext.OrdenCompra.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error nativo en la DAL al registrar la Orden de Compra maestro-detalle.", ex);
            }
        }

        public IEnumerable<OrdenCompra> GetAll()
        {
            try
            {
                return _dbContext.OrdenCompra
                    .Include(o => o.IdProveedorNavigation)
                    .Include(o => o.IdEstadoSolicitudNavigation)
                    .OrderByDescending(o => o.FechaOc)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar el historial completo de Órdenes de Compra.", ex);
            }
        }

        public IEnumerable<OrdenCompra> GetByEstado(int idEstadoOc)
        {
            try
            {
                return _dbContext.OrdenCompra
                    .Include(o => o.IdProveedorNavigation)
                    .Include(o => o.IdEstadoSolicitudNavigation)
                    .Where(o => o.IdEstadoOc == idEstadoOc)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al filtrar Órdenes de Compra por estado {idEstadoOc}.", ex);
            }
        }

        public OrdenCompra GetById(Guid idOc)
        {
            try
            {
                // Traemos obligatoriamente el maestro con sus renglones y la información extendida del producto
                return _dbContext.OrdenCompra
                    .Include(o => o.IdProveedorNavigation)
                    .Include(o => o.IdEstadoSolicitudNavigation)
                    .Include(o => o.OrdenCompraDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
                    .FirstOrDefault(o => o.IdOrdenCompra == idOc)!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error nativo en la DAL al buscar la Orden de Compra ID: {idOc}.", ex);
            }
        }

        public IEnumerable<OrdenCompra> GetByProveedor(Guid idProveedor)
        {
            try
            {
                return _dbContext.OrdenCompra
                    .Include(o => o.IdProveedorNavigation)
                    .Include(o => o.IdEstadoSolicitudNavigation)
                    .Where(o => o.IdProveedor == idProveedor)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al filtrar Órdenes de Compra por Proveedor {idProveedor}.", ex);
            }
        }

        public int ObtenerUltimoNumeroOc()
        {
            try
            {
                // Buscamos el número secuencial más alto. Si la tabla está vacía devuelve 0 de forma segura.
                // Nota: Si en tu modelo el Nro de OC está en un campo autoincremental de SQL, este método devuelve 0 
                // y se delega al motor, de lo contrario usamos este calculador en la BLL.
                return _dbContext.OrdenCompra.Max(o => (int?)o.NroSolicitud) ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el correlativo secuencial de la Orden de Compra.", ex);
            }
        }

        public void Update(OrdenCompra entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbContext.OrdenCompra.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error nativo en la DAL al actualizar la Orden de Compra ID: {entity.IdOrdenCompra}.", ex);
            }
        }
    }
}
