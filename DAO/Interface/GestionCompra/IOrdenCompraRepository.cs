using Models;
using System;

namespace DAO.Interface.GestionCompra
{
    /// <summary>
    /// Repositorio de persistencia y gestión transaccional física para las Órdenes de Compra en SQL Server.
    /// </summary>
    public interface IOrdenCompraRepository
    {
        /// <summary>
        /// Registra una nueva Orden de Compra (Borrador o Emitida) junto con sus renglones en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad física de cabecera con su colección de detalles acoplada.</param>
        void Add(OrdenCompra entity);

        /// <summary>
        /// Actualiza los cambios de estado, montos totales o modificaciones en los renglones de una orden existente.
        /// </summary>
        /// <param name="entity">Entidad OrdenCompra modificada.</param>
        void Update(OrdenCompra entity);

        /// <summary>
        /// Recupera una Orden de Compra específica mediante su clave primaria.
        /// </summary>
        /// <param name="idOc">Guid identificador de la orden.</param>
        /// <param name="incluirDetalles">Indica si se deben incluir los detalles de la orden.</param>
        /// <returns>La entidad OrdenCompra o null si no se encuentra.</returns>
        OrdenCompra GetById(Guid idOc, bool incluirDetalles);

        /// <summary>
        /// Trae el historial de órdenes de compra filtrado estrictamente por el contexto de la sucursal activa,
        /// aplicando Eager Loading (Includes) para resolver proveedores, usuarios y artículos de un solo tiro en SQL.
        /// </summary>
        /// <param name="idSucursal">Identificador unívoco regional.</param>
        /// <param name="fechaDesde">Fecha de inicio del rango de búsqueda.</param>
        /// <param name="fechaHasta">Fecha de fin del rango de búsqueda.</param>
        /// <returns>Colección de Órdenes de Compra con sus grafos de objetos cargados.</returns>
        IEnumerable<OrdenCompra> GetHistorialConDetalles(Guid idSucursal,DateTime fechaDesde, DateTime fechaHasta);

        /// <summary>
        /// Consulta el historial de órdenes de compra filtrado por sucursal, proveedor y estado.
        /// </summary>
        /// <param name="idSucursal">Identificador unívoco de la sucursal.</param>
        /// <param name="idProveedor">Identificador del proveedor.</param>
        /// <param name="idEstado">Identificador del estado de la orden.</param>
        /// <returns>Colección de Órdenes de Compra que cumplen con los criterios de filtrado.</returns>
        IEnumerable<OrdenCompra> ConsultarHistorial(Guid idSucursal,Guid idProveedor, int idEstado);

        /// <summary>
        /// Consulta el contador secuencial máximo en las tablas físicas.
        /// </summary>
        /// <remarks> Seguirá usándose como fallback si falla el algoritmo no incremental de Timestamp.</remarks>
        /// <returns>El último número entero registrado o 0 si la tabla está vacía.</returns>
        int ObtenerUltimoNumeroOc();

        /// <summary>
        /// Consulta si el proveedor tiene órdenes activas (Borrador o Emitida) en la base de datos.
        /// </summary>
        /// <param name="idProveedor">Identificador del proveedor.</param>
        /// <returns>True si el proveedor tiene órdenes activas, False en caso contrario.</returns>
        bool TieneOrdenesActivas(Guid idProveedor);

        /// <summary>
        /// Elimina los renglones de detalle de la orden de compra en la base de datos.
        /// </summary>
        /// <param name="detalle">Colección de renglones de detalle a eliminar.</param>
        void RemoveDetalle(IEnumerable<OrdenCompraDetalle> detalle);


    }
}
