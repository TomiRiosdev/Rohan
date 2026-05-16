using BLL.DomainDtos;
using Models;

namespace BLL.GestiónStock.Mapper
{
    public static class ProductoProveedorMapper
    {
        public static ProductoProveedorDTO ToDTO(this ProductoProveedor relacion)
        {
            if (relacion == null)
                throw new ArgumentNullException(nameof(relacion), "La entidad ProductoProveedor no puede ser null.");

            return new ProductoProveedorDTO
            {
                IdProductoProveedor = relacion.IdProductoProveedor,
                IdProducto = relacion.IdProducto ?? Guid.Empty,
                ProductoNombre = relacion.IdProductoNavigation?.Nombre ?? "Producto no identificado",
                IdProveedor = relacion.IdProveedor ?? Guid.Empty,
                ProveedorNombre = relacion.IdProveedorNavigation?.Nombre ?? "Proveedor no identificado",
                EsProveedorPrincipal = relacion.EsProveedorPrincipal ?? false,
                UltimoPrecioCompra = relacion.UltimoPrecioCompra ?? 0
            };
        }

        public static ProductoProveedor ToEntity(this ProductoProveedorDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO ProductoProveedor no puede ser null.");

            return new ProductoProveedor
            {
                IdProductoProveedor = dto.IdProductoProveedor,
                IdProducto = dto.IdProducto,
                IdProveedor = dto.IdProveedor,
                EsProveedorPrincipal = dto.EsProveedorPrincipal,
                UltimoPrecioCompra = dto.UltimoPrecioCompra
            };
        }

        public static IEnumerable<ProductoProveedorDTO> ToDTOList(this IEnumerable<ProductoProveedor> lista)
        {
            return lista?.Select(ToDTO) ?? Enumerable.Empty<ProductoProveedorDTO>();
        }

        public static void UpdateEntity(this ProductoProveedor entity, ProductoProveedorDTO dto)
        {
            if (entity == null || dto == null) return;

            entity.EsProveedorPrincipal = dto.EsProveedorPrincipal;
            entity.UltimoPrecioCompra = dto.UltimoPrecioCompra;
        }
    }
}
