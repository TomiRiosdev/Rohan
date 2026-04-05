
namespace BLL.GestiónSucursal.Exceptions
{
    public class TipoSucursalException : Exception
    {
        public TipoSucursalException() { }
        public TipoSucursalException(string message) : base(message) { }
        public TipoSucursalException(string message, Exception innerException) : base(message, innerException) { }
    }
}
