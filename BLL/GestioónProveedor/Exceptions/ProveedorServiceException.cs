using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónProveedor.Exceptions
{
    public class ProveedorServiceException : Exception
    {
        public ProveedorServiceException() { }

        public ProveedorServiceException(string message) : base(message) { }

        public ProveedorServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
