using System;
using System.Collections.Generic;

namespace Models;

public partial class Lote
{
    public Guid IdLote { get; set; }

    public Guid? IdProducto { get; set; }

    public Guid? IdSucursal { get; set; }

    public Guid? IdProveedor { get; set; }

    public int? CantidadInicial { get; set; }

    public int? CantidadActual { get; set; }

    public decimal? CostoUnitario { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? NumeroLote { get; set; }

    public Guid? IdOrdenCompraDetalle { get; set; }
   
    public DateTime? FechaVencimiento { get; set; }

    public virtual OrdenCompraDetalle? IdOrdenCompraDetalleNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public virtual ICollection<MovimientosStock> MovimientosStock { get; set; } = new List<MovimientosStock>();
    public virtual ICollection<OrdenTraspasoDetalle> OrdenTraspasoDetalle { get; set; } = new List<OrdenTraspasoDetalle>();
}
