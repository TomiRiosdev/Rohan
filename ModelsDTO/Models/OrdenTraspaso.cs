using System;
using System.Collections.Generic;

namespace Models;

public partial class OrdenTraspaso
{
    public Guid IdOrdenTraspaso { get; set; }

    public int? NroTraspaso { get; set; }

    public Guid? IdSucursalOrigen { get; set; }

    public Guid? IdSucursalDestino { get; set; }

    public Guid? IdSolicitudPedido { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaEmision { get; set; }

    public DateTime? FechaRecepcion { get; set; }

    public Guid? IdUsuarioEmisior { get; set; }

    public Guid? IdUsuarioReceptor { get; set; }

    public string? Observaciones { get; set; }

    public virtual Estados? IdEstadoSolicitudNavigation { get; set; }

    public virtual SolicitudPedido? IdSolicitudPedidoNavigation { get; set; }

    public virtual Sucursal? IdSucursalDestinoNavigation { get; set; }

    public virtual Sucursal? IdSucursalOrigenNavigation { get; set; }

    public virtual ICollection<OrdenTraspasoDetalle> OrdenTraspasoDetalle { get; set; } = new List<OrdenTraspasoDetalle>();
}
