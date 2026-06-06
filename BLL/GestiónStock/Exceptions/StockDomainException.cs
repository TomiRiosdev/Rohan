using System;

namespace BLL.GestiónStock.Exceptions
{
    // Excepción Base del Módulo
    public class RohanStockException : Exception
    {
        public RohanStockException(string mensaje, Exception? inner = null) : base(mensaje, inner) { }
    }

    // Errores de validación sintáctica (FluentValidation o datos nulos)
    public class StockValidationException : RohanStockException
    {
        public StockValidationException(string mensaje) : base(mensaje) { }
    }

    // Violaciones de reglas de negocio en frío (Techos operativos, mermas inválidas)
    public class TechoOperativoException : RohanStockException
    {
        public int LimiteMaximo { get; }
        public int CantidadIntentada { get; }

        public TechoOperativoException(int limiteMaximo, int cantidadIntentada)
            : base($"Techo operativo excedido: Intentó ingresar {cantidadIntentada} u. pero el límite máximo configurado es {limiteMaximo} u.")
        {
            LimiteMaximo = limiteMaximo;
            CantidadIntentada = cantidadIntentada;
        }
    }

    // Caídas de infraestructura, base de datos, o inconsistencias relacionales
    public class StockDomainException : RohanStockException
    {
        public StockDomainException(string mensaje, Exception? inner = null) : base(mensaje, inner) { }
    }
}
