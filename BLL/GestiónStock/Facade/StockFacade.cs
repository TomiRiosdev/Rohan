using BLL.DomainDtos;
using BLL.GestiónStock.Interface;
using System.Collections.Generic;


namespace BLL.GestiónStock.Facade
{
    public class StockFacade : IFacade
    {
        private readonly IStockService _stockService;
        private readonly IKardexService _kardexService;
        private readonly IMermaService _mermaService;
        private readonly ITraspasoService _traspasoService;

        // El constructor unifica el subsistema mediante inyección
        public StockFacade
        (
            IStockService stockService,
            IKardexService kardexService, 
            IMermaService mermaService,
            ITraspasoService traspasoService
        )
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _kardexService = kardexService ?? throw new ArgumentNullException(nameof(kardexService));
            _mermaService = mermaService ?? throw new ArgumentNullException(nameof(mermaService));
            _traspasoService = traspasoService ?? throw new ArgumentNullException(nameof(traspasoService));
        }
        // Delegados hacia StockService
        public void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal, string usuarioNombre)
            => _stockService.RegistrarStockManual(stockDto, idSucursal, usuarioNombre);

        public void RegistrarIngresoPorOrdenCompra(Guid idOrdenCompra, Guid idSucursal, string usuarioNombre, List<RecepcionMercaderiaDTO> detalleRecepcion)
            => _stockService.RegistrarIngresoPorOrdenCompra(idOrdenCompra, idSucursal, usuarioNombre, detalleRecepcion);

        public void RegistrarMermaLote(Guid idLote, int cantidadABajar, string observaciones, Guid idSucursal)
            => _stockService.RegistrarMermaLote(idLote, cantidadABajar, observaciones, idSucursal);

        public IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal)
            => _stockService.ObtenerConsolidadoPorSucursal(idSucursal);

        public ConfiguracionAlertasDTO ObtenerAlertasPorProducto(Guid idProducto)
            => _mermaService.ObtenerAlertasPorProducto(idProducto);

        public void GuardarConfiguracionAlertas(ConfiguracionAlertasDTO dto)
            => _mermaService.GuardarConfiguracionAlertas(dto);

        public IEnumerable<InventarioAlertaDTO> ObtenerTableroAlertas(Guid idSucursal)
            => _mermaService.ObtenerAlertasInventario(idSucursal);

        public List<LoteDetalleVencimientoDTO> ObtenerLotesPorProducto(Guid idProducto, Guid idSucursal)
            => _mermaService.ObtenerLotesPorProducto(idProducto, idSucursal);

        public IEnumerable<MovimientoStockDTO> ObtenerHistorialKardex(Guid idSucursal, DateTime desde, DateTime hasta)
            => _kardexService.ObtenerHistorial(idSucursal, desde, hasta);

        public void RegistrarEgresoManualLote(Guid idLote, int cantidadADescontar, string observaciones, Guid idSucursal, string usuarioNombre)
          => _stockService.RegistrarEgresoManualLote(idLote, cantidadADescontar, observaciones, idSucursal, usuarioNombre);

        public void GenerarTraspasoDesdeSolicitud(Guid idSucursalOrigen, Guid idSolicitud)
            => _traspasoService.GenerarTraspasoDesdeSolicitud(idSucursalOrigen, idSolicitud);

    }
}
