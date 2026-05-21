using BLL.DomainDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónCompra.Validator
{
    // 1. VALIDADOR DEL RENGLÓN (DETALLE)
    public class SolicitudPedidoDetalleValidator : AbstractValidator<SolicitudPedidoDetalleDTO>
    {
        public SolicitudPedidoDetalleValidator()
        {
            RuleFor(x => x.IdProducto)
                .NotEmpty().WithMessage("Cada renglón de la solicitud debe especificar un producto válido.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad solicitada en cada renglón debe ser mayor a cero.");
        }
    }

    // 2. VALIDADOR DE LA CABECERA (MAESTRO)
    public class SolicitudPedidoValidator : AbstractValidator<SolicitudPedidoDTO>
    {
        public SolicitudPedidoValidator()
        {
            // Validamos que la solicitud contenga elementos en su lista de detalles
            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("Operación inválida: No se puede registrar una Solicitud de Pedido sin renglones.")
                .Must(detalles => detalles != null && detalles.Any())
                .WithMessage("La solicitud debe contener al menos un producto.");

            // REGLA EN CASCADA: Registramos el validador hijo para que itere y evalúe cada renglón de la lista
            RuleForEach(x => x.Detalles)
                .SetValidator(new SolicitudPedidoDetalleValidator());
        }
    }
}
