using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Enum
{
    public enum TipoMovimientoEnum
    {
        IngresoManual = 1,
        IngresoPorCompra = 2,
        EgresoManual = 3,
        EgresoPorMerma = 4,
        EgresoPorTransferencia = 5, 
        IngresoPorTransferencia = 6 
    }
    
}
