using BLL.DomainDtos;
using Models;


namespace BLL.GestiónSucursal.Mapper
{
    public static class SucursalMapper
    {
        public static SucursalDTO ToDTO(this Sucursal sucursal)
        {

            if (sucursal == null)
                throw new ArgumentNullException(nameof(sucursal), "La entidad Sucursal no puede ser null.");

            return new SucursalDTO
            {
                Id = sucursal.IdSucursal,
                Nombre = sucursal.Nombre,
                Email = sucursal.Email,
                Direccion = sucursal.Direccion,
                CodigoPostal = sucursal.CodigoPostal,
                Telefono = sucursal.Telefono,
                IdTipoSucursal = sucursal.IdTipoSucursal ?? Guid.Empty,
                TipoSucursalNombre = sucursal.IdTipoSucursalNavigation?.Descripcion ?? "Sin tipo de sucursal",
                Localidad = sucursal.Localidad,

            };
        }

        public static Sucursal ToEntity(this SucursalDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO Sucursal no puede ser null.");

            return new Sucursal
            {
                IdSucursal = dto.Id,
                Nombre = dto.Nombre,
                Email = dto.Email,
                Direccion = dto.Direccion,
                CodigoPostal = dto.CodigoPostal,
                Telefono = dto.Telefono,
                IdTipoSucursal = dto.IdTipoSucursal,
                Localidad = dto.Localidad,
                Habilitado = dto.Habilitado
            };
        } 

        public static IEnumerable<SucursalDTO> ToDTOList(this IEnumerable<Sucursal> sucursales)
        {
                return sucursales?.Select(ToDTO) ?? Enumerable.Empty<SucursalDTO>();
        }
        
        public static void UpdateEntity(this Sucursal entity, SucursalDTO dto)
        {
            if (entity == null || dto == null) return;

            entity.Nombre = dto.Nombre;
            entity.Email = dto.Email;
            entity.Direccion = dto.Direccion;
            entity.CodigoPostal = dto.CodigoPostal;
            entity.Telefono = dto.Telefono;
            entity.IdTipoSucursal = dto.IdTipoSucursal;
            entity.Localidad = dto.Localidad;
           
        }
    }
}
