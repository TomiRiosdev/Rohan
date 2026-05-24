using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónStock.Exceptions
{
    // Excepción Base para todo el dominio de Inventario
    public class StockDomainException : Exception
    {
        public StockDomainException(string message) : base(message) { }
        public StockDomainException(string message, Exception innerException) : base(message, innerException) { }
    }

    // Error cuando fallan las validaciones sintácticas de FluentValidation
    public class StockValidationException : StockDomainException
    {
        public StockValidationException(string message) : base(message) { }
    }

    // Error específico de Negocio: Se superó el Stock Máximo configurado
    public class TechoOperativoException : StockDomainException
    {
        public TechoOperativoException(int maximoPermitido, int cantidadIntentada)
            : base($"Operación inválida: La cantidad intentada ({cantidadIntentada}) supera el techo operativo máximo permitido ({maximoPermitido}) para esta sucursal.") { }
    }
}

