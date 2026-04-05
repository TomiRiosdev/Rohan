using System;
using System.Collections.Generic;

namespace Models;

public partial class SolicitudPedido
{
    public Guid IdSolicitudPedido { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdSucursal { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public Guid? IdEstadoSolicitud { get; set; }

    public virtual EstadoSolicitud? IdEstadoSolicitudNavigation { get; set; }

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public virtual ICollection<SolicitudPedidoDetalle> SolicitudPedidoDetalles { get; set; } = new List<SolicitudPedidoDetalle>();
}
