using DAO.Interface;
using Models;
using System.Data.Entity;

namespace DAO.Implementations.SQLServer
{
    public class ProductoProveedorRepository : IProductoProveedorRepository
    {
        private readonly RohanContext _dbContext;

        public ProductoProveedorRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "El contexto no puede ser nulo.");
        }

        // 1. ASOCIAR UN PRODUCTO A UN PROVEEDOR
        public void Add(ProductoProveedor entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Seteamos el ID si no viene asignado desde la BLL
            if (entity.IdProductoProveedor == Guid.Empty)
            {
                entity.IdProductoProveedor = Guid.NewGuid();
            }

            _dbContext.ProductoProveedor.Add(entity); 
        }

        // 2. BUSCAR QUÉ PRODUCTOS LE PERTENECEN A UN PROVEEDOR
        // (Clave para filtrar el combo de productos cuando se elija el proveedor en la Orden de Compra)
        public IEnumerable<ProductoProveedor> GetByProveedor(Guid idProveedor)
        {
            try
            {
                if (idProveedor == Guid.Empty) throw new ArgumentException("ID de proveedor inválido.");

                return _dbContext.ProductoProveedor
                    .Include(pp => pp.IdProductoNavigation) // Trae los datos del Producto (Nombre, SKU)
                    .Where(pp => pp.IdProveedor == idProveedor)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: No se pudieron recuperar los productos del proveedor {idProveedor}.", ex);
            }
        }

        // 3. BUSCAR QUÉ PROVEEDORES TIENE UN PRODUCTO
        public IEnumerable<ProductoProveedor> GetByProducto(Guid idProducto)
        {
            try
            {
                if (idProducto == Guid.Empty) throw new ArgumentException("ID de producto inválido.");

                return _dbContext.ProductoProveedor
                    .Include(pp => pp.IdProveedorNavigation) // Trae los datos del Proveedor (Nombre, CUIT)
                    .Where(pp => pp.IdProducto == idProducto)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: No se pudieron recuperar los proveedores del producto {idProducto}.", ex);
            }
        }

        // 4. DESVINCULAR RELACIÓN
        public void Remove(Guid idProductoProveedor)
        {
            try
            {
                var registro = _dbContext.ProductoProveedor.Find(idProductoProveedor);
                if (registro == null) throw new KeyNotFoundException("No se encontró la relación Producto-Proveedor.");

                // Al ser una tabla puramente intermedia de asociación, acá sí corresponde un Hard Delete (Remove físico)
                // ya que si se quiere romper el vínculo, la fila deja de existir. No afecta históricos.
                _dbContext.ProductoProveedor.Remove(registro);
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: No se pudo eliminar la relación Producto-Proveedor.", ex);
            }
        }
    }
}
