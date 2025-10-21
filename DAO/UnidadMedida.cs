using System;
using System.Collections.Generic;

namespace DAO;

public partial class UnidadMedida
{
    public Guid IdUnidadMedida { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Habilitado { get; set; }
}
