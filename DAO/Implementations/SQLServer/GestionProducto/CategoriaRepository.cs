using DAO.Interface.GestionProducto;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAO.Implementations.SQLServer.GestionProducto
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly RohanContext _dbContext;
        public CategoriaRepository(RohanContext dbContext)
        {
           _dbContext = dbContext;
        }
          
        public Guid Add(Categoria entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
                }

                //genera el GUID automáticamente
                if (entity.IdCategoria == Guid.Empty)
                    entity.IdCategoria = Guid.NewGuid();

                _dbContext.Categoria.Add(entity);
                return entity.IdCategoria;
            }
            catch (Exception ex)
            {
                // Manejo de excepción genérica en caso de que falle la operación interna
                throw new Exception("DAO Error: No se pudo agregar la categoría al contexto.", ex);
            }
        }

        public bool ExistsByName(string nombre)
        {
            return _dbContext.Categoria
           .Any(p => p.Descripcion.ToLower() == nombre.ToLower());
        }

        public IEnumerable<Categoria> GetAll()
        {
            try
            {
                return _dbContext.Categoria.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Falló la obtención de categorías.", ex);
            }
        }

        public Categoria GetById(Guid id)
        {
            try
            {
                return _dbContext.Categoria.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda de la categoría por ID {id}.", ex);
            }
        }

        public Categoria GetByNombre(string name)
        {
            try
            {
                return _dbContext.Categoria
                    .FirstOrDefault(c => c.Descripcion.ToLower() == name.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda de la categoría por nombre '{name}'.", ex);
            }
        }

        public void Remove(Guid id)
        {
            try
            {
                var entity = GetById(id);
                if (entity !=null)
                {
                    _dbContext.Categoria.Remove(entity);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("DAO Error: No se pudo eliminar la categoría.", ex);
            }
        }

        public void Update(Categoria entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "La entidad a actualizar no puede ser nula.");
                }

             
                _dbContext.Entry(entity).State = EntityState.Modified;
      
                _dbContext.Categoria.Update(entity); 
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: No se pudo marcar la categoría para actualización.", ex);
            }
        }
    }
}
