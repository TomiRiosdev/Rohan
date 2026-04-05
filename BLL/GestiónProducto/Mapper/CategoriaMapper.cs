using BLL.DomainDtos;
using Models;


namespace BLL.GestiónProducto.Mapper
{
    public static class CategoriaMapper
    {
        public static CategoriaDTO ToDTO(Categoria entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad Categoria no puede ser null.");

            return new CategoriaDTO
            {
                Id = entity.IdCategoria,
                Descripcion = entity.Descripcion
            };
        }

        public static Categoria ToEntity(CategoriaDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO Categoria no puede ser null.");

            return new Categoria
            {
                IdCategoria = dto.Id,
                Descripcion = dto.Descripcion?.Trim()
            };
        }

        public static void UpdateEntity(Categoria entity, CategoriaDTO dto)
        {
            if (entity == null || dto == null) return;

            entity.Descripcion = dto.Descripcion?.Trim();
        
        }

        public static IEnumerable<CategoriaDTO> ToDTOList(IEnumerable<Categoria> entities)
        {
            return entities?.Select(ToDTO) ?? Enumerable.Empty<CategoriaDTO>();
        }
    }
}
