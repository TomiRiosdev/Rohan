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
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly RohanDbContext _dbContext;
        public ProveedorRepository(RohanDbContext dbContext)
        { 
              _dbContext = dbContext;
        }

        public Guid Add(Proveedore entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }
            entity.IdProveedor = Guid.NewGuid();
            _dbContext.Proveedores.Add(entity);

            return entity.IdProveedor;
        }

        public IEnumerable<Proveedore> GetAll()
        {
            if (_dbContext.Proveedores == null)
            {
                throw new InvalidOperationException("El conjunto de Proveedores es nulo.");
            }

            return _dbContext.Proveedores
                .Where(p => p.Habilitado)
                .ToList();
        }

        public IEnumerable<Proveedore> GetAllDesHabilitados()
        {
           if (_dbContext.Proveedores == null)
            {
                throw new InvalidOperationException("El conjunto de Proveedores es nulo.");
            }
            return _dbContext.Proveedores
                .Where(p => !p.Habilitado) // Asumiendo que 'Habilitado' es una propiedad booleana en Proveedore
                .ToList();
        }

        public Proveedore GetById(Guid id)
        {
            // Validación de ID va mejor en la BLL, pero se mantiene aquí por consistencia.
            if (id == Guid.Empty)
            {
                throw new ArgumentException("El ID no puede ser vacío.", nameof(id));
            }

            // Consulta por ID y por estado Habilitado
            return _dbContext.Proveedores
                .FirstOrDefault(p => p.IdProveedor == id && p.Habilitado == true);
        }

        public Proveedore GetByNombre(string name)
        {
            var proveedor = _dbContext.Proveedores
                    .FirstOrDefault(p => p.Habilitado == true &&
                                         p.Nombre.Equals(name, StringComparison.OrdinalIgnoreCase));
            return proveedor;
        }

        public void Remove(Guid id)
        {
            var proveedor = _dbContext.Proveedores.Find(id);
            if(proveedor != null)
            {
                // ELIMINACIÓN LÓGICA (Soft Delete): Cambia el estado en lugar de removerlo.
                proveedor.Habilitado = false;

                // Actualizar el estado de seguimiento del objeto en el contexto
                _dbContext.Entry(proveedor).State = EntityState.Modified;
            }
        }

        public void Update(Proveedore entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
