using System;
using System.Collections.Generic;

namespace DAO.DomainModel;

public partial class UnidadMedidum
{
    public Guid IdUnidadMedida { get; set; }

    public string Nombre { get; set; } = null!;

    public string Habilitado { get; set; } = null!;
}
