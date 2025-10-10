using System;
using System.Collections.Generic;

namespace DAO.DomainModel;

public partial class Producto
{
    public Guid IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public Guid IdCategoria { get; set; }

    public Guid IdTipoProducto { get; set; }

    public Guid IdUnidadMedida { get; set; }

    public Guid IdProveedor { get; set; }

    public bool Habilitado { get; set; }
}
