using System;
using System.Collections.Generic;

namespace Models;

public partial class Sucursal
{
    public Guid IdSucursal { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public int? CodigoPostal { get; set; }

    public int? Telefono { get; set; }

    public Guid? IdTipoSucursal { get; set; }

    public string? Localidad { get; set; }

    public bool? Habilitado { get; set; }

    public virtual TipoSucursal? IdTipoSucursalNavigation { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();

    public virtual ICollection<MovimientosStock> MovimientosStockIdSucursalDestinoNavigations { get; set; } = new List<MovimientosStock>();

    public virtual ICollection<MovimientosStock> MovimientosStockIdSucursalNavigations { get; set; } = new List<MovimientosStock>();

    public virtual ICollection<MovimientosStock> MovimientosStockIdSucursalOrigenNavigations { get; set; } = new List<MovimientosStock>();

    public virtual ICollection<SolicitudPedido> SolicitudPedidos { get; set; } = new List<SolicitudPedido>();

    public virtual ICollection<StockPorSucursal> StockPorSucursals { get; set; } = new List<StockPorSucursal>();
}
