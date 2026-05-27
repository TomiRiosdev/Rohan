using System;
using System.Collections.Generic;

namespace Models;

public partial class TipoMovimiento
{
    public int IdTipoMovimiento { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<MovimientosStock> MovimientosStock { get; set; } = new List<MovimientosStock>();
}
