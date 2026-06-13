using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónCompra.Exceptions
{
    public class RohanComprasException : Exception
    {
        public RohanComprasException(string mensaje, Exception? inner = null) : base(mensaje, inner) { }
    }

    //  Errores de validación sintáctica (FluentValidation, campos vacíos o números negativos)
    public class ComprasValidationException : RohanComprasException
    {
        public ComprasValidationException(string mensaje) : base(mensaje) { }
    }

    //  Violaciones de reglas de negocio en frío (Precios en cero, proveedores inactivos, productos no asociados)
    public class ReglaNegocioComprasException : RohanComprasException
    {
        public ReglaNegocioComprasException(string mensaje) : base(mensaje) { }
    }

    //  Caídas de infraestructura, base de datos o inconsistencias relacionales en compras
    public class ComprasDomainException : RohanComprasException
    {
        public ComprasDomainException(string mensaje, Exception? inner = null) : base(mensaje, inner) { }
    }
}
