

namespace BLL.GestiónSucursal.Exceptions
{
    public class SucursalServiceException : Exception
    {
        public SucursalServiceException() { }
        public SucursalServiceException(string message) : base(message) { }
        public SucursalServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
