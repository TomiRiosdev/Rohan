using Models;
using Microsoft.EntityFrameworkCore;
using DAO.Interface.GestionCompra;


namespace DAO.Implementations.SQLServer.GestionCompra
{
    public class SolicitudPedidoRepository : ISolicitudPedidoRepository
    {
        private readonly RohanContext _dbContext;
        public IEstadoSolicitudRepository Estados { get; private set; }

        public SolicitudPedidoRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "El contexto no puede ser nulo.");

            // Instanciamos el sub-repositorio compartiendo exactamente el mismo contexto transaccional
            Estados = new EstadoSolicitudRepository(_dbContext);
        }

        // 1. REGISTRAR UNA NUEVA SOLICITUD CON SUS RENGLONES
        public void Add(SolicitudPedido entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // EF es inteligente: al agregar la entidad cabecera, detecta la colección interna 
            // de 'SolicitudPedidoDetalles' y preparará los INSERTS para todas las tablas en la misma cola.
            _dbContext.SolicitudPedido.Add(entity);
        }

        // 2. OBTENER MAESTRO-DETALLE COMPLETO POR ID
        public SolicitudPedido GetById(Guid idSolicitud)
        {
            try
            {
                if (idSolicitud == Guid.Empty) throw new ArgumentException("ID de solicitud inválido.");

                return _dbContext.SolicitudPedido
                    // 1. Cargamos los renglones usando la nueva propiedad directa generada por EF
                    .Include(s => s.SolicitudPedidoDetalle)
                        // 2. De esos renglones, navegamos al producto asociado
                        .ThenInclude(d => d.IdProductoNavigation)

                    // 3. Cargamos la navegación del Estado (Revisá si EF lo nombró sin el "Id" adelante)
                    .Include(s => s.IdEstadoSolicitudNavigation)

                    .FirstOrDefault(s => s.IdSolicitudPedido == idSolicitud);
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Error al recuperar la solicitud de pedido {idSolicitud}.", ex);
            }
        }

        public IEnumerable<SolicitudPedido> GetBySucursal(Guid idSucursal)
        {
            try
            {
                if (idSucursal == Guid.Empty) throw new ArgumentException("ID de sucursal inválido.");

                return _dbContext.SolicitudPedido
                    // Incluimos el estado para que la grilla principal pueda mostrar "Pendiente", "Aprobada", etc.
                    .Include(s => s.IdEstadoSolicitudNavigation)
                    .Where(s => s.IdSucursal == idSucursal)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: No se pudo listar el historial de solicitudes para la sucursal {idSucursal}.", ex);
            }
        }
    }

    #region SUB-REPOSITORIO INTERNO PARA ESTADOS 
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
            if (string.IsNullOrWhiteSpace(descripcion)) return null;

            // Buscamos ignorando mayúsculas/minúsculas para blindar el matching con el Enum de la BLL
            return _dbContext.EstadoSolicitud
                .FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower());
        }
    }
    #endregion
}
