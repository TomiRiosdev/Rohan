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
        IStockPorSucursalRepository StockPorSucursalRepository { get; }
       IProductoProveedorRepository ProductoProveedorRepository { get; }

        // Método para guardar los cambios en la base de datos

        void SaveChanges();
    }
}
