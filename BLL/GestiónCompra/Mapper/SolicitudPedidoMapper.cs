using BLL.DomainDtos;
using Models;


namespace BLL.GestiónCompra.Mapper
{
    public static class SolicitudPedidoMapper
    {
        // --- MAPEOS DEL DETALLE (RENGLONES) ---
        public static SolicitudPedidoDetalleDTO ToDTO(this SolicitudPedidoDetalle entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad SolicitudPedidoDetalle no puede ser null.");

            return new SolicitudPedidoDetalleDTO
            {
                IdSolicitudPedido = entity.IdSolicitudPedido,
                IdProducto = entity.IdProducto ?? Guid.Empty,
                ProductoNombre = entity.IdProductoNavigation?.Nombre ?? "Producto no identificado",
                Renglon = entity.Renglon ?? 0,
                Cantidad = entity.Cantidad ?? 0
            };
        }

        public static SolicitudPedidoDetalle ToEntity(this SolicitudPedidoDetalleDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO SolicitudPedidoDetalle no puede ser null.");

            return new SolicitudPedidoDetalle
            {
                IdSolicitudPedido = dto.IdSolicitudPedido,
                IdProducto = dto.IdProducto,
                Renglon = dto.Renglon,
                Cantidad = dto.Cantidad
            };
        }

        // --- MAPEOS DE LA CABECERA (MAESTRO) ---
        public static SolicitudPedidoDTO ToDTO(this SolicitudPedido entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad SolicitudPedido no puede ser null.");

            var dto = new SolicitudPedidoDTO
            {
                IdSolicitudPedido = entity.IdSolicitudPedido,
                NroSolicitud = entity.NroSolicitud ?? 0,
                IdUsuario = entity.IdUsuario ?? Guid.Empty,
                IdSucursal = entity.IdSucursal ?? Guid.Empty,
                FechaSolicitud = entity.FechaSolicitud ?? DateTime.Now,
                IdEstadoSolicitud = entity.IdEstadoSolicitud ?? Guid.Empty,
                EstadoDescripcion = entity.IdEstadoSolicitudNavigation?.Descripcion ?? "Sin Estado"
            };

            // Mapeamos recursivamente la colección de hijos si el repositorio los incluyó
            if (entity.SolicitudPedidoDetalles != null && entity.SolicitudPedidoDetalles.Any())
            {
                dto.Detalles = entity.SolicitudPedidoDetalles.Select(d => d.ToDTO()).ToList();
            }

            return dto;
        }

        public static SolicitudPedido ToEntity(this SolicitudPedidoDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO SolicitudPedido no puede ser null.");

            var entity = new SolicitudPedido
            {
                IdSolicitudPedido = dto.IdSolicitudPedido,
                IdUsuario = dto.IdUsuario,
                IdSucursal = dto.IdSucursal,
                FechaSolicitud = dto.FechaSolicitud,
                IdEstadoSolicitud = dto.IdEstadoSolicitud
               
            };

            // Transformamos los DTOs hijos a entidades físicas listas para EF
            if (dto.Detalles != null)
            {
                entity.SolicitudPedidoDetalles = dto.Detalles.Select(d => d.ToEntity()).ToList();
            }

            return entity;
        }

        public static IEnumerable<SolicitudPedidoDTO> ToDTOList(this IEnumerable<SolicitudPedido> solicitudes)
        {
            return solicitudes?.Select(ToDTO) ?? Enumerable.Empty<SolicitudPedidoDTO>();
        }
    }
}
