using System;
using System.Collections.Generic;

namespace DAO;

public partial class TipoProducto
{
    public Guid IdTipoProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Habilitado { get; set; }
}
