using BLL.DomainDtos;
using FluentValidation;
using System;


namespace BLL.GestiónCompra.Validator
{
    public class OrdenCompraDetalleDTOValidator : AbstractValidator<OrdenCompraDetalleDTO>
    {
        public OrdenCompraDetalleDTOValidator()
        {
            RuleFor(d => d.IdProducto)
                .NotEmpty().WithMessage("Cada renglón de la orden de compra debe tener un producto asociado.");

            RuleFor(d => d.CantidadPedida)
                .GreaterThan(0).WithMessage("La cantidad solicitada de bultos debe ser mayor a 0.");

            RuleFor(d => d.PrecioPactado)
                .GreaterThan(0).WithMessage("El precio pactado para el producto debe ser mayor a $0.00 (No se permiten costos vacíos o bonificaciones totales).");

            RuleFor(d => d.Renglon)
                .GreaterThan(0).WithMessage("El número de renglón secuencial no es válido.");
        }
    }
}
