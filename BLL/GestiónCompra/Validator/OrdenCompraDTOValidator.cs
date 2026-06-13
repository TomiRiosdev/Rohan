using BLL.DomainDtos;
using FluentValidation;
using System;

namespace BLL.GestiónCompra.Validator
{
    public class OrdenCompraDTOValidator : AbstractValidator<OrdenCompraDTO>
    {
        public OrdenCompraDTOValidator()
        {
            RuleFor(o => o.IdProveedor)
                .NotEmpty().WithMessage("Debe seleccionar un proveedor válido para emitir la Orden de Compra.");

            RuleFor(o => o.IdUsuario)
                .NotEmpty().WithMessage("Error de auditoría: No se detectó el usuario operador que genera el documento.");

            RuleFor(o => o.Detalles)
                .NotEmpty().WithMessage("La Orden de Compra debe contener al menos un renglón de producto.");

            // Ejecuta el validador secundario por cada ítem de la lista interna
            RuleForEach(o => o.Detalles)
                .SetValidator(new OrdenCompraDetalleDTOValidator());
        }
    }
}
