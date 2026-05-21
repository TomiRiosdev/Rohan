using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestioónProveedor.Exceptions
{
    public class ProductoProveedorServiceException : Exception
    {
        public ProductoProveedorServiceException() { }
        public ProductoProveedorServiceException(string message) : base(message) { }
        public ProductoProveedorServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}

