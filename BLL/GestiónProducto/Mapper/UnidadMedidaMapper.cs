using BLL.DomainDtos;
using Models;


namespace BLL.GestiónProducto.Mapper
{
    public static class UnidadMedidaMapper
    {
        public static UnidadMedidaDTO ToDTO(UnidadMedida entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad Unidad Medida no puede ser null.");

            return new UnidadMedidaDTO
            {
                Id = entity.IdUnidadMedida,
                Descripcion = entity.Descripcion
            };
        }

        public static UnidadMedida ToEntity(UnidadMedidaDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO Unidad Medida no puede ser null.");

            return new UnidadMedida
            {
                IdUnidadMedida = dto.Id,
                Descripcion = dto.Descripcion?.Trim()
            };
        }

        public static void UpdateEntity(UnidadMedida entity, UnidadMedidaDTO dto)
        {
            if (entity == null || dto == null) return;

            entity.Descripcion = dto.Descripcion?.Trim();

        }

        public static IEnumerable<UnidadMedidaDTO> ToDTOList(IEnumerable<UnidadMedida> entities)
        {
            return entities?.Select(ToDTO) ?? Enumerable.Empty<UnidadMedidaDTO>();
        }
    }
}
