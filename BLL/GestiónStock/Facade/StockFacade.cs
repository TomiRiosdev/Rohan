using BLL.DomainDtos;
using BLL.GestiónStock.Interface;


namespace BLL.GestiónStock.Facade
{
    public class StockFacade : IStockFacade
    {
        private readonly IStockService _stockService;
        private readonly IKardexService _kardexService;
        private readonly IMermaService _mermaService;

        // El constructor unifica el subsistema mediante inyección
        public StockFacade
        (
            IStockService stockService,
            IKardexService kardexService, 
            IMermaService mermaService
        )
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _kardexService = kardexService ?? throw new ArgumentNullException(nameof(kardexService));
            _mermaService = mermaService ?? throw new ArgumentNullException(nameof(mermaService));
        }

        public void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal)
        {
             _stockService.RegistrarStockManual(stockDto, idSucursal);
        }

        public IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal)
        {
            return _stockService.ObtenerConsolidadoPorSucursal(idSucursal);
        }

        public IEnumerable<MovimientoStockDTO> ObtenerHistorialKardex(Guid idSucursal, DateTime desde, DateTime hasta)
        {
            return _kardexService.ObtenerHistorial(idSucursal, desde, hasta);
        }

        public IEnumerable<InventarioAlertaDTO> ObtenerTableroAlertas(Guid idSucursal)
        {
            return _mermaService.ObtenerAlertasInventario(idSucursal);
        }
    }
}
