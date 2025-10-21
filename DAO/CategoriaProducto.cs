using System;
using System.Collections.Generic;

namespace DAO;

public partial class CategoriaProducto
{
    public Guid IdCategoriaProdcuto { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Habilitado { get; set; }
}
