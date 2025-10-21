using DAO;
using DAO;
using DAO.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementations.SQLServer
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly RohanDbContext _dbContext;
        public CategoriaRepository(RohanDbContext dbContext)
        {
           _dbContext = dbContext;
        }
          
        public Guid Add(CategoriaProducto entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
                }

                entity.IdCategoriaProdcuto = Guid.NewGuid();
                _dbContext.CategoriaProductos.Add(entity);
                return entity.IdCategoriaProdcuto;
            }
            catch (Exception ex)
            {
                // Manejo de excepción genérica en caso de que falle la operación interna
                throw new Exception("DAO Error: No se pudo agregar la categoría al contexto.", ex);
            }
        }

        public IEnumerable<CategoriaProducto> GetAll()
        {
            if (_dbContext.CategoriaProductos == null)
            {
                throw new InvalidOperationException("El conjunto de Proveedores es nulo.");
            }
            return _dbContext.CategoriaProductos
                .Where(p => p.Habilitado) // Asumiendo que 'Habilitado' es una propiedad booleana en Proveedore
                .ToList();
        }

        public IEnumerable<CategoriaProducto> GetAllDesHabilitados()
        {
           if (_dbContext.CategoriaProductos == null)
            {
                throw new InvalidOperationException("El conjunto de Categoría de Productos es nulo.");
            }
            try
            {
                return _dbContext.CategoriaProductos
                    .Where(c => !c.Habilitado) 
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("DAO Error: Falló la obtención de categorías deshabilitadas.", ex);
            }
        }

        public CategoriaProducto GetById(Guid id)
        {
            try
            {
                // Método eficiente para buscar por clave primaria
                return _dbContext.CategoriaProductos.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda de la categoría por ID {id}.", ex);
            }
        }

        public CategoriaProducto GetByNombre(string name)
        {
            try
            {
                // Busca la primera entidad que coincida con el nombre
                return _dbContext.CategoriaProductos.FirstOrDefault(c => c.Nombre == name);
            }
            catch (Exception ex)
            {
                throw new Exception($"DAO Error: Falló la búsqueda de la categoría por nombre '{name}'.", ex);
            }
        }

        public void Remove(Guid id)
        {
            var proveedor = _dbContext.CategoriaProductos.Find(id);
            if (proveedor != null)
            {
                // ELIMINACIÓN LÓGICA (Soft Delete): Cambia el estado en lugar de removerlo.
                proveedor.Habilitado = false;

                // Actualizar el estado de seguimiento del objeto en el contexto
                _dbContext.Entry(proveedor).State = EntityState.Modified;
            }
        }

        public void Update(CategoriaProducto entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "La entidad a actualizar no puede ser nula.");
                }

                // Opción 1: Adjuntar la entidad y marcarla como modificada (si el tracking está desactivado)
                _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                // Opción 2: Usar el método Update (si la entidad está detach)
                // _dbContext.CategoriaProductos.Update(entity); 
            }
            catch (Exception ex)
            {
                // Manejo de excepción genérica de actualización
                throw new Exception("DAO Error: No se pudo marcar la categoría para actualización.", ex);
            }
        }
    }
}
