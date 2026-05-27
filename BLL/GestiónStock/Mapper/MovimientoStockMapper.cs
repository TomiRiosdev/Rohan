using BLL.DomainDtos;
using Models;

namespace BLL.GestiónStock.Mapper
{
    public static class MovimientoStockMapper
    {
        public static MovimientoStockDTO ToDTO(this MovimientosStock entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad MovimientosStock no puede ser nula.");

            return new MovimientoStockDTO
            {
                IdMovimiento = entity.IdMovimiento,
                IdSucursal = entity.IdSucursal ?? Guid.Empty,
                IdLote = entity.IdLote ?? Guid.Empty,
                NumeroLote = entity.IdLoteNavigation?.NumeroLote ?? "Sin Lote",
           

                // Mapeamos la descripción del tipo (ej: "Ingreso Manual", "Venta")
                TipoMovimientoTexto = entity.IdTipoMovimientoNavigation?.Descripcion ?? "Desconocido",

                Cantidad = entity.Cantidad ?? 0,
                FechaMovimiento = entity.FechaMovimiento ?? DateTime.Now,
                Observaciones = entity.Observaciones ?? string.Empty,

                // Aplanamos los datos del producto navegando a través del lote
                IdProducto = entity.IdLoteNavigation?.IdProducto ?? Guid.Empty,
                ProductoNombre = entity.IdLoteNavigation?.IdProductoNavigation?.Nombre ?? "Producto no identificado"
            };
        }

        public static MovimientosStock ToEntity(this MovimientoStockDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El DTO MovimientoStock no puede ser nulo.");

            return new MovimientosStock
            {
                IdMovimiento = dto.IdMovimiento,
                IdSucursal = dto.IdSucursal == Guid.Empty ? (Guid?)null : dto.IdSucursal,
                IdLote = dto.IdLote == Guid.Empty ? (Guid?)null : dto.IdLote,
             
                Cantidad = dto.Cantidad,
                FechaMovimiento = dto.FechaMovimiento,
                Observaciones = dto.Observaciones
                // Los campos de transferencia (IdSucursalOrigen/Destino) se pueden setear en los métodos específicos del Service
            };
        }

        public static IEnumerable<MovimientoStockDTO> ToDTOList(this IEnumerable<MovimientosStock> entidades)
        {
            return entidades?.Select(ToDTO) ?? Enumerable.Empty<MovimientoStockDTO>();
        }
    }
}
