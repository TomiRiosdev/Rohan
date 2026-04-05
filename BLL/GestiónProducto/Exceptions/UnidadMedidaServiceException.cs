using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónProducto.Exceptions
{
    public class UnidadMedidaServiceException : Exception
    {
        public UnidadMedidaServiceException() { }

        public UnidadMedidaServiceException(string message) : base(message) { }

        public UnidadMedidaServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
