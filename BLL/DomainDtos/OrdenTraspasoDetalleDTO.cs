using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainDtos
{
    public class OrdenTraspasoDetalleDTO
    {
        public Guid IdOrdenTraspasoDetalle { get; set; }
        public Guid IdOrdenTraspaso { get; set; }
        public int Renglon { get; set; }

        public Guid IdProducto { get; set; }
        public string ProductoNombre { get; set; }
        public int CodigoSku { get; set; }

        // --- BASES FÍSICAS (En Unidades Sueltas) ---
        public int CantidadSolicitada { get; set; } // Lo que pidió el local originalmente
        public int CantidadEnviada { get; set; }    // Lo que el depósito decide enviar (Unidades)
        public int CantidadRecibida { get; set; }
        public int StockActual { get; set; }        // Stock real actual en el depósito
        public int CantidadPorBulto { get; set; }   // Ej: 6 (si viene en pack de 6)

        // --- NAVEGACIÓN ---
        public Guid? IdLoteOrigen { get; set; }
        public string NumeroLoteOrigen { get; set; }

        public int CantidadSolicitadaBultos => CantidadPorBulto > 0 ? CantidadSolicitada / CantidadPorBulto : CantidadSolicitada;

        public int StockActualBultos => CantidadPorBulto > 0 ? StockActual / CantidadPorBulto : StockActual;

        // ESTA ES LA COLUMNA EDITABLE. 
        // Si el operario escribe "2" bultos, automáticamente guarda "12" en CantidadEnviada.
        public int CantidadEnviadaBultos
        {
            get => CantidadPorBulto > 0 ? CantidadEnviada / CantidadPorBulto : CantidadEnviada;
            set => CantidadEnviada = CantidadPorBulto > 0 ? value * CantidadPorBulto : value;
        }
    }

}