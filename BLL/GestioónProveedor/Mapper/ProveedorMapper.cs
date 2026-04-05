using BLL.DomainDtos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestioónProveedor.Mapper
{
    public static class ProveedorMapper
    {
        public static ProveedorDTO ToDTO(Proveedor entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad Proveedor no puede ser null.");
            return new ProveedorDTO()
            {
                Id = entity.IdProveedor,
                Nombre = entity.Nombre,
                RazonSocial = entity.RazonSocial,
                Cuit = entity.Cuit,
                Telefono = entity.Telefono,
                Email = entity.Email,
            };
        }

        public static Proveedor ToEntity(ProveedorDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO Proveedor no puede ser null.");
            return new Proveedor()
            {
                IdProveedor = dto.Id,
                Nombre = dto.Nombre,
                RazonSocial = dto.RazonSocial,
                Cuit = dto.Cuit,
                Telefono = dto.Telefono,
                Email = dto.Email,
            };
        }

        public static IEnumerable<ProveedorDTO> ToDTOList(IEnumerable<Proveedor> entities)
        {
             return entities?.Select(ToDTO) ?? Enumerable.Empty<ProveedorDTO>();
        }

        // Método para actualizar entidad existente
        public static void UpdateEntity(Proveedor entity, ProveedorDTO dto)
        {
            if (entity == null || dto == null) return;
            entity.Nombre = dto.Nombre;
            entity.RazonSocial = dto.RazonSocial;
            entity.Cuit = dto.Cuit;
            entity.Telefono = dto.Telefono;
            entity.Email = dto.Email;
        }
    }
}
