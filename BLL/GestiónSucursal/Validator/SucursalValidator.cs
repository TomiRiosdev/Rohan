using BLL.DomainDtos;
using FluentValidation;


namespace BLL.GestiónSucursal.Validator
{
   public class SucursalValidator : AbstractValidator<SucursalDTO>
    {
        public SucursalValidator()
        {
            // Nombre
            RuleFor(s => s.Nombre)
                .NotEmpty().WithMessage("El nombre de la sucursal es obligatorio")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres");

            // Email
            RuleFor(s => s.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El formato del email no es válido")
                .MaximumLength(150).WithMessage("El email no puede superar los 150 caracteres");

            // Dirección
            RuleFor(s => s.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria")
                .MaximumLength(200).WithMessage("La dirección no puede superar los 200 caracteres");

            // Localidad
            RuleFor(s => s.Localidad)
                .NotEmpty().WithMessage("La localidad es obligatoria")
                .MaximumLength(100).WithMessage("La localidad no puede superar los 100 caracteres");

            // IdTipoSucursal
            RuleFor(s => s.IdTipoSucursal)
                .NotEqual(Guid.Empty).WithMessage("Debe seleccionar un tipo de sucursal");

            // Código Postal (opcional pero con reglas si se ingresa)
            RuleFor(s => s.CodigoPostal)
                .GreaterThan(0).When(s => s.CodigoPostal.HasValue)
                .WithMessage("El código postal debe ser mayor a 0")
                .LessThan(100000).When(s => s.CodigoPostal.HasValue)
                .WithMessage("El código postal no parece válido");

            // Teléfono (opcional pero con reglas si se ingresa)
            RuleFor(s => s.Telefono)
                .GreaterThan(0).When(s => s.Telefono.HasValue)
                .WithMessage("El teléfono debe ser un número positivo")
                .WithMessage("El teléfono no parece válido");
        }
    
    }
}
