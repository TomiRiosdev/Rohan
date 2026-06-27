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
            string observacionesDb = (entity.Observaciones ?? string.Empty).Trim();
            string usuarioDetectado = "Gerente de Sucursal"; // Valor por defecto 

            if (observacionesDb.Contains("[") && observacionesDb.Contains("]"))
            {
                int inicioCorchete = observacionesDb.IndexOf("[");
                int finCorchete = observacionesDb.IndexOf("]");

                if (finCorchete > inicioCorchete)
                {
                    // Extraemos puramente el contenido: "Gerente de Sucursal (Juan)"
                    usuarioDetectado = observacionesDb.Substring(inicioCorchete + 1, finCorchete - inicioCorchete - 1);

                    // Recortamos la cadena para dejar solo el mensaje del usuario
                    observacionesDb = observacionesDb.Substring(finCorchete + 1).Trim();
                }
            }

            if (string.IsNullOrWhiteSpace(observacionesDb) || observacionesDb.StartsWith("Ajuste manual"))
            {
                observacionesDb = "Ajuste manual de stock";
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
                    Observaciones = observacionesDb,
                    UsuarioNombre = entity.UsuarioNombre ?? "Sistema",
                    IdProducto = entity.IdLoteNavigation?.IdProducto ?? Guid.Empty,
                    ProductoNombre = entity.IdLoteNavigation?.IdProductoNavigation?.Nombre ?? "Producto no identificado",
                    CodigoSku = (int?)entity.IdLoteNavigation?.IdProductoNavigation?.CodigoSku,
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
