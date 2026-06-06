using BLL.DomainDtos;
using BLL.Enum;
using Models;

namespace BLL.GestiónProducto.Mapper
{
    public static class ProductoMapper
    {
        public static ProductoDTO ToDTO(Producto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad Producto no puede ser null.");

            return new ProductoDTO()
            {
                Id = entity.IdProducto,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                CodigoSku = entity.CodigoSku,
                ContenidoPorVenta = entity.ContenidoPorVenta,
                IdCategoria = entity.IdCategoria ?? Guid.Empty,
                IdUnidadMedida = entity.IdUnidadMedida ?? Guid.Empty,
                IdTipoEnvase = entity.IdTipoEnvase ?? 0,
                CantidadPorBulto = (int)(entity.CantidadPorBulto ?? 0),
                CategoriaNombre = entity.IdCategoriaNavigation?.Descripcion ?? "Sin categoría",
                UnidadMedidaNombre = entity.IdUnidadMedidaNavigation?.Descripcion?? "Sin unidad",   // ← Cambia si tu propiedad es Descripcion
                

            };
        }

        public static Producto ToEntity(ProductoDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO Producto no puede ser null.");

            return new Producto()
            {
                IdProducto = dto.Id,
                Nombre = dto.Nombre,
                IdCategoria = dto.IdCategoria,
                IdUnidadMedida = dto.IdUnidadMedida,
                CodigoSku = dto.CodigoSku ?? 0,
                ContenidoPorVenta = dto.ContenidoPorVenta ?? 0,
                Descripcion = dto.Descripcion,
                IdTipoEnvase = dto.IdTipoEnvase,
                CantidadPorBulto = dto.CantidadPorBulto

            };
        }

        public static IEnumerable<ProductoDTO> ToDTOList(IEnumerable<Producto> entities)
        {
            return entities?.Select(ToDTO) ?? Enumerable.Empty<ProductoDTO>();
        }

        // Método para actualizar entidad existente
        public static void UpdateEntity(Producto entity, ProductoDTO dto)
        {
            if (entity == null || dto == null) return;

            entity.Nombre = dto.Nombre;
            entity.Descripcion = dto.Descripcion;
            entity.CodigoSku = dto.CodigoSku;
            entity.ContenidoPorVenta = dto.ContenidoPorVenta ?? 0;
            entity.IdCategoria = dto.IdCategoria;
            entity.IdUnidadMedida = dto.IdUnidadMedida;
            entity.IdTipoEnvase = dto.IdTipoEnvase;
            entity.CantidadPorBulto = dto.CantidadPorBulto;

        }
    }
} 