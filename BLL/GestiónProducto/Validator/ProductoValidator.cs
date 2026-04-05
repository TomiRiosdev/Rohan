using BLL.DomainDtos;
using FluentValidation;

namespace BLL.GestiónProducto.Validator
{
    public class ProductoValidator : AbstractValidator<ProductoDTO>
    {
        public ProductoValidator()
        {
            #region Validaciones Principales

            // Nombre (Obligatorio solo letra y  espacio)
            RuleFor(x => x.Nombre)
                   .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                   .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                   .MaximumLength(150).WithMessage("El nombre no puede superar los 150 caracteres.")
                   .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
                   .WithMessage("El nombre solo puede contener letras y espacios (sin números ni símbolos).");

            // Categoría (Obligatoria)
            RuleFor(x => x.IdCategoria)
                .NotEmpty().WithMessage("Debe seleccionar una categoría.")
                .NotEqual(Guid.Empty).WithMessage("Debe seleccionar una categoría válida.");

            // Unidad de Medida (Obligatoria)
            RuleFor(x => x.IdUnidadMedida)
                .NotEmpty().WithMessage("Debe seleccionar una unidad de medida.")
                .NotEqual(Guid.Empty).WithMessage("Debe seleccionar una unidad de medida válida.");

            // Código SKU (Opcional pero si se ingresa debe ser positivo)
            RuleFor(x => x.CodigoSku)
            .Must(codigo => !codigo.HasValue || codigo.Value > 0)
            .WithMessage("El código SKU debe ser un número positivo mayor a 0.");

            // Contenido por Venta (Opcional pero positivo)
            RuleFor(x => x.ContenidoPorVenta)
                .GreaterThan(0).When(x => x.ContenidoPorVenta.HasValue)
                .WithMessage("El contenido por venta debe ser mayor a 0.");

            // Descripción (Opcional)
            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Descripcion));

            #endregion

        }
    }
}
