using Models;


namespace Models;

public partial class OrdenCompra
{
    public Guid IdOrdenCompra { get; set; }

    public Guid? IdProveedor { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdEstadoOc { get; set; }

    public int? NroSolicitud { get; set; }

    public DateTime? FechaOc { get; set; }

    public decimal? CostoTotal { get; set; }

    public virtual EstadoSolicitud? IdEstadoOcNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }
}
