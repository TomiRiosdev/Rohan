using System;
using System.Collections.Generic;

namespace Models;

public partial class MovimientosStock
{
    public Guid IdMovimiento { get; set; }

    public Guid? IdSucursal { get; set; }

    public int? IdTipoMovimiento { get; set; }

    public Guid? IdLote { get; set; }

    public Guid? IdSucursalOrigen { get; set; }

    public Guid? IdSucursalDestino { get; set; }

    public int? Cantidad { get; set; }

    public DateTime? FechaMovimiento { get; set; }

    public string? Observaciones { get; set; }

    public virtual Lote? IdLoteNavigation { get; set; }

    public virtual Sucursal? IdSucursalDestinoNavigation { get; set; }

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public virtual Sucursal? IdSucursalOrigenNavigation { get; set; }

    public virtual TipoMovimiento? IdTipoMovimientoNavigation { get; set; }
}
