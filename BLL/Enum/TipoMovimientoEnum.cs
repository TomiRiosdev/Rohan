using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Enum
{
    public enum TipoMovimientoEnum
    {
        IngresoManual = 1,
        IngresoPorCompra = 2,
        EgresoPorVenta = 3,
        EgresoPorMerma = 4,
        Transferencia = 5
    }
    public static class TipoMovimientoExtensions
    {
        // Diccionario estático que empareja cada Enum con su GUID exacto de SQL Server
        private static readonly Dictionary<TipoMovimientoEnum, Guid> _IdMapa = new()
    {
        { TipoMovimientoEnum.EgresoPorMerma, Guid.Parse("FD17622A-FF99-414C-8A31-356D51F2BC70") },
        { TipoMovimientoEnum.EgresoPorVenta, Guid.Parse("45D7E0B0-8695-489C-8CC5-47C0E4B1761C") },
        { TipoMovimientoEnum.Transferencia,  Guid.Parse("5B032DB3-013C-4A9F-8F0A-56E954AE5511") },
        { TipoMovimientoEnum.IngresoPorCompra, Guid.Parse("1304A73E-73D9-496E-8269-769827E3CE2C") },
        { TipoMovimientoEnum.IngresoManual,   Guid.Parse("D97CB7FF-EE41-496E-86C5-C7C115E68250") }
    };

        // Método de extensión para obtener el GUID de forma directa y fluida
        public static Guid ToGuid(this TipoMovimientoEnum tipo)
        {
            return _IdMapa[tipo];
        }
    }
}
