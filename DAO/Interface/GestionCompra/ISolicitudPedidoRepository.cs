using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface.GestionCompra
{
    /// <summary>
    /// Repositorio de persistencia para las Solicitudes de Pedido internas de las sucursales.
    /// </summary>
    public interface ISolicitudPedidoRepository
    {
        /// <summary>
        /// Inserta una Solicitud de Pedido en la base de datos. Al contener la lista de hijos, 
        /// Entity Framework insertará automáticamente los renglones en SolicitudPedidoDetalle de forma atómica.
        /// </summary>
        /// <param name="entity">Entidad cabecera SolicitudPedido.</param>
        void Add(SolicitudPedido entity);

        /// <summary>
        /// Recupera una solicitud puntual resolviendo de forma obligatoria e inmediata sus detalles (Eager Loading).
        /// Evita excepciones de Lazy Loading al renderizar grillas Maestro-Detalle.
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud.</param>
        /// <returns>Entidad SolicitudPedido con su colección de detalles cargada.</returns>
        SolicitudPedido GetById(Guid idSolicitud);

        /// <summary>
        /// Obtiene el historial de requerimientos internos originados exclusivamente por la sucursal indicada.
        /// </summary>
        /// <param name="idSucursal">Identificador unívoco de la sucursal de origen.</param>
        /// <returns>Colección diferida de solicitudes de la sede.</returns>
        IEnumerable<SolicitudPedido> GetBySucursal(Guid idSucursal);

        /// <summary>
        /// Punto de acceso integrado (Propiedad de navegación de repositorio) para consultar los estados 
        /// de las solicitudes sin crear un archivo de repositorio independiente para una tabla de diccionario.
        /// </summary>
        IEstadoSolicitudRepository Estados { get; }

        /// <summary>
        /// Consulta el último número secuencial de solicitud utilizado por una sucursal específica 
        /// para calcular el siguiente correlativo local (Ej: Pedido Sucursal 1 N° 45).
        /// </summary>
        /// <param name="idSucursal">Identificador de la sucursal de control.</param>
        /// <returns>El último número entero correlativo asignado a esa sucursal.</returns>
        int GetNextNroSolicitud(Guid idSucursal);

        /// <summary>
        /// Actualiza los campos de la solicitud, fundamentalmente para cambiar su estado (Pendiente -> Aprobada / Cancelada).
        /// </summary>
        /// <param name="entity">Entidad física SolicitudPedido a actualizar.</param>
        void Update(SolicitudPedido entity);
    }

    /// <summary>
    /// Repositorio secundario embebido para la lectura exclusiva del diccionario de estados comerciales (Pendiente, Aprobada, Cancelada).
    /// </summary>
    public interface IEstadoSolicitudRepository
    {
        /// <summary>
        /// Trae la lista completa de estados para rellenar combos de filtrado en la UI.
        /// </summary>
        IEnumerable<EstadoSolicitud> GetAll();

        /// <summary>
        /// Busca un estado puntual por su nombre exacto (útil para validaciones lógicas duras en la BLL).
        /// </summary>
        /// <param name="descripcion">Texto del estado (Ej: "Pendiente").</param>
        EstadoSolicitud GetByDescripcion(string descripcion);
    }
}
