using System;
using System.Collections.Generic;

namespace Models;

public partial class SolicitudPedidoDetalle
{
    public Guid IdSolicitudPedido { get; set; }

    public Guid? IdSolicitud { get; set; }

    public Guid? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public int? Renglon { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual SolicitudPedido? IdSolicitudPedidoNavigation { get; set; }

    public virtual ICollection<VinculoSolicitudOc> VinculoSolicitudOc { get; set; } = new List<VinculoSolicitudOc>();
}
