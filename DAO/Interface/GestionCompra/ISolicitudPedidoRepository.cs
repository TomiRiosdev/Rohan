using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface.GestionCompra
{
    public interface ISolicitudPedidoRepository
    {
        // 1. Persistencia de la cabecera (EF se encarga de insertar los detalles en cascada si la entidad los contiene)
        void Add(SolicitudPedido entity);

        // 2. Buscar una solicitud puntual trayendo OBLIGATORIAMENTE sus renglones (Maestro-Detalle)
        SolicitudPedido GetById(Guid idSolicitud);

        // 3. Recuperar el historial de solicitudes filtrado por el local actual del operador
        IEnumerable<SolicitudPedido> GetBySucursal(Guid idSucursal);

        // 4. Repositorio exclusivo para consultar los estados comerciales de la tabla EstadoSolicitud
        IEstadoSolicitudRepository Estados { get; }

        // 5. Obtener el siguiente número de solicitud para una sucursal específica
        int GetNextNroSolicitud(Guid idSucursal);
    }

    // Interfaz secundaria integrada para no llenar de archivos la DAL con tablas de soporte cortas
    public interface IEstadoSolicitudRepository
    {
        IEnumerable<EstadoSolicitud> GetAll();
        EstadoSolicitud GetByDescripcion(string descripcion);
    }
}
