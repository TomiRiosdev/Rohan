using BLL.DomainDtos;
using Models;


namespace BLL.GestiónCompra.Mapper
{
    public static class SolicitudPedidoMapper
    {
        #region Mapeos de Cabecera (SolicitudPedido <-> SolicitudPedidoDTO)

        public static SolicitudPedidoDTO ToDTO(this SolicitudPedido entity)
        {
            if (entity == null) return null!;

            return new SolicitudPedidoDTO
            {
                IdSolicitudPedido = entity.IdSolicitudPedido,
                IdUsuario = entity.IdUsuario,
                IdSucursal = entity.IdSucursal,
                NroSolicitud = entity.NroSolicitud ?? 0,
                FechaSolicitud = entity.FechaSolicitud ?? DateTime.Now,
                IdEstadoSolicitud = entity.IdEstadoSolicitud ?? 1,
                EstadoNombre = entity.IdEstadoSolicitudNavigation?.Descripcion ?? "Pendiente",

                // Mapeo en cascada de los renglones del detalle
                Detalles = entity.SolicitudPedidoDetalle != null
                    ? entity.SolicitudPedidoDetalle.Select(d => d.ToDTO(entity.IdSolicitudPedido)).ToList()
                    : new List<SolicitudPedidoDetalleDTO>()
            };
        }

        public static SolicitudPedido ToEntity(this SolicitudPedidoDTO dto)
        {
            if (dto == null) return null!;

            var entity = new SolicitudPedido
            {
                IdSolicitudPedido = dto.IdSolicitudPedido,
                IdUsuario = dto.IdUsuario,
                IdSucursal = dto.IdSucursal,
                NroSolicitud = dto.NroSolicitud,
                FechaSolicitud = dto.FechaSolicitud,
                IdEstadoSolicitud = dto.IdEstadoSolicitud
            };

            // Mapeamos los detalles pasándole el ID de la cabecera para mantener la integridad referencial
            if (dto.Detalles != null)
            {
                entity.SolicitudPedidoDetalle = dto.Detalles
                    .Select(d => d.ToEntity(dto.IdSolicitudPedido))
                    .ToList();
            }

            return entity;
        }

        #endregion

        #region Mapeos de Detalle (SolicitudPedidoDetalle <-> SolicitudPedidoDetalleDTO)

        /// <summary>
        /// Traduce un renglón físico de la base de datos a un DTO comercial para la interfaz de Rohan.
        /// </summary>
        public static SolicitudPedidoDetalleDTO ToDTO(this SolicitudPedidoDetalle entity, Guid idCabecera)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad SolicitudPedidoDetalle no puede ser nula.");

            return new SolicitudPedidoDetalleDTO
            {
                IdProducto = entity.IdProducto ?? Guid.Empty,
                Renglon = entity.Renglon ?? 0,
                CantidadBultosSolicitada = entity.Cantidad ?? 0,

                // Mapeos ricos extraídos de la navegación del Producto maestro (Evitan cortocircuitos por Nulls)
                CodigoSku = entity.IdProductoNavigation?.CodigoSku ?? 0,
                ProductoNombre = entity.IdProductoNavigation?.Nombre ?? "Materia Prima No Identificada",
                UnidadesPorBulto = entity.IdProductoNavigation?.CantidadPorBulto ?? 1,

                // Si en el modelo de base de datos no existe PresentacionTipo, dejamos por defecto "Caja" 
                // para que no rompa tu DTO industrial.
                PresentacionTipo = "Caja"
            };
        }

        /// <summary>
        /// Traduce un renglón del carrito de la UI a una entidad física lista para insertarse en SQL Server.
        /// </summary>
        public static SolicitudPedidoDetalle ToEntity(this SolicitudPedidoDetalleDTO dto, Guid idCabecera)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO SolicitudPedidoDetalle no puede ser nulo.");

            return new SolicitudPedidoDetalle
            {
                //  Forzamos el calce de llaves primarias compuestas/relacionales
                IdSolicitudPedido = idCabecera,
                IdSolicitud = idCabecera, // Asignamos ambos campos por las variantes de tu modelo

                IdProducto = dto.IdProducto,
                Renglon = dto.Renglon,
                Cantidad = dto.CantidadBultosSolicitada

                // Nota: Si tu tabla 'SolicitudPedidoDetalle' física en Models llega a tener la columna 
                // PresentacionTipo más adelante, descomentás esta línea:
                // PresentacionTipo = dto.PresentacionTipo
            };
        }

        #endregion
    }
}
