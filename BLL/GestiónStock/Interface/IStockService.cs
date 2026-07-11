using BLL.DomainDtos;
using Models;

namespace BLL.GestiónStock.Interface
{
    public interface IStockService
    {
        //Sub-categoría A: StockService (Operativo / Escritura y Lectura Core)
        //Maneja los saldos consolidados y la creación física de los lotes.Es el único que usa los métodos de inserción(Add) de la base de datos de stock.
        
        void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal, string usuarioNombre);
        void RegistrarIngresoPorOrdenCompra(Guid idOrdenCompra, Guid idSucursal, string usuarioNombre, List<RecepcionMercaderiaDTO> detalleRecepcion);
        void RegistrarMermaLote(Guid idLote, int cantidadABajar, string observaciones, Guid idSucursal);
        void RegistrarEgresoManualLote(Guid idLote, int cantidadADescontar, string observaciones, Guid idSucursal, string usuarioNombre);
        IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal);
    }
}
 