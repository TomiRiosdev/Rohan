using Models;
using System;
using System.Collections.Generic;

namespace Models;

public partial class OrdenCompraDetalle
{
    public Guid IdOrdenCompraDetalle { get; set; }

    public Guid? IdOrdenCompra { get; set; }

    public Guid? IdProducto { get; set; }

    public int? CantidadPedida { get; set; }

    public int? CantidadRecibida { get; set; }

    public decimal? PrecioPactado { get; set; }

    public int? Renglon { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();

    public virtual ICollection<VinculoSolicitudOc> VinculoSolicitudOcs { get; set; } = new List<VinculoSolicitudOc>();
}
