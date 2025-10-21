using DAO;
using DAO;
using DAO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementations.SQLServer
{
    public class UnidadMedidaRepository : IUnidadMedidaRepository
    {
        private readonly RohanDbContext _dbContext;

        public UnidadMedidaRepository(RohanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(UnidadMedida entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }
            entity.IdUnidadMedida = Guid.NewGuid(); // Asigna el ID aquí por convención
            _dbContext.UnidadMedidas.Add(entity);

            return entity.IdUnidadMedida;
        }

        public IEnumerable<UnidadMedida> GetAll()
        {
            return _dbContext.UnidadMedidas
                .Where(u => u.Habilitado)
                .ToList();
        }

        public IEnumerable<UnidadMedida> GetAllDesHabilitados()
        {
            // DEVUELVE SOLO DESHABILITADOS (Para el formulario de rehabilitación)
            return _dbContext.UnidadMedidas
                .Where(u => !u.Habilitado)
                .ToList();
        }

        public UnidadMedida GetById(Guid id)
        {
            // BUSCA POR ID Y POR ESTADO HABILITADO
            if (id == Guid.Empty)
            {
                throw new ArgumentException("El ID no puede ser vacío.", nameof(id));
            }
            return _dbContext.UnidadMedidas
                     .FirstOrDefault(u => u.IdUnidadMedida == id && u.Habilitado == true);
        }

        public UnidadMedida GetByNombre(string name)
        {
            // BUSCA POR NOMBRE Y POR ESTADO HABILITADO
            return _dbContext.UnidadMedidas
                         .FirstOrDefault(u => u.Habilitado == true &&
                                              u.Nombre.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Remove(Guid id)
        {
            var entidad = _dbContext.UnidadMedidas.Find(id);
            if (entidad != null)
            {
                entidad.Habilitado = false;
                _dbContext.Entry(entidad).State = EntityState.Modified;
            }
            var proveedor = _dbContext.Proveedores.Find(id);
            
        }

        public void Update(UnidadMedida entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
