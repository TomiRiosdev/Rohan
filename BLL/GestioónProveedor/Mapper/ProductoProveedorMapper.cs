using BLL.DomainDtos;
using Models;
using System;

namespace BLL.GestiónProveedor.Mapper
{
    public static class ProductoProveedorMapper
    {
        public static ProductoProveedorDTO ToDTO(this ProductoProveedor entity)
        {
            if (entity == null) return null!;

            return new ProductoProveedorDTO
            {
                IdProducto = entity.IdProducto ?? Guid.Empty,
                IdProveedor = entity.IdProveedor ?? Guid.Empty,
                EsProveedorPrincipal = entity.EsProveedorPrincipal ?? false,

                // Mapeos relacionales seguros (Evitan NullReferenceException)
                CodigoSku = entity.IdProductoNavigation?.CodigoSku ?? 0,
                ProductoNombre = entity.IdProductoNavigation?.Nombre ?? "Materia Prima No Identificada",
                CategoriaNombre = entity.IdProductoNavigation?.IdCategoriaNavigation?.Descripcion ?? "Sin Categoría",

                ProveedorRazonSocial = entity.IdProveedorNavigation?.RazonSocial ?? "Proveedor Desconocido",
                ProveedorCuit = entity.IdProveedorNavigation?.Cuit ?? "Sin CUIT"
            };
        }

        public static ProductoProveedor ToEntity(this ProductoProveedorDTO dto)
        {
            if (dto == null) return null!;

            return new ProductoProveedor
            {
                IdProducto = dto.IdProducto,
                IdProveedor = dto.IdProveedor,
                EsProveedorPrincipal = dto.EsProveedorPrincipal,
              //  FechaAsignacion = DateTime.Now // Auditoría de trazabilidad interna de Rohan
            };
        }
    }
}
