using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class ProductoServiceException : Exception
    {
        public ProductoServiceException() { }

        public ProductoServiceException(string message) : base(message) { }

        public ProductoServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
