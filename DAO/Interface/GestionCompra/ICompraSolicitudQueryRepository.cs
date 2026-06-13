using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface.GestionCompra
{
    public interface ICompraSolicitudQueryRepository
    {
        // Devuelve True si hay al menos una solicitud con Estado = 1 (Pendiente)
        bool HaySolicitudesPendientes();

        // Trae todas las solicitudes que están listas para ser transformadas en OC
        IEnumerable<SolicitudPedido> ObtenerSolicitudesPendientes();

        // Filtros cruzados para la carga manual del formulario
        IEnumerable<Producto> ObtenerProductosPorProveedor(Guid idProveedor);
        IEnumerable<Proveedor> ObtenerProveedoresPorProducto(Guid idProducto);
    }
}
