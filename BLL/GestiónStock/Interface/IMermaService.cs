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
        //Sub-categoría C: MermaService (Analítico / Solo Lectura Predictiva)
        //No hace operaciones de escritura(INSERT/UPDATE). Solo lee los lotes activos y el consolidado para calcular los semáforos de riesgo del local.

        IEnumerable<InventarioAlertaDTO> ObtenerAlertasInventario(Guid idSucursal);
    }
}
