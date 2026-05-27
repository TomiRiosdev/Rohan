using System;
using System.Collections.Generic;

namespace Models;

public partial class OrdenCompra
{
    public Guid IdOrdenCompra { get; set; }

    public Guid? IdProveedor { get; set; }

    public Guid? IdUsuario { get; set; }

    public int? IdEstadoOc { get; set; }

    public int? NroSolicitud { get; set; }

    public DateTime? FechaOc { get; set; }

    public decimal? CostoTotal { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }
    public virtual EstadoSolicitud? IdEstadoOcNavigation { get; set; }
    public virtual ICollection<OrdenCompraDetalle> OrdenCompraDetalle { get; set; } = new List<OrdenCompraDetalle>();
}
