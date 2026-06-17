using BLL.DomainDtos;
using Models;
using System;


namespace BLL.GestiónCompra.Mapper
{
    public static class OrdenCompraMapper
    {
        #region Mapeos de Cabecera (OrdenCompra <-> OrdenCompraDTO)

        public static OrdenCompraDTO ToDTO(this OrdenCompra entity)
        {
            if (entity == null) return null!;

            return new OrdenCompraDTO
            {
                IdOrdenCompra = entity.IdOrdenCompra,
                IdProveedor = entity.IdProveedor,
                IdUsuario = entity.IdUsuario,
                FechaOc = entity.FechaOc ?? DateTime.Now,
                IdEstadoOc = entity.IdEstadoOc ?? 1,
                EstadoDescripcion = entity.IdEstadoSolicitudNavigation?.Descripcion ?? "Pendiente",
                NroOrdenCompra = entity.NroSolicitud ?? 0,
                //NombreUsuario = entity.IdUsuarioNavigation?.NombreUsuario ?? "Sistema / Automático",

                // Datos extendidos del proveedor para la UI/Documento
                RazonSocialProveedor = entity.IdProveedorNavigation?.RazonSocial ?? "Proveedor no identificado",
                CuitProveedor = entity.IdProveedorNavigation?.Cuit ?? string.Empty,

                // Mapeo en cascada de los renglones
                Detalles = entity.OrdenCompraDetalle != null
                    ? entity.OrdenCompraDetalle.Select(d => d.ToDTO()).ToList()
                    : new List<OrdenCompraDetalleDTO>()
            };
        }

        public static OrdenCompra ToEntity(this OrdenCompraDTO dto, Guid idOcGarantizado)
        {
            if (dto == null) return null!;

            var entity = new OrdenCompra
            {
                IdOrdenCompra = idOcGarantizado,
                IdProveedor = dto.IdProveedor,
                IdUsuario = dto.IdUsuario,
                IdEstadoOc = dto.IdEstadoOc,
                FechaOc = dto.FechaOc,
                CostoTotal = dto.CostoTotal, // Guardamos el acumulado total calculado
                NroSolicitud = dto.NroOrdenCompra // Usamos esta columna como el número secuencial de la OC
            };

            // Mapeamos los renglones del detalle inyectándoles el ID de la cabecera madre
            if (dto.Detalles != null)
            {
                entity.OrdenCompraDetalle = dto.Detalles
                    .Select(d => d.ToEntity(idOcGarantizado))
                    .ToList();
            }

            return entity;
        }

        #endregion

        #region Mapeos de Detalle (OrdenCompraDetalle <-> OrdenCompraDetalleDTO)

        public static OrdenCompraDetalleDTO ToDTO(this OrdenCompraDetalle entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new OrdenCompraDetalleDTO
            {
                IdOrdenCompraDetalle = entity.IdOrdenCompraDetalle,
                IdProducto = entity.IdProducto ?? Guid.Empty,
                CantidadPedida = entity.CantidadPedida ?? 0,
                CantidadRecibida = entity.CantidadRecibida ?? 0,
                PrecioPactado = entity.PrecioPactado ?? 0,
                Renglon = entity.Renglon ?? 0,

                // Mapeos ricos de navegación desde Producto
                CodigoSku = entity.IdProductoNavigation?.CodigoSku ?? 0,
                ProductoNombre = entity.IdProductoNavigation?.Nombre ?? "Materia Prima",
                UnidadesPorBulto = entity.IdProductoNavigation?.CantidadPorBulto ?? 1
            };
        }

        public static OrdenCompraDetalle ToEntity(this OrdenCompraDetalleDTO dto, Guid idOcGarantizado)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            Guid idDetalleNuevo = Guid.NewGuid();

            var detalleEntity = new OrdenCompraDetalle
            {
                IdOrdenCompraDetalle = idDetalleNuevo,
                IdOrdenCompra = idOcGarantizado,
                IdProducto = dto.IdProducto,
                CantidadPedida = dto.CantidadPedida,
                CantidadRecibida = 0, // Arranca siempre en cero bultos recibidos
                PrecioPactado = dto.PrecioPactado,
                Renglon = dto.Renglon
            };

            // Si el renglón nació a partir de una Solicitud de Pedido,
            // armamos físicamente la entidad intermedia de Vinculo de forma interna en el mapper
            if (dto.IdSolicitudPedidoDetalleOrigen.HasValue)
            {
                detalleEntity.VinculoSolicitudOc.Add(new VinculoSolicitudOc
                {
                    IdVinculoSolicitudOc = Guid.NewGuid(),
                    IdOrdenCompraDetalle = idDetalleNuevo,
                    IdSolicitudPedidoDetalle = dto.IdSolicitudPedidoDetalleOrigen.Value,
                    CantidadAsignada = dto.CantidadAsignadaDesdeSolicitud ?? dto.CantidadPedida
                });
            }

            return detalleEntity;
        }

        #endregion
    }
}
