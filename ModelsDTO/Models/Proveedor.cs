using System;
using System.Collections.Generic;

namespace Models;

public partial class Proveedor
{
    public Guid IdProveedor { get; set; }

    public string? Nombre { get; set; }

    public string? RazonSocial { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public string? Cuit { get; set; }

    public bool? Habilitado { get; set; }

    public virtual ICollection<Lote> Lote { get; set; } = new List<Lote>();

    public virtual ICollection<OrdenCompra> OrdenCompra{ get; set; } = new List<OrdenCompra>();

    public virtual ICollection<ProductoProveedor> ProductoProveedor { get; set; } = new List<ProductoProveedor>();
}
