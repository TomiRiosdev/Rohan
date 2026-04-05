using System;
using System.Collections.Generic;

namespace Models;

public partial class TipoUsuario
{
    public Guid IdTipoUsuario { get; set; }

    public string? Descripcion { get; set; }
}
