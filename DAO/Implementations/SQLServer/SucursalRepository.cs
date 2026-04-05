using DAO.Interface;
using Models;
using Microsoft.EntityFrameworkCore;

namespace DAO.Implementations.SQLServer
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly RohanContext _dbContext;
        public SucursalRepository
        (
           RohanContext rohanContext
        )
        {
           _dbContext = rohanContext ?? throw new ArgumentNullException(nameof(rohanContext));
        }

        #region METODOS CRUD
        public Guid Add(Sucursal entity)
        {
            try
            {
                if(entity == null)
                    throw new ArgumentException(nameof(entity), "La sucursal no puede ser nula");
                entity.IdSucursal = Guid.NewGuid();
                entity.Habilitado = true;

                _dbContext.Sucursal.Add(entity);
                _dbContext.SaveChanges();
                return entity.IdSucursal;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al agregar la sucursal: " + ex.Message, ex);
            }
        }
        
        public void Remove(Guid id)
        {
            try
            {
                var sucursal = _dbContext.Sucursal.Find(id);
                if(sucursal == null)
                    throw new KeyNotFoundException("No se encontró la sucursal con el ID proporcionado.");

                sucursal.Habilitado = false;
                _dbContext.Entry(sucursal).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al eliminar la sucursal: " + ex.Message, ex);
            }
        }
        
        public void Update(Sucursal entity)
        {
            try
            {
                if(entity == null)
                    throw new ArgumentException(nameof(entity), "La sucursal no puede ser nula");

                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al actualizar la sucursal: " + ex.Message, ex);
            }
        }
       
        public Sucursal GetById(Guid id)
        {
            try
            {
                var sucursal = _dbContext.Sucursal
                    .Include(s => s.IdTipoSucursalNavigation)
                    .FirstOrDefault(s => s.IdSucursal == id);

                if(sucursal == null)
                    throw new KeyNotFoundException("No se encontró la sucursal con el ID proporcionado.");
                
                return sucursal;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener la sucursal por ID: " + ex.Message, ex);
            }
        }

        #endregion

        #region OTROS METODOS
        public bool ExistsByName(string nombre)
        {
            return _dbContext.Sucursal
                .Any(s => s.Nombre.ToLower() == nombre.ToLower());
        }

        public bool ExistsByNameExceptId(string nombre, Guid idExcluir)
        {
            return _dbContext.Sucursal
                .Any(s => s.Nombre.ToLower() == nombre.ToLower() && s.IdSucursal != idExcluir);
        }

        public IEnumerable<Sucursal> GetAll()
        {
            try
            {
                return _dbContext.Sucursal
                    .Include(s => s.IdTipoSucursalNavigation)
                    .Where(p => p.Habilitado.HasValue && p.Habilitado.Value)
                    .AsNoTracking()
                    .OrderBy(s => s.Nombre)
                    .ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener todas las sucursales: " + ex.Message, ex);
            }
        }

        public IEnumerable<Sucursal> GetAllDesHabilitados()
        {
            try
            {
                return _dbContext.Sucursal
                    .Where(p => p.Habilitado == false)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las sucursales deshabilitadas.", ex);
            }
        }

        public IEnumerable<Sucursal> GetByNombre(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("El nombre del producto no puede ser vacío.", nameof(name));
                return _dbContext.Sucursal
                    .Include(p => p.IdTipoSucursalNavigation)
                    .Where(p => p.Nombre.Contains(name) && p.Habilitado == true)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($" Error al buscar la sucursal por nombre: {name}.", ex);
            }
        }

        #endregion
    }
}
