using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.GestiónStock.Exceptions
{
    public class StockPorSucursalServiceException : Exception
    {
        public StockPorSucursalServiceException() { }

        public StockPorSucursalServiceException(string message) : base(message) { }
        public StockPorSucursalServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}

