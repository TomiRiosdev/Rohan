using DAO.Interface;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DAO.Implementations.SQLServer
{
    public class UnidadMedidaRepository : IUnidadMedidaRepository
    {
        private readonly RohanContext _dbContext;

        public UnidadMedidaRepository(RohanContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(UnidadMedida entity)
        {
            try
            {
                if(entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
                if(entity.IdUnidadMedida == Guid.Empty)
                  entity.IdUnidadMedida = Guid.NewGuid();

                _dbContext.UnidadMedida.Add(entity);
                _dbContext.SaveChanges();
                return entity.IdUnidadMedida;
            }
            catch (Exception ex)
            {

                throw new Exception("DAO Error: No se pudo agregar la Unidad de Medida..", ex);
            }
        }

        public bool ExistsByName(string nombre)
        {
            return _dbContext.UnidadMedida
            .Any(p => p.Descripcion.ToLower() == nombre.ToLower());
        }

        public IEnumerable<UnidadMedida> GetAll()
        {
            try
            {
                return _dbContext.UnidadMedida.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Falló la obtención de Unidad de Medida..", ex);
            }
        }

        public UnidadMedida GetById(Guid id)
        {
            try
            {
                return _dbContext.UnidadMedida.Find(id);
            }
            catch (Exception ex)
            {

                throw new Exception("DAO Error: Falló la obtención de Unidad de Medida.", ex);
            }
        }

        public UnidadMedida GetByNombre(string name)
        {
            try
            {
                return _dbContext.UnidadMedida
                     .FirstOrDefault(um => um.Descripcion.ToLower() == name.ToLower());
            }
            catch (Exception ex)
            {

                throw new Exception("DAO Error: Falló la obtención de Unidad de Medida.", ex);
            }
        }

        public void Remove(Guid id)
        {
            try
            {
                var entity = GetById(id);
                if(entity == null)
                {
                    _dbContext.UnidadMedida.Remove(entity);
                    _dbContext.SaveChanges();
                }
                   
            }
            catch (Exception ex)
            {

                throw new Exception("DAO Error: Falló la eliminacion de Unidad de Medida.", ex);
            }
        }
        

        public void Update(UnidadMedida entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
