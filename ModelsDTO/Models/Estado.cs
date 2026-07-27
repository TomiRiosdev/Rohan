using System;
using System.Collections.Generic;

namespace Models;

public partial class Estados
{
    public int IdEstadoSolicitud { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<OrdenCompra> OrdenCompra { get; set; } = new List<OrdenCompra>();

    public virtual ICollection<OrdenTraspaso> OrdenTraspaso { get; set; } = new List<OrdenTraspaso>();

    public virtual ICollection<SolicitudPedido> SolicitudPedido { get; set; } = new List<SolicitudPedido>();
}
