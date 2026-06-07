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
   
    public int? IdTipoEnvase { get; set; }
   
    public int? CantidadPorBulto { get; set; }

    public int? DiasVidaUtil { get; set; }

    public int? DiasAlertaVencimiento { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual UnidadMedida? IdUnidadMedidaNavigation { get; set; }

    public virtual ICollection<Lote> Lote { get; set; } = new List<Lote>();

    public virtual ICollection<ProductoProveedor> ProductoProveedor { get; set; } = new List<ProductoProveedor>();

    public virtual ICollection<SolicitudPedidoDetalle> SolicitudPedidoDetalle { get; set; } = new List<SolicitudPedidoDetalle>();

    public virtual ICollection<StockPorSucursal> StockPorSucursal { get; set; } = new List<StockPorSucursal>();
}
