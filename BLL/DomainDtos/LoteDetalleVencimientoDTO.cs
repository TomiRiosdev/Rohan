using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class LoteDetalleVencimientoDTO
    {
        public Guid IdLote { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public int CantidadInicial { get; set; }
        public int CantidadActual { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string TipoVencimientoTexto => FechaVencimiento.HasValue
            ? FechaVencimiento.Value.ToString("dd/MM/yyyy")
            : "No Perecedero";

        public int DiasRestantes => FechaVencimiento.HasValue
            ? (FechaVencimiento.Value.Date - DateTime.Today).Days
            : 9999; // Representa que no tiene vencimiento inmediato

        public string EstadoVisualTexto
        {
            get
            {
                if (!FechaVencimiento.HasValue) return "Estable";
                int dias = DiasRestantes;
                if (dias < 0) return "VENCIDO ";
                if (dias == 0) return "Vence HOY ";
                return $"Vence en {dias} días";
            }
        }
    }
}
