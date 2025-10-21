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
    public class TipoProductoRepository : ITipoProductoRepository
    {
        private readonly RohanDbContext _dbContext;
        public TipoProductoRepository(RohanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Add(TipoProducto entity)
        {
           if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }
            entity.IdTipoProducto = Guid.NewGuid();
            _dbContext.TipoProductos.Add(entity);
            return entity.IdTipoProducto;
        }

        public void Remove(Guid id)
        {
            var proveedor = _dbContext.Proveedores.Find(id);
            if (proveedor != null)
            {
                // ELIMINACIÓN LÓGICA (Soft Delete): Cambia el estado en lugar de removerlo.
                proveedor.Habilitado = false;

                // Actualizar el estado de seguimiento del objeto en el contexto
                _dbContext.Entry(proveedor).State = EntityState.Modified;
            }
        }

        public IEnumerable<TipoProducto> GetAll()
        {
            // DEVUELVE SOLO HABILITADOS
            return _dbContext.TipoProductos
                .Where(t => t.Habilitado)
                .ToList();
        }

        public IEnumerable<TipoProducto> GetAllDesHabilitados()
        {
            // DEVUELVE SOLO DESHABILITADOS
            return _dbContext.TipoProductos
                .Where(t => !t.Habilitado)
                .ToList();
        }

        public TipoProducto GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("El ID no puede ser vacío.", nameof(id));
            }
            // BUSCA POR ID Y POR ESTADO HABILITADO
            return _dbContext.TipoProductos
                .FirstOrDefault(t => t.IdTipoProducto == id && t.Habilitado == true);
        }

        public TipoProducto GetByNombre(string name)
        {
            // BUSCA POR NOMBRE Y POR ESTADO HABILITADO
            return _dbContext.TipoProductos
                .FirstOrDefault(t => t.Habilitado == true &&
                                     t.Nombre.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


        public void Update(TipoProducto entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }


    }
}
