using BLL.DomainDtos;
using BLL.Enum;
using Models;

namespace BLL.GestiónStock.Mapper
{
    public static class MovimientoStockMapper
    {
        public static MovimientoStockDTO ToDTO(this MovimientosStock entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "La entidad MovimientosStock no puede ser nula.");

            // 1.Resolución segura del texto del Tipo de Movimiento (Fallback por Enum si falla Include)
            string tipoTexto = "Desconocido";
            if (entity.IdTipoMovimiento.HasValue)
            {
                TipoMovimientoEnum tipoEnum = (TipoMovimientoEnum)entity.IdTipoMovimiento.Value;
                tipoTexto = tipoEnum.ToString();
            }

            // 2.AUDITORÍA DINÁMICA: Extraer el Rol/Usuario inyectado en los corchetes
            string observacionesDb = entity.Observaciones ?? string.Empty;
            string usuarioDetectado = "Sistema / Operario"; // Valor por defecto en caso de registros viejos o automáticos

            if (observacionesDb.StartsWith("["))
            {
                int finCorchete = observacionesDb.IndexOf("]");
                if (finCorchete > 1)
                {
                    // Extraemos lo que está adentro del corchete: "Gerente de Sucursal (Juan)"
                    usuarioDetectado = observacionesDb.Substring(1, finCorchete - 1);

                    // Limpiamos la cadena de observaciones quitando el prefijo para que en la grilla se lea limpio
                    observacionesDb = observacionesDb.Substring(finCorchete + 1).Trim();
                }
            }

            // 3. Mapeo final al DTO enriquecido
            return new MovimientoStockDTO
            {
                IdMovimiento = entity.IdMovimiento,
                IdSucursal = entity.IdSucursal ?? Guid.Empty,
                IdLote = entity.IdLote ?? Guid.Empty,
                NumeroLote = entity.IdLoteNavigation?.NumeroLote ?? "Sin Lote",
                TipoMovimientoTexto = entity.IdTipoMovimientoNavigation?.Descripcion ?? tipoTexto,

                Cantidad = entity.Cantidad ?? 0,
                FechaMovimiento = entity.FechaMovimiento ?? DateTime.Now,
                Observaciones = observacionesDb, // Notas limpias sin el texto del usuario metido a la fuerza

                IdProducto = entity.IdLoteNavigation?.IdProducto ?? Guid.Empty,
                ProductoNombre = entity.IdLoteNavigation?.IdProductoNavigation?.Nombre ?? "Producto no identificado",
                CodigoSku = (int?)entity.IdLoteNavigation?.IdProductoNavigation?.CodigoSku,

                UsuarioNombre = usuarioDetectado, //Ahora expone dinámicamente el Rol y Nombre del operario

                DocumentoReferencia = string.IsNullOrEmpty(observacionesDb) ? "-" :
                    (observacionesDb.Contains("OC-") ? observacionesDb : "-")
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
            
            };
        }

        public static IEnumerable<MovimientoStockDTO> ToDTOList(this IEnumerable<MovimientosStock> entidades)
        {
            return entidades?.Select(ToDTO) ?? Enumerable.Empty<MovimientoStockDTO>();
        }
    }
}
