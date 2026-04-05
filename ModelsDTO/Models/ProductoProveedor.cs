using System;
using System.Collections.Generic;

namespace Models;

public partial class ProductoProveedor
{
    public Guid IdProductoProveedor { get; set; }

    public Guid? IdProducto { get; set; }

    public Guid? IdProveedor { get; set; }

    public bool? EsProveedorPrincipal { get; set; }

    public decimal? UltimoPrecioCompra { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }
}
