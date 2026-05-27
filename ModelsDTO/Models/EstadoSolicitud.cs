using System;
using System.Collections.Generic;

namespace Models;

public partial class EstadoSolicitud
{
    public int IdEstadoSolicitud { get; set; }

    public string? Descripcion { get; set; }

  
    public virtual ICollection<SolicitudPedido> SolicitudPedido { get; set; } = new List<SolicitudPedido>();

    public virtual ICollection<OrdenCompra> OrdenCompra { get; set; } = new List<OrdenCompra>();
}
