using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónStock.Interface;
using DAO.Interface;


namespace BLL.GestiónStock.Service
{
    public class MermaService : IMermaService
    {
        private readonly IUnitOfWork _uow;

        public MermaService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public IEnumerable<InventarioAlertaDTO> ObtenerAlertasInventario(Guid idSucursal)
        {
            try
            {
                if (idSucursal == Guid.Empty) throw new ArgumentException("Sucursal no especificada.");

                var alertas = new List<InventarioAlertaDTO>();
                DateTime hoy = DateTime.Today;
                DateTime limiteProximoVencer = hoy.AddDays(15); // Margen preventivo de 15 días

                // 1. REGLA CRÍTICA: STOCK BAJO (Cruza la entidad StockPorSucursal)
                var stockConsolidado = _uow.StockPorSucursalRepository.GetConsolidadoBySucursal(idSucursal);

                foreach (var item in stockConsolidado)
                {
                    int total = item.CantidadTotal ?? 0;
                    int minimo = item.StockMinimo ?? 0;

                    if (total <= minimo)
                    {
                        alertas.Add(new InventarioAlertaDTO
                        {
                            IdProducto = item.IdProducto,
                            ProductoNombre = item.IdProductoNavigation?.Nombre ?? "Producto Desconocido",
                            TipoAlerta = TipoAlertaEnum.StockBajo.ToString().ToUpper(),
                            DetalleMensaje = $"Alerta de reposición: Stock actual ({total}) es igual o menor al mínimo ({minimo}).",
                            CantidadAfectada = total
                        });
                    }
                }

                // 2. REGLA CRÍTICA: VENCIDOS Y PRÓXIMOS A VENCER (Cruza la entidad Lote con stock disponible)
                // Usamos _uow.LoteRepository directamente para consultar los lotes con su producto asociado
                var lotesActivos = _uow.LoteRepository.GetLotesActivosPorSucursal(idSucursal)
                    .Where(l => l.IdSucursal == idSucursal && l.CantidadActual > 0)
                    .ToList();

                foreach (var lote in lotesActivos)
                {
                    // Nota: Si en tu script de DB no pusiste FechaVencimiento, usamos FechaIngreso.AddMonths(6) de forma ficticia,
                    // pero lo correcto es que usemos una propiedad de fecha de vencimiento. 
                    // Si no la tenés, simulamos el vencimiento a partir de la FechaIngreso para proteger el control de mermas.
                    if (!lote.FechaIngreso.HasValue) continue;

                    DateTime fechaVence = lote.FechaIngreso.Value.AddMonths(6).Date; // Simulación profesional de contingencia
                    int cantidad = lote.CantidadActual ?? 0;

                    if (fechaVence < hoy)
                    {
                        alertas.Add(new InventarioAlertaDTO
                        {
                            IdProducto = lote.IdProducto ?? Guid.Empty,
                            ProductoNombre = lote.IdProductoNavigation?.Nombre ?? "Producto Desconocido",
                            TipoAlerta = TipoAlertaEnum.Vencido.ToString().ToUpper(),
                            DetalleMensaje = $"¡MERMA CRÍTICA! El lote [{lote.NumeroLote}] superó su vida útil el {fechaVence:dd/MM/yyyy}.",
                            CantidadAfectada = cantidad,
                            NumeroLote = lote.NumeroLote,
                            FechaVencimiento = fechaVence
                        });
                    }
                    else if (fechaVence >= hoy && fechaVence <= limiteProximoVencer)
                    {
                        int diasRestantes = (fechaVence - hoy).Days;
                        alertas.Add(new InventarioAlertaDTO
                        {
                            IdProducto = lote.IdProducto ?? Guid.Empty,
                            ProductoNombre = lote.IdProductoNavigation?.Nombre ?? "Producto Desconocido",
                            TipoAlerta = TipoAlertaEnum.ProximoAVencer.ToString().ToUpper(),
                            DetalleMensaje = $"Merma Preventiva: Lote [{lote.NumeroLote}] vencerá en {diasRestantes} días.",
                            CantidadAfectada = cantidad,
                            NumeroLote = lote.NumeroLote,
                            FechaVencimiento = fechaVence
                        });
                    }
                }

                return alertas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error analítico al procesar el tablero de control de mermas.", ex);
            }
        }
    }
}
