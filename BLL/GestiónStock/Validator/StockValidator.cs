using BLL.DomainDtos;
using FluentValidation;


namespace BLL.GestiónStock.Validator
{
    public class StockValidator : AbstractValidator<StockPorSucursalDTO>
    {
        public StockValidator()
        {
            // 1. Validaciones de Identidad de Contexto
            RuleFor(x => x.IdProducto)
                .NotEmpty().WithMessage("Debe seleccionar un producto válido para asignar stock.");

            // 2. Validaciones de Rangos Matemáticos de Stock
            RuleFor(x => x.CantidadTotal)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad total de stock no puede ser un número negativo.");

            RuleFor(x => x.StockMinimo)
                .GreaterThanOrEqualTo(0).WithMessage("El stock mínimo establecido no puede ser negativo.");

            RuleFor(x => x.StockMaximo)
                .GreaterThan(0).WithMessage("El stock máximo debe ser mayor a cero.");

            // 3. Regla Cruzada de Consistencia (Lógica Relacional)
            RuleFor(x => x.StockMaximo)
                .GreaterThanOrEqualTo(x => x.StockMinimo)
                .WithMessage("Inconsistencia: El stock máximo no puede ser inferior al stock mínimo.");

            // 4. Validaciones obligatorias para el Lote que se genera en paralelo
            RuleFor(x => x.CostoUnitario)
                .GreaterThanOrEqualTo(0).WithMessage("El costo unitario asignado al lote no puede ser negativo.");

            // Si ingresa un número de lote, validamos que no sean solo espacios en blanco
            RuleFor(x => x.NumeroLote)
                .Must(x => x == null || !string.IsNullOrWhiteSpace(x))
                .WithMessage("El formato del número de lote proporcionado no es válido.");
        }
    }
}
