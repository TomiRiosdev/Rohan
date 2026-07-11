using DAO.Interface.GestionAuditoria;
using DAO.Interface.GestionCompra;
using DAO.Interface.GestionProducto;
using DAO.Interface.GestionProveedor;
using DAO.Interface.GestionStock;
using DAO.Interface.GestionSucursal;
using System;

namespace DAO.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        // Métodos para exponer los repositorios específicos
        IProductoRepository ProductoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        IProveedorRepository ProveedorRepository { get; }
        IUnidadMedidaRepository UnidadMedidaRepository { get; }
        ISucursalRepository SucursalRepository { get; }
        ITipoSucursalRepository TipoSucursalRepository { get; }
        IStockRepository StockPorSucursalRepository { get; }
        IProductoProveedorRepository ProductoProveedorRepository { get; }
        ISolicitudPedidoRepository SolicitudPedidoRepository { get; }
        IMovimientosStockRepository MovimientosStockRepository { get; }
        ILoteRepository LoteRepository { get; }
        ITipoMovimientoRepository TipoMovimientoRepository { get; }
        IOrdenCompraRepository OrdenCompraRepository { get; }
        ICompraSolicitudQueryRepository CompraSolicitudQueryRepository { get; }
        IAuditoriaRepository AuditoriaRepository { get; }

        // Método para guardar los cambios en la base de datos

        void SaveChanges();
    }
}
