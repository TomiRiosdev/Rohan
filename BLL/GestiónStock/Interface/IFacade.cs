using BLL.DomainDtos;

namespace BLL.GestiónStock.Interface
{
    public interface IFacade
    {
        // OPERACIONES DE STOCK CORE (Hacia StockService)
            void RegistrarStockManual(StockPorSucursalDTO stockDto, Guid idSucursal, string usuarioNombre);
            void RegistrarStockPorOc(Guid idProducto, int cantidadComprada, decimal costoPactado, string nroRemitoOc, Guid idSucursal);
            void RegistrarMermaLote(Guid idLote, int cantidadABajar, string observaciones, Guid idSucursal);
            IEnumerable<StockPorSucursalDTO> ObtenerConsolidadoPorSucursal(Guid idSucursal);

            // TABLERO OPERATIVO Y LOGÍSTICA SANITARIA (Hacia MermaService)
            ConfiguracionAlertasDTO ObtenerAlertasPorProducto(Guid idProducto);
            void GuardarConfiguracionAlertas(ConfiguracionAlertasDTO dto);
            IEnumerable<InventarioAlertaDTO> ObtenerTableroAlertas(Guid idSucursal);
            List<LoteDetalleVencimientoDTO> ObtenerLotesPorProducto(Guid idProducto, Guid idSucursal);

            // LIBRO CONTABLE DE AUDITORÍA (Hacia KardexService)
            IEnumerable<MovimientoStockDTO> ObtenerHistorialKardex(Guid idSucursal, DateTime desde, DateTime hasta);
        
    }
}
