using System;
using System.Collections.Generic;

namespace Models;

public partial class SolicitudPedido
{
    public Guid IdSolicitudPedido { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdSucursal { get; set; }

    public int? NroSolicitud { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public int? IdEstadoSolicitud { get; set; }

    public string? UsuarioNombre { get; set; }

    public virtual Estados? IdEstadoSolicitudNavigation { get; set; }

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public virtual ICollection<OrdenTraspaso> OrdenTraspaso { get; set; } = new List<OrdenTraspaso>();
    public virtual ICollection<SolicitudPedidoDetalle> SolicitudPedidoDetalle { get; set; } = new List<SolicitudPedidoDetalle>();
}
