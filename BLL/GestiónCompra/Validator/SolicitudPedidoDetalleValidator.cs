using BLL.DomainDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónCompra.Validator
{
    // 1. VALIDADOR DEL RENGLÓN 
    public class SolicitudPedidoDetalleValidator : AbstractValidator<SolicitudPedidoDetalleDTO>
    {
        public SolicitudPedidoDetalleValidator()
        {
            RuleFor(x => x.IdProducto)
                .NotEmpty().WithMessage("Cada renglón de la solicitud debe especificar un producto válido.");

            //  Protege contra cantidades negativas o nulas de stock comercial
            RuleFor(x => x.CantidadBultosSolicitada)
                .GreaterThan(0).WithMessage("La cantidad de bultos solicitada debe ser mayor a cero.");
        }
    }

    // 2. VALIDADOR DE LA CABECERA 
    public class SolicitudPedidoValidator : AbstractValidator<SolicitudPedidoDTO>
    {
        public SolicitudPedidoValidator()
        {
            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("Operación inválida: No se puede registrar una Solicitud de Pedido sin renglones.")
                .Must(detalles => detalles != null && detalles.Any())
                .WithMessage("La solicitud debe contener al menos un producto.")

                // Evita que metan el mismo producto dos veces en el mismo pedido
                .Must(detalles => detalles != null && detalles.Select(d => d.IdProducto).Distinct().Count() == detalles.Count)
                .WithMessage("Error de carga: No se permiten renglones duplicados para el mismo producto. Consolide las cantidades.");

            RuleForEach(x => x.Detalles)
                .SetValidator(new SolicitudPedidoDetalleValidator());
        }
    }
}
