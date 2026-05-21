using BLL.DomainDtos;
using Models;

namespace BLL.GestiónStock.Interface
{
    public interface IStockService
    {
        //Sub-categoría A: StockService (Operativo / Escritura y Lectura Core)
        //Maneja los saldos consolidados y la creación física de los lotes.Es el único que usa los métodos de inserción(Add) de la base de datos de stock.
        
        void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal);
        void RegistrarStockPorOc(Guid idProducto, int cantidadComprada, decimal costoPactado, string nroRemitoOc, Guid idSucursal);
        IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal);
    }
}
