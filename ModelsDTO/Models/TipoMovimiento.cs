using System;
using System.Collections.Generic;

namespace Models;

public partial class TipoMovimiento
{
    public Guid IdTipoMovimiento { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<MovimientosStock> MovimientosStocks { get; set; } = new List<MovimientosStock>();
}
