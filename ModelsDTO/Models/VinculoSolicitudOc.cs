using System;
using System.Collections.Generic;

namespace Models;

public partial class VinculoSolicitudOc
{
    public Guid IdVinculoSolicitudOc { get; set; }

    public Guid? IdOrdenCompraDetalle { get; set; }

    public Guid? IdSolicitudPedidoDetalle { get; set; }

    public int? CantidadAsignada { get; set; }

    public virtual OrdenCompraDetalle? IdOrdenCompraDetalleNavigation { get; set; }

    public virtual SolicitudPedidoDetalle? IdSolicitudPedidoDetalleNavigation { get; set; }
}
