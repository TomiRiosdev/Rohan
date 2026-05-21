using BLL.DomainDtos;
using BLL.Enum;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónStock.Interface
{
    public interface IKardexService
    {
     /// Sub-categoría B: KardexService (Auditoría / Solo Escritura de Historial)
     // Su única responsabilidad es insertar filas en la tabla MovimientosStock.Es un servicio esclavo.
     // StockService lo recibe por inyección de dependencias en su constructor y lo llama internamente cada vez que suma o resta mercadería.
        
        void RegistrarMovimiento(Guid idSucursal, Guid idLote, TipoMovimientoEnum tipo, int cantidad, string observaciones);
        IEnumerable<MovimientoStockDTO> ObtenerHistorial(Guid idSucursal, DateTime desde, DateTime hasta);
    }
}
