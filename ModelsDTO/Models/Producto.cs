using System;
using System.Collections.Generic;

namespace Models;

public partial class Producto
{
    public Guid IdProducto { get; set; }

    public Guid? IdCategoria { get; set; }

    public int? CodigoSku { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public Guid? IdUnidadMedida { get; set; }

    public int? ContenidoPorVenta { get; set; }

    public bool? Habilitado { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual UnidadMedida? IdUnidadMedidaNavigation { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();

    public virtual ICollection<ProductoProveedor> ProductoProveedors { get; set; } = new List<ProductoProveedor>();

    public virtual ICollection<SolicitudPedidoDetalle> SolicitudPedidoDetalles { get; set; } = new List<SolicitudPedidoDetalle>();

    public virtual ICollection<StockPorSucursal> StockPorSucursals { get; set; } = new List<StockPorSucursal>();
}
