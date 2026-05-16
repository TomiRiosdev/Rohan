using DAO.Interface;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAO.Implementations.SQLServer
{
    public class TipoSucursalRepository : ITipoSucursalRepository
    {
        private readonly RohanContext _dbContext;
        public TipoSucursalRepository(RohanContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(TipoSucursal entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
                }

                //genera el GUID automáticamente
                if (entity.IdTipoSucursal == Guid.Empty)
                    entity.IdTipoSucursal = Guid.NewGuid();

                _dbContext.TipoSucursal.Add(entity);
               
                return entity.IdTipoSucursal;
            }
            catch (Exception ex)
            {
                // Manejo de excepción genérica en caso de que falle la operación interna
                throw new Exception("DAO Error: No se pudo agregar el tipo de sucursal.", ex);
            }
        }

        public bool ExistsByName(string nombre)
        {
            return _dbContext.TipoSucursal
          .Any(p => p.Descripcion.ToLower() == nombre.ToLower());
        }

        public IEnumerable<TipoSucursal> GetAll()
        {
            try
            {
                return _dbContext.TipoSucursal.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Falló la obtención de tipo de sucursal.", ex);
            }
            
        }

        public TipoSucursal GetById(Guid id)
        {

            try
            {
                return _dbContext.TipoSucursal.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda del tipo dde sucursal por ID {id}.", ex);
            }
        }

        public TipoSucursal GetByNombre(string name)
        {
            try
            {
                return _dbContext.TipoSucursal
                    .FirstOrDefault(c => c.Descripcion.ToLower() == name.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda del tipo de sucursal por nombre '{name}'.", ex);

            }
        }

        public void Remove(Guid id)
        {
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    _dbContext.TipoSucursal.Remove(entity);
                   
                }
            }
            catch (Exception ex)
            {

                throw new Exception("DAO Error: No se pudo eliminar.", ex);
            }
        }

        public void Update(TipoSucursal entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "La entidad a actualizar no puede ser nula.");
                }


                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.TipoSucursal.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: No se pudo marcar el tipo de sacursal para la actualización.", ex);
            }
        }
    }
}
