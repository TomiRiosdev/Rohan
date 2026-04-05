using System;
using System.Collections.Generic;

namespace Models;

public partial class StockPorSucursal
{
    public Guid IdStockPorSucursal { get; set; }

    public Guid IdSucursal { get; set; }

    public Guid IdProducto { get; set; }

    public int? CantidadTotal { get; set; }

    public int? StockMinimo { get; set; }

    public int? StockMaximo { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
