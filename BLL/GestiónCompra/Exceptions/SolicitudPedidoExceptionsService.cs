using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónCompra.Exceptions
{
    public class SolicitudPedidoServiceException : Exception
    {
        public SolicitudPedidoServiceException() { }
        public SolicitudPedidoServiceException(string message) : base(message) { }
        public SolicitudPedidoServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
