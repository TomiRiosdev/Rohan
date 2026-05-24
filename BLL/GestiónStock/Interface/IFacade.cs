using BLL.DomainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónStock.Interface
{
    public interface IStockFacade
    {
        // Delegado hacia StockService
        void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal);
        IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal);

        // Delegado hacia KardexService
        IEnumerable<MovimientoStockDTO> ObtenerHistorialKardex(Guid idSucursal, DateTime desde, DateTime hasta);

        // Delegado hacia MermaService
        IEnumerable<InventarioAlertaDTO> ObtenerTableroAlertas(Guid idSucursal);
    }
}
