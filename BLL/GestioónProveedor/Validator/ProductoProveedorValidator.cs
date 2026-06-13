using BLL.DomainDtos;
using FluentValidation;


namespace BLL.GestiónProveedor.Validator
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
          
        }
    }
}
