using System;
using System.Collections.Generic;

namespace DAO;

public partial class Proveedore
{
    public Guid IdProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Telefono { get; set; }

    public bool Habilitado { get; set; }
}
