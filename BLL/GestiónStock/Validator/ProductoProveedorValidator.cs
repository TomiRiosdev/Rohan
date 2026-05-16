using BLL.DomainDtos;
using FluentValidation;


namespace BLL.GestiónStock.Validator
{
    public class ProductoProveedorValidator : AbstractValidator<ProductoProveedorDTO>
    {
        public ProductoProveedorValidator()
        {
            // 1. Validaciones de Identidades
            RuleFor(x => x.IdProducto)
                .NotEmpty().WithMessage("La relación requiere un producto válido.");

            RuleFor(x => x.IdProveedor)
                .NotEmpty().WithMessage("La relación requiere un proveedor válido.");

            // 2. Validaciones Comerciales
            RuleFor(x => x.UltimoPrecioCompra)
                .GreaterThanOrEqualTo(0).WithMessage("El último precio de compra registrado no puede ser un valor negativo.");
          
        }
    }
}
