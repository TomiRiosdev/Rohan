using System;
using System.Collections.Generic;

namespace Models;

public partial class Categoria
{
    public Guid IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
