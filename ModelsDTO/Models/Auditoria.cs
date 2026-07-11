using System;
using System.Collections.Generic;

namespace Models;

public partial class Auditoria
{
    public Guid IdAuditoria { get; set; }

    public Guid? IdEntidadRelacionada { get; set; }

    public string? Operacion { get; set; }

    public string? Detalle { get; set; }

    public DateTime? Fecha { get; set; }

    public string? NombreUsuario { get; set; }

    public Guid? IdUsuario { get; set; }

    public string? NombreSucursal { get; set; }

    public Guid? IdSucursal { get; set; }
}
