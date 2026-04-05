using Models;
using DAO.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAO.Implementations.SQLServer
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly RohanContext _dbContext;
        public ProveedorRepository(RohanContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "El contexto de base de datos no puede ser nulo.");
        }
        

        public Guid Add(Proveedor entity)
        {
            try
            {
                if(entity == null)
                    throw new ArgumentNullException(nameof(entity), "El proveedor a agregar no puede ser nulo.");
                entity.IdProveedor = Guid.NewGuid(); // Asigna un nuevo GUID al proveedor
                entity.Habilitado = true; // Asegura que el proveedor esté habilitado al agregarlo
                
                _dbContext.Proveedor.Add(entity);
                _dbContext.SaveChanges(); // Guarda los cambios en la base de datos

                return entity.IdProveedor; 
            }
            catch (DbUpdateException ex)
            { 
                throw new Exception("DAO Error: Error de persistencia al agregar el proveedor. Verifique restricciones de base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Ocurrió un error inesperado al intentar agregar el proveedor.", ex);
            }
        }

        public bool ExistsByName(string nombre)
        {
            return _dbContext.Proveedor
            .Any(p => p.Nombre.ToLower() == nombre.ToLower());
        }

        public IEnumerable<Proveedor> GetAll()
        {
            try
            {
                return _dbContext.Proveedor
                    .Where(p => p.Habilitado == true)
                    .AsNoTracking() 
                    .ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("DAO Error: No se pudieron obtener los proveedores habilitados.", ex);
            }
        }

        public IEnumerable<Proveedor> GetAllDesHabilitados()
        {
            try
            {
                return _dbContext.Proveedor
                    .Where(p => p.Habilitado == false)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("DAO Error: No se pudieron obtener los proveedores deshabilitados.", ex);
            }
        }

        public Proveedor GetById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("El ID proporcionado no es válido.", nameof(id));

                // Buscamos el proveedor sin importar si está habilitado o no (para auditoría)
                var proveedor = _dbContext.Proveedor.Find(id);

                if (proveedor == null)
                    throw new KeyNotFoundException($"No se encontró ningún proveedor con el ID: {id}");

                return proveedor;
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Error al buscar el proveedor con ID {id}.", ex);
            }
        }

        public IEnumerable<Proveedor> GetByNombre(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("El nombre del Proveedor no puede ser vacío.", nameof(name));
                return _dbContext.Proveedor
                    .Where(p => p.Nombre.Contains(name) && p.Habilitado == true)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Error al buscar producto por nombre: {name}.", ex);
            }
        }

        public void Remove(Guid id)
        {
            try
            {
                var proveedor = _dbContext.Proveedor.Find(id);
                if (proveedor == null)
                    throw new KeyNotFoundException("No se puede eliminar un proveedor inexistente.");

                // SOFT DELETE
                proveedor.Habilitado = false;

                _dbContext.Entry(proveedor).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Falló la deshabilitación lógica del proveedor.", ex);
            }
        }

        public void Update(Proveedor entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("DAO Error: El proveedor fue modificado por otro usuario. Recargue los datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: No se pudo actualizar la información del proveedor.", ex);
            }
        }

       
    }
}
