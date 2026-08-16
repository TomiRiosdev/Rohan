using BLL.DomainDtos;
using Models;
using System;

namespace BLL.GestiónStock.Mapper
{
    public static class OrdenTraspasoMapper
    {
        public static OrdenTraspasoDTO ToDTO(this OrdenTraspaso entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad OrdenTraspaso no puede ser nula.");

            return new OrdenTraspasoDTO
            {
                IdOrdenTraspaso = entity.IdOrdenTraspaso,
                NroTraspaso = entity.NroTraspaso ?? 0,

                IdSucursalOrigen = entity.IdSucursalOrigen ?? Guid.Empty,
                SucursalOrigenNombre = entity.IdSucursalOrigenNavigation?.Nombre ?? "Sin Origen",

                IdSucursalDestino = entity.IdSucursalDestino ?? Guid.Empty,
                SucursalDestinoNombre = entity.IdSucursalDestinoNavigation?.Nombre ?? "Sin Destino",

                IdSolicitudPedido = entity.IdSolicitudPedido ?? Guid.Empty,

                IdEstado = entity.IdEstado ?? 0,
                EstadoTexto = entity.IdEstadoSolicitudNavigation?.Descripcion ?? "Desconocido",

                FechaEmision = entity.FechaEmision,
                FechaRecepcion = entity.FechaRecepcion,
                Observaciones = entity.Observaciones ?? string.Empty,

                // Mapeamos los detalles si vienen incluidos en la consulta
                Detalles = entity.OrdenTraspasoDetalle != null
                            ? entity.OrdenTraspasoDetalle.Select(d => d.ToDTO()).ToList()
                            : new List<OrdenTraspasoDetalleDTO>()
            };
        }

        public static OrdenTraspasoDetalleDTO ToDTO(this OrdenTraspasoDetalle entity)
        {
            if (entity == null) return null;

            return new OrdenTraspasoDetalleDTO
            {
                IdOrdenTraspasoDetalle = entity.IdOrdenTraspasoDetalle,
                IdOrdenTraspaso = entity.IdOrdenTraspaso ?? Guid.Empty,
                Renglon = entity.Renglon ?? 0,

                IdProducto = entity.IdProducto ?? Guid.Empty,
                ProductoNombre = entity.IdProductoNavigation?.Nombre ?? "Producto desconocido",
                CodigoSku = entity.IdProductoNavigation?.CodigoSku ?? 0,

                // Mapeamos el coeficiente logístico
                CantidadPorBulto = entity.IdProductoNavigation?.CantidadPorBulto ?? 1,

                // Inicialmente, lo que se guardó en CantidadEnviada es lo que se solicitó
                CantidadSolicitada = entity.CantidadEnviada ?? 0,
                CantidadEnviada = entity.CantidadEnviada ?? 0,
                CantidadRecibida = entity.CantidadRecibida ?? 0,

                IdLoteOrigen = entity.IdLoteOrigen,
                NumeroLoteOrigen = entity.IdLoteOrigenNavigation?.NumeroLote ?? "Sin Asignar"
            };
        }

        public static IEnumerable<OrdenTraspasoDTO> ToDTOList(this IEnumerable<OrdenTraspaso> entidades)
        {
            return entidades?.Select(ToDTO) ?? Enumerable.Empty<OrdenTraspasoDTO>();
        }
    }
}
