using DAO.Interface.GestionProveedor;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DAO.Implementations.SQLServer.GestionProveedor
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
        public IEnumerable<ProductoProveedor> GetByProveedor(Guid idProveedor)
        {
            try
            {
                if (idProveedor == Guid.Empty) throw new ArgumentException("ID de proveedor inválido.");

                return _dbContext.ProductoProveedor
                       .Include(pp => pp.IdProductoNavigation) 
                       .Where(pp =>
                            pp.IdProveedor == idProveedor &&
                            pp.IdProductoNavigation.Habilitado == true) 
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
    
        public void Delete(Guid idProducto, Guid idProveedor)
        {
            var registro = _dbContext.ProductoProveedor
                 .FirstOrDefault(pp => pp.IdProducto == idProducto && pp.IdProveedor == idProveedor);

            if (registro != null)
            {
                _dbContext.ProductoProveedor.Remove(registro);
            }
        }

        public bool ExisteRelacion(Guid idProducto, Guid idProveedor)
        {
            return _dbContext.ProductoProveedor
                .Any(pp => pp.IdProducto == idProducto && pp.IdProveedor == idProveedor);
        }

        public void UpdatePrecioUnitario(Guid idProducto, Guid idProveedor, decimal nuevoPrecioUnitario)
        {
            var registro = _dbContext.ProductoProveedor
            .FirstOrDefault(pp => pp.IdProducto == idProducto && pp.IdProveedor == idProveedor);

            if (registro == null)
                throw new Exception("No se encontró la relación entre el producto y el proveedor.");

            // Asumimos que tienes una propiedad 'PrecioUnitario' en tu entidad ProductoProveedor
            registro.UltimoPrecioCompra = nuevoPrecioUnitario;
        }

        public void AgregarProveedorPrincipal(Guid idProducto, Guid idProveedor)
        {
            // 1. Primero, quitamos el flag de principal a todos los proveedores de este producto
            var actuales = _dbContext.ProductoProveedor.Where(pp => pp.IdProducto == idProducto);
            foreach (var item in actuales)
            {
                item.EsProveedorPrincipal = false;
            }

            // 2. Marcamos al nuevo como principal
            var nuevoPrincipal = actuales.FirstOrDefault(pp => pp.IdProveedor == idProveedor);
            if (nuevoPrincipal != null)
            {
                nuevoPrincipal.EsProveedorPrincipal = true;
            }
        }
    }
}
