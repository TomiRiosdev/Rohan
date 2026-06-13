using BLL.DomainDtos;
using BLL.GestiónProveedor.Interface;
using BLL.GestiónProveedor.Mapper;
using BLL.Infrastructure;
using DAO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónProveedor.Service
{
    public class ProductoProveedorService : IProductoProveedorService
    {
        private readonly IUnitOfWork _uow;

        public ProductoProveedorService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public void VincularProductoAProveedor(ProductoProveedorDTO dto)
        {
            try
            {
                // 1. Validaciones básicas de integridad
                if (dto == null)
                    throw new Exception("Los datos de asignación provistos son nulos.");
                if (dto.IdProducto == Guid.Empty || dto.IdProveedor == Guid.Empty)
                    throw new Exception("Identificadores de Producto o Proveedor inválidos para establecer el vínculo.");

                // 2. Control de Regla de Negocio: Evitar duplicados físicos en la base de datos
                bool existe = _uow.ProductoProveedorRepository.ExisteRelacion(dto.IdProducto, dto.IdProveedor);
                if (existe)
                    throw new Exception("Operación redundante: El producto seleccionado ya se encuentra asignado a este proveedor.");

                // 3. Transformación e inserción a través del repositorio compuesto
                var entity = dto.ToEntity();

                _uow.ProductoProveedorRepository.Add(entity);
                _uow.SaveChanges(); // Confirmación de persistencia atómica
            }
            catch (Exception ex) when (ex.Message.Contains("Operación redundante") || ex.Message.Contains("inválidos"))
            {
                throw; // Dejamos burbujear los errores de negocio limpios hacia la UI
            }
            catch (Exception ex)
            {
                // Telemetría forense en caso de caídas de índices de SQL Server
                var context = ExceptionContext.Crear(ex, new object[] { dto });
                ExceptionLogger.Log(context);
                throw new Exception("Falla crítica al intentar registrar la asignación Producto-Proveedor en el servidor.", ex);
            }
        }

        public void DesvincularProductoDeProveedor(Guid idProducto, Guid idProveedor)
        {
            try
            {
                if (idProducto == Guid.Empty || idProveedor == Guid.Empty)
                    throw new Exception("No se puede revocar la asignación: Identificadores inválidos.");

                // Ejecutamos la baja física en la tabla puente intermediaria
                _uow.ProductoProveedorRepository.Delete(idProducto, idProveedor);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                var context = ExceptionContext.Crear(ex, new object[] { idProducto, idProveedor });
                ExceptionLogger.Log(context);
                throw new Exception("Error interno de infraestructura al intentar remover el vínculo comercial.", ex);
            }
        }

        public IEnumerable<ProductoProveedorDTO> ListarProductosPorProveedor(Guid idProveedor)
        {
            try
            {
                if (idProveedor == Guid.Empty) return Enumerable.Empty<ProductoProveedorDTO>();

                var entidadesPuente = _uow.ProductoProveedorRepository.GetByProveedor(idProveedor);

                // Mapeo fluido utilizando los métodos de extensión estáticos compilados hoy
                return entidadesPuente.Select(pp => pp.ToDTO()).ToList();
            }
            catch (Exception ex)
            {
                var context = ExceptionContext.Crear(ex, new object[] { idProveedor });
                ExceptionLogger.Log(context);
                throw new Exception("Error al consultar el catálogo de materias primas asignadas al proveedor.", ex);
            }
        }

        public IEnumerable<ProductoProveedorDTO> ListarProveedoresPorProducto(Guid idProducto)
        {
            try
            {
                if (idProducto == Guid.Empty) return Enumerable.Empty<ProductoProveedorDTO>();

                var entidadesPuente = _uow.ProductoProveedorRepository.GetByProducto(idProducto);

                return entidadesPuente.Select(pp => pp.ToDTO()).ToList();
            }
            catch (Exception ex)
            {
                var context = ExceptionContext.Crear(ex, new object[] { idProducto });
                ExceptionLogger.Log(context);
                throw new Exception("Error analítico al recuperar los canales de suministro del producto.", ex);
            }
        }
    }
}
