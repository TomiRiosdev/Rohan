using BLL.DomainDtos;
using BLL.GestiónStock.Interface;


namespace BLL.GestiónStock.Facade
{
    public class StockFacade : IFacade
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
        // Delegados hacia StockService
        public void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal)
            => _stockService.RegistrarStockManual(stockDto, idSucursal);

        public void RegistrarStockPorOc(Guid idProducto, int cantidadComprada, decimal costoPactado, string nroRemitoOc, Guid idSucursal)
            => _stockService.RegistrarStockPorOc(idProducto, cantidadComprada, costoPactado, nroRemitoOc, idSucursal);

        public void RegistrarMermaLote(Guid idLote, int cantidadABajar, string observaciones, Guid idSucursal)
            => _stockService.RegistrarMermaLote(idLote, cantidadABajar, observaciones, idSucursal);

        public IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal)
            => _stockService.ObtenerConsolidadoPorSucursal(idSucursal);

        // 🚀 Delegados hacia MermaService
        public ConfiguracionAlertasDTO ObtenerAlertasPorProducto(Guid idProducto)
            => _mermaService.ObtenerAlertasPorProducto(idProducto);

        public void GuardarConfiguracionAlertas(ConfiguracionAlertasDTO dto)
            => _mermaService.GuardarConfiguracionAlertas(dto);

        public IEnumerable<InventarioAlertaDTO> ObtenerTableroAlertas(Guid idSucursal)
            => _mermaService.ObtenerAlertasInventario(idSucursal);

        public List<LoteDetalleVencimientoDTO> ObtenerLotesPorProducto(Guid idProducto, Guid idSucursal)
            => _mermaService.ObtenerLotesPorProducto(idProducto, idSucursal);

        // 🚀 Delegados hacia KardexService
        public IEnumerable<MovimientoStockDTO> ObtenerHistorialKardex(Guid idSucursal, DateTime desde, DateTime hasta)
            => _kardexService.ObtenerHistorial(idSucursal, desde, hasta);
    }
}
