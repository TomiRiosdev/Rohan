using BLL.DomainDtos;
using FluentValidation;

namespace BLL.GestiónProducto.Validator
{
    public class CategoriaValidator : AbstractValidator<CategoriaDTO>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción de la categoría es obligatoria.")
                .MinimumLength(3).WithMessage("La descripción debe tener al menos 3 caracteres.")
                .MaximumLength(100).WithMessage("La descripción no puede superar los 100 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
                .WithMessage("El nombre solo puede contener letras y espacios (sin números ni símbolos).");
        }
    }
}
