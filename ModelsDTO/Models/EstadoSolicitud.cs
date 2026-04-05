using System;
using System.Collections.Generic;

namespace Models;

public partial class EstadoSolicitud
{
    public Guid IdEstadoSolicitud { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();

    public virtual ICollection<SolicitudPedido> SolicitudPedidos { get; set; } = new List<SolicitudPedido>();
}
