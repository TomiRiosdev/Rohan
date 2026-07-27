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

        public int CantidadEnviada { get; set; }
        public int CantidadRecibida { get; set; }

        public Guid? IdLoteOrigen { get; set; }
        public string NumeroLoteOrigen { get; set; }
    }
}

