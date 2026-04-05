using DAO.Interface;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DAO.Implementations.SQLServer
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly RohanContext _dbContext;
        public ProductoRepository
        (
            RohanContext dbContext
        )
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Guid Add(Producto entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "El producto no puede ser nulo.");

                entity.IdProducto = Guid.NewGuid();
                entity.FechaCreacion = DateTime.Now;
                entity.Habilitado = true;

                _dbContext.Producto.Add(entity);
                _dbContext.SaveChanges();

                return entity.IdProducto;
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: No se pudo agregar el producto.", ex);
            }
        }

        public void Remove(Guid id)
        {
            try
            {
                var producto = _dbContext.Producto.Find(id);
                if (producto == null) throw new KeyNotFoundException();

                producto.Habilitado = false; // Soft Delete
                _dbContext.Entry(producto).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Falló la deshabilitación del producto.", ex);
            }
        }

        public void Update(Producto entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: No se pudo actualizar el producto.", ex);
            }
        }

        public Producto GetById(Guid id)
        {
            try
            {
                var producto = _dbContext.Producto
                    .Include(p => p.IdCategoriaNavigation)
                    .Include(p => p.IdUnidadMedidaNavigation)
                    .FirstOrDefault(p => p.IdProducto == id);

                if (producto == null)
                    throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

                return producto;
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda del producto {id}.", ex);
            }
        }

        public IEnumerable<Producto> GetAll()
        {
            try
            {
                return _dbContext.Producto
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdUnidadMedidaNavigation)
                .Where(p => p.Habilitado.HasValue && p.Habilitado.Value)
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Error al obtener la lista de productos.", ex);
            }
        }

        public IEnumerable<Producto> GetAllDesHabilitados()
        {
            try
            {
                return _dbContext.Producto
                    .Where(p => p.Habilitado == false)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Error al obtener productos deshabilitados.", ex);
            }
        }

        public IEnumerable<Producto> GetByNombre(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("El nombre del producto no puede ser vacío.", nameof(name));
                return _dbContext.Producto
                    .Include(p => p.IdCategoriaNavigation)
                    .Include(p => p.IdUnidadMedidaNavigation)
                    .Where(p => p.Nombre.Contains(name) && p.Habilitado == true)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Error al buscar producto por nombre: {name}.", ex);
            }
        }

        public bool ExistsByName(string nombre)
        {
            return _dbContext.Producto
            .Any(p => p.Nombre.ToLower() == nombre.ToLower());
        }

        public bool ExistsByCodigoSku(int codigoSku)
        {
            return _dbContext.Producto
            .Any(p => p.CodigoSku == codigoSku);
        }

        public bool ExistsByNameExceptId(string nombre, Guid idExcluir)
        {
            return _dbContext.Producto
              .Any(p => p.Nombre.ToLower() == nombre.ToLower() && p.IdProducto != idExcluir);
        }

        public bool ExistsByCodigoSkuExceptId(int codigoSku, Guid idExcluir)
        {
            return _dbContext.Producto
            .Any(p => p.CodigoSku == codigoSku && p.IdProducto != idExcluir);
        }
    }
}
