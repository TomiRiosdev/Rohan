using BLL.DomainDtos;

namespace BLL.GestiónStock.Interface
{
    public interface IFacade
    {
        // OPERACIONES DE STOCK CORE (Hacia StockService)
            void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal, string usuarioNombre);
            void RegistrarIngresoPorOrdenCompra(Guid idOrdenCompra, Guid idSucursal, string usuarioNombre, List<RecepcionMercaderiaDTO> detalleRecepcion);
            void RegistrarMermaLote(Guid idLote, int cantidadABajar, string observaciones, Guid idSucursal);
            IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal);
            void RegistrarEgresoManualLote(Guid idLote, int cantidadADescontar, string observaciones, Guid idSucursal, string usuarioNombre);

            // TABLERO OPERATIVO Y LOGÍSTICA SANITARIA (Hacia MermaService)
            ConfiguracionAlertasDTO ObtenerAlertasPorProducto(Guid idProducto);
            void GuardarConfiguracionAlertas(ConfiguracionAlertasDTO dto);
            IEnumerable<InventarioAlertaDTO> ObtenerTableroAlertas(Guid idSucursal);
            List<LoteDetalleVencimientoDTO> ObtenerLotesPorProducto(Guid idProducto, Guid idSucursal);

            // LIBRO CONTABLE DE AUDITORÍA (Hacia KardexService)
            IEnumerable<MovimientoStockDTO> ObtenerHistorialKardex(Guid idSucursal, DateTime desde, DateTime hasta);

            // GESTIÓN DE TRASPASOS (Hacia TraspasoService)
            void GenerarTraspasoDesdeSolicitud(Guid idSucursalOrigen, Guid idSolicitud);

    }
}
