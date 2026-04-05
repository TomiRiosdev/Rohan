using BLL.DomainDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónSucursal.Validator
{
    public class TipoSucursalValidator : AbstractValidator<TipoSucursalDTO>
    {
        public TipoSucursalValidator()
        {
            RuleFor(x => x.Descripcion)
               .NotEmpty().WithMessage("La descripción del tipo de sucursal es obligatoria.")
               .MinimumLength(3).WithMessage("La descripción debe tener al menos 3 caracteres.")
               .MaximumLength(100).WithMessage("La descripción no puede superar los 100 caracteres.")
               .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
               .WithMessage("El nombre solo puede contener letras y espacios (sin números ni símbolos).");
        }
    }
}
