using BLL.DomainDtos;
using Models;


namespace BLL.GestiónCompra.Mapper
{
    public static class SolicitudPedidoMapper
    {
        public static SolicitudPedidoDTO ToDTO(this SolicitudPedido entity)
        {
            if (entity == null) return null;

            return new SolicitudPedidoDTO
            {
                IdSolicitudPedido = entity.IdSolicitudPedido,
                IdUsuario = entity.IdUsuario,
                IdSucursal = entity.IdSucursal,
                NroSolicitud = entity.NroSolicitud ?? 0,
                FechaSolicitud = entity.FechaSolicitud ?? DateTime.Now,
                IdEstadoSolicitud = entity.IdEstadoSolicitud ?? 1,
                EstadoNombre = entity.IdEstadoSolicitudNavigation?.Descripcion ?? "Pendiente",
                UsuarioNombre = entity.IdUsuarioNavigation?.Nombre, //  Traemos el operario que lo emitió

                Detalles = entity.SolicitudPedidoDetalle != null
                    ? entity.SolicitudPedidoDetalle.Select(d => d.ToDTO()).ToList()
                    : new List<SolicitudPedidoDetalleDTO>()
            };
        }

        public static SolicitudPedido ToEntity(this SolicitudPedidoDTO dto)
        {
            if (dto == null) return null;

            return new SolicitudPedido
            {
                IdSolicitudPedido = dto.IdSolicitudPedido,
                IdUsuario = dto.IdUsuario,
                IdSucursal = dto.IdSucursal,
                NroSolicitud = dto.NroSolicitud,
                FechaSolicitud = dto.FechaSolicitud,
                IdEstadoSolicitud = dto.IdEstadoSolicitud,
                SolicitudPedidoDetalle = dto.Detalles != null
                    ? dto.Detalles.Select(d => d.ToEntity()).ToList()
                    : new List<SolicitudPedidoDetalle>()
            };
        }

        public static SolicitudPedidoDetalleDTO ToDTO(this SolicitudPedidoDetalle entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad SolicitudPedidoDetalle no puede ser null.");

            return new SolicitudPedidoDetalleDTO
            {
                IdProducto = entity.IdProducto ?? Guid.Empty,
                CodigoSku = entity.IdProductoNavigation?.CodigoSku ?? 0,
                ProductoNombre = entity.IdProductoNavigation?.Nombre ?? "Producto no identificado",
                Renglon = entity.Renglon ?? 0,

                // 🚀 CORRECCIÓN: Mapeamos contra tus campos específicos de Bultos del DTO
                CantidadBultosSolicitada = entity.Cantidad ?? 0,
                PresentacionTipo = entity.PresentacionTipo ?? "Caja",
                UnidadesPorBulto = entity.IdProductoNavigation?.CantidadPorBulto ?? 1
            };
        }

        public static SolicitudPedidoDetalle ToEntity(this SolicitudPedidoDetalleDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO SolicitudPedidoDetalle no puede ser null.");

            return new SolicitudPedidoDetalle
            {
                IdProducto = dto.IdProducto,
                Renglon = dto.Renglon,

                //  CORRECCIÓN: Guardamos la cantidad de bultos en el campo de la base de datos
                Cantidad = dto.CantidadBultosSolicitada,
                PresentacionTipo = dto.PresentacionTipo
            };
        }
    }
}
