using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class OrdenCompraDetalleDTO
    {
        // Identificadores Únicos Corporativos
        public Guid IdOrdenCompraDetalle { get; set; }
        public Guid IdProducto { get; set; }

        // Información Visual Extendida (Para que la Grilla de la UI no salga vacía)
        public int CodigoSku { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public int Renglon { get; set; }

        // Datos de Control de Cantidades Logísticas
        public int CantidadPedida { get; set; }
        public int CantidadRecibida { get; set; } // Arrancará en 0 hasta que pase a Recepción/Lotes

        // Datos Financieros
        public decimal PrecioPactado { get; set; }

        // Propiedad Calculada Dinámica (Ahorra código en la UI y mappers)
        public decimal SubTotal => CantidadPedida * PrecioPactado;

        //  Opcional: El ID de la solicitud de origen si vino de la carga automática 1 a 1
        public Guid? IdSolicitudPedidoDetalleOrigen { get; set; }
        public int? CantidadAsignadaDesdeSolicitud { get; set; }

        public int UnidadesPorBulto { get; set; } = 1; // Para calcular la cantidad de bultos en la UI, si el producto tiene esta info

        public string Observaciones { get; set; }
    }
}
