using BLL.DomainDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónStock.Interface
{
    public interface ITraspasoService
    {
        void GenerarTraspasoDesdeSolicitud(Guid idSucursalOrigen, Guid idSolicitud);
        IEnumerable<OrdenTraspasoDTO> ObtenerTraspasosEnPreparacion(Guid idSucursalOrigen);
        void ConfirmarEnvioTraspaso(Guid idOrdenTraspaso, string usuarioNombre, List<OrdenTraspasoDetalleDTO> detallesConfirmados);
    }
}
    