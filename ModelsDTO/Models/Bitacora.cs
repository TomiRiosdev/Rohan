using System;
using System.Collections.Generic;

namespace Models;

public partial class Bitacora
{
    public Guid IdBitacora { get; set; }

    public Guid? IdEntidadRelacionada { get; set; }

    public string? Operacion { get; set; }

    public string? Detalle { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Usuario { get; set; }
}
