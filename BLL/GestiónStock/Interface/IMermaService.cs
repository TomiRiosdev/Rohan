using BLL.DomainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónStock.Interface
{
    public interface IMermaService
    {
        ConfiguracionAlertasDTO ObtenerAlertasPorProducto(Guid idProducto);
        void GuardarConfiguracionAlertas(ConfiguracionAlertasDTO dto);
        IEnumerable<InventarioAlertaDTO> ObtenerAlertasInventario(Guid idSucursal);
        List<LoteDetalleVencimientoDTO> ObtenerLotesPorProducto(Guid idProducto, Guid idSucursal);
    }
}
