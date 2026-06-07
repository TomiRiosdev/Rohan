using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class ConfiguracionAlertasDTO
    {
        public Guid IdProducto { get; set; }
        public int CodigoSku { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;

        // Alertas de Stock Base
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }

        // Configuración de Mermas (Vencimiento General)
        public int? DiasVidaUtil { get; set; }
        public int? DiasAlertaVencimiento { get; set; }
    }
}
