using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class CategoriaServiceException : Exception
    {
        public CategoriaServiceException() { }

        public CategoriaServiceException(string message) : base(message) { }

        public CategoriaServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
