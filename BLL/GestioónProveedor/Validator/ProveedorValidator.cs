using BLL.DomainDtos;
using FluentValidation;

namespace BLL.GestiónProveedor.Validator
{
    public class ProveedorValidator : AbstractValidator<ProveedorDTO>
    {
        public ProveedorValidator() 
        {
            #region Validaciones Principales
            
            // Nombre (Obligatorio solo letra y  espacio)
                RuleFor(x => x.Nombre)
                        .NotEmpty().WithMessage("El nombre del proveedor es obligatorio.")
                        .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                        .MaximumLength(150).WithMessage("El nombre no puede superar los 150 caracteres.")
                        .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
                        .WithMessage("El nombre solo puede contener letras y espacios (sin números ni símbolos).");
    
                // CUIT 
                RuleFor(x => x.Cuit)
                    .NotEmpty().WithMessage("El CUIT del proveedor es obligatorio.")
                    .WithMessage("El CUIT debe ser un número positivo.");
    
                // Email 
                RuleFor(x => x.Email)
                     .NotEmpty().WithMessage("El Email del proveedor es obligatorio.")
                    .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
                    .WithMessage("El email debe tener un formato válido.");
    
                // Teléfono 
                RuleFor(x => x.Telefono)
                    .NotEmpty().WithMessage("El telefono del proveedor es obligatorio.")
                    .WithMessage("El teléfono debe ser un número positivo.");
    
                // Razón Social (Opcional pero si se ingresa no puede superar los 200 caracteres)
                RuleFor(x => x.RazonSocial)
                    .MaximumLength(200).WithMessage("La razón social no puede superar los 200 caracteres.")
                    .When(x => !string.IsNullOrWhiteSpace(x.RazonSocial));

            #endregion
        }
    }
}
