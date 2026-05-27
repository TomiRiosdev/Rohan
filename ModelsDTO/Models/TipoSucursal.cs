using System;
using System.Collections.Generic;

namespace Models;

public partial class TipoSucursal
{
    public Guid IdTipoSucursal { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Sucursal> Sucursal { get; set; } = new List<Sucursal>();
}
