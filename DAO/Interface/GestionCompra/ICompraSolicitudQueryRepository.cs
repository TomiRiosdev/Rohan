using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface.GestionCompra
{
    /// <summary>
    /// Repositorio especializado en consultas optimizadas de lectura (ReadOnly) 
    /// y filtros cruzados entre el catálogo de productos, proveedores y solicitudes.
    /// </summary>
    public interface ICompraSolicitudQueryRepository
    {
        /// <summary>
        /// Evalúa en la base de datos de forma veloz si existen solicitudes en estado "Pendiente" (IdEstado = 1) 
        /// para una sucursal específica, optimizando las alertas visuales de la UI.
        /// </summary>
        /// <param name="idSucursal">Identificador unívoco de la sucursal activa.</param>
        /// <returns>True si hay al menos un documento pendiente de unificación; de lo contrario, False.</returns>
        bool HaySolicitudesPendientesPorSucursal(Guid idSucursal);

        /// <summary>
        /// Recupera el listado completo de Solicitudes de Pedido con estado "Pendiente" (IdEstado = 1) 
        /// pertenecientes a una sucursal, cargando sus renglones para la Mesa de Entradas.
        /// </summary>
        /// <param name="idSucursal">Identificador unívoco de la sucursal emisora.</param>
        /// <returns>Colección diferida de entidades SolicitudPedido.</returns>
        IEnumerable<SolicitudPedido> ObtenerSolicitudesPendientesPorSucursal(Guid idSucursal);

        /// <summary>
        /// Obtiene el catálogo de artículos que comercializa un proveedor específico. 
        /// Se utiliza para poblar los combos en la carga manual de Órdenes de Compra.
        /// </summary>
        /// <param name="idProveedor">Identificador del proveedor seleccionado.</param>
        /// <returns>Colección de productos vinculados al proveedor.</returns>
        IEnumerable<Producto> ObtenerProductosPorProveedor(Guid idProveedor);

        /// <summary>
        /// Obtiene la lista de proveedores capaces de abastecer un producto determinado.
        /// </summary>
        /// <param name="idProducto">Identificador de la materia prima o artículo.</param>
        /// <returns>Colección de proveedores asociados.</returns>     
        IEnumerable<Proveedor> ObtenerProveedoresPorProducto(Guid idProducto);
        
        /// <summary>
        /// Consulta la tabla intermedia ProductoProveedor para extraer el costo de compra actual 
        /// y validar el Proveedor marcado como prioritario ("Principal") para el motor de compras automáticas.
        /// </summary>
        /// <param name="idProducto">Identificador del producto a desglosar.</param>
        /// <returns>La entidad de relación ProductoProveedor con sus propiedades de navegación resueltas.</returns>
        ProductoProveedor ObtenerRelacionProveedorPrincipal(Guid idProducto);
    }
}
