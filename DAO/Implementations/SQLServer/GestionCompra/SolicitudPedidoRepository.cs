using DAO;
using DAO.Interface.GestionCompra;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Implementations.SQLServer.GestionCompra
{
    /// <summary>
    /// Repositorio de persistencia para las Solicitudes de Pedido internas de las sucursales.
    /// </summary>
    public class SolicitudPedidoRepository : ISolicitudPedidoRepository
    {
        private readonly RohanContext _dbContext;

        /// <summary>
        /// Punto de acceso integrado para consultar los estados de las solicitudes.
        /// </summary>
        public IEstadoSolicitudRepository Estados { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SolicitudPedidoRepository"/>.
        /// </summary>
        /// <param name="dbContext">Contexto de datos de Entity Framework.</param>
        public SolicitudPedidoRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "El contexto no puede ser nulo.");
            Estados = new EstadoSolicitudRepository(_dbContext);
        }

        /// <summary>
        /// Inserta una Solicitud de Pedido en la base de datos de manera diferida.
        /// </summary>
        public void Add(SolicitudPedido entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbContext.SolicitudPedido.Add(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error nativo en el repositorio DAL al insertar el maestro-detalle de la solicitud.", ex);
            }
        }

        /// <summary>
        /// Buscar una solicitud puntual trayendo de forma obligatoria sus renglones (Maestro-Detalle).
        /// </summary>
        public SolicitudPedido GetById(Guid idSolicitud)
        {
            try
            {
                if (idSolicitud == Guid.Empty) throw new ArgumentException("ID de solicitud inválido.");

                return _dbContext.SolicitudPedido
                    .Include(s => s.SolicitudPedidoDetalle)
                        .ThenInclude(d => d.IdProductoNavigation)
                    .Include(s => s.IdEstadoSolicitudNavigation)
                    .FirstOrDefault(s => s.IdSolicitudPedido == idSolicitud)!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"DAO Error: Error al recuperar la solicitud de pedido {idSolicitud}.", ex);
            }
        }

        /// <summary>
        /// Recuperar el historial de solicitudes filtrado por el local actual del operador.
        /// </summary>
        public IEnumerable<SolicitudPedido> GetBySucursal(Guid idSucursal)
        {
            try
            {
                if (idSucursal == Guid.Empty) throw new ArgumentException("ID de sucursal inválido.");

                return _dbContext.SolicitudPedido
                    .Include(s => s.IdEstadoSolicitudNavigation)
                    .Where(s => s.IdSucursal == idSucursal)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"DAO Error: No se pudo listar el historial de solicitudes para la sucursal {idSucursal}.", ex);
            }
        }

        /// <summary>
        /// Obtener el siguiente número de solicitud para una sucursal específica.
        /// </summary>
        public int GetNextNroSolicitud(Guid idSucursal)
        {
            try
            {
                return _dbContext.SolicitudPedido
                    .Where(s => s.IdSucursal == idSucursal)
                    .Max(s => (int?)s.NroSolicitud) ?? 0;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en la DAL al calcular el correlativo de solicitudes para la sucursal {idSucursal}.", ex);
            }
        }

        /// <summary>
        /// Actualiza los campos de la solicitud, fundamentalmente para cambiar su estado.
        /// </summary>
        public void Update(SolicitudPedido entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                _dbContext.SolicitudPedido.Update(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error nativo en la DAL al actualizar la Solicitud de Pedido ID: {entity.IdSolicitudPedido}.", ex);
            }
        }
    }

    /// <summary>
    /// Repositorio secundario embebido para la lectura del diccionario de estados comerciales.
    /// </summary>
    public class EstadoSolicitudRepository : IEstadoSolicitudRepository
    {
        private readonly RohanContext _dbContext;

        public EstadoSolicitudRepository(RohanContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<EstadoSolicitud> GetAll()
        {
            return _dbContext.EstadoSolicitud.AsNoTracking().ToList();
        }

        public EstadoSolicitud GetByDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion)) return null!;

            return _dbContext.EstadoSolicitud
                .FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower())!;
        }
    }
}

