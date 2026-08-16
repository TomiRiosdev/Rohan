using DAO.Interface.GestionStock;
using Microsoft.EntityFrameworkCore;
using Models;
using System;


namespace DAO.Implementations.SQLServer.GestionStock
{
    public class OrdenTraspasoRepository : IOrdenTraspasoRepository
    {
        private readonly RohanContext _dbContext;
      
        public OrdenTraspasoRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AddOrdenTraspaso(OrdenTraspaso entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbContext.OrdenTraspasos.Add(entity); // Verifica si tu DbSet se llama OrdenTraspasos u OrdenTraspaso
        }

        public OrdenTraspaso GetById(Guid id)
        {
            try
            {
                return _dbContext.OrdenTraspasos
                    .Include(o => o.IdSucursalOrigenNavigation)
                    .Include(o => o.IdSucursalDestinoNavigation)
                    .Include(o => o.IdEstadoSolicitudNavigation)
                    .Include(o => o.OrdenTraspasoDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
                    .Include(o => o.OrdenTraspasoDetalle)
                        .ThenInclude(d => d.IdLoteOrigenNavigation)
                    .FirstOrDefault(o => o.IdOrdenTraspaso == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al obtener el traspaso por Id: {id}.", ex);
            }
        }

        //  Lista las ordenes de traspaso pendientes de la sucursal origen,pendientes a confirmar o rechazar.
        public IEnumerable<OrdenTraspaso> GetTraspasosPendientes(Guid idSucursalOrigen)
        {
            try
            {
                return _dbContext.OrdenTraspasos
                    .Include(o => o.IdSucursalOrigenNavigation)
                    .Include(o => o.IdSucursalDestinoNavigation)
                    .Include(o => o.IdEstadoSolicitudNavigation)
                    .Include(o => o.OrdenTraspasoDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
                    .Where(o => o.IdSucursalOrigen == idSucursalOrigen && o.IdEstado == 1)
                    .OrderBy(o => o.FechaEmision)
                    .AsNoTracking() 
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al obtener los traspasos pendientes de la sucursal {idSucursalOrigen}.", ex);
            }
        }

        // Lista las ordenes de traspaso enviadas de la sucursal origen, que se visualizar en la sucursal destino.
        public IEnumerable<OrdenTraspaso> GetTraspasosEnviados(Guid idSucursalDestino)
        {
            try
            {
                return _dbContext.OrdenTraspasos
                    .Include(o => o.IdSucursalOrigenNavigation)
                    .Include(o => o.IdSucursalDestinoNavigation)
                    .Include(o => o.IdEstadoSolicitudNavigation)
                    .Include(o => o.OrdenTraspasoDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
   
                    .Where(o => o.IdSucursalDestino == idSucursalDestino && o.IdEstado == 5)
                    .OrderBy(o => o.FechaEmision)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al obtener los traspasos enviados hacia la sucursal {idSucursalDestino}.", ex);
            }
        }
    }
}
