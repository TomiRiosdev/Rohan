using BLL.DomainDtos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónSucursal.Mapper
{
    public static class TipoSucursalMapper
    {
        public static TipoSucursalDTO ToDTO(this TipoSucursal entity)
        {
            if (entity == null) return null;

            return new TipoSucursalDTO
            {
                Id = entity.IdTipoSucursal,
                Descripcion = entity.Descripcion
            };
        }

        public static TipoSucursal ToEntity(this TipoSucursalDTO dto)
        {
            if (dto == null) return null;

            return new TipoSucursal
            {
                IdTipoSucursal = dto.Id,
                Descripcion = dto.Descripcion
            };
        }
        public static IEnumerable<TipoSucursalDTO> ToDTOList(this IEnumerable<TipoSucursal> entities)
        {
            return entities?.Select(e => e.ToDTO()).ToList();
        }
      
        public static void UpdateEntity(this TipoSucursal entity, TipoSucursalDTO dto)
        {
            if (entity == null || dto == null) return;
            entity.Descripcion = dto.Descripcion;
        }
    }
}
