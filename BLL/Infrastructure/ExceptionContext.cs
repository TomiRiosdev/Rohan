using System;
using System.Runtime.CompilerServices;

namespace BLL.Infrastructure
{
    public class ExceptionContext
    {
        public Exception Exception { get; set; } = null!;
        public string MethodName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public object[] Arguments { get; set; }
        public DateTime Timestamp { get; set; }

        public ExceptionContext()
        {
            Timestamp = DateTime.Now;
            Arguments = Array.Empty<object>();
        }

        /// <summary>
        /// Fábrica inteligente que captura automáticamente el nombre del método que falló mediante Reflection y Atributos del compilador.
        /// </summary>
        public static ExceptionContext Crear(Exception ex, object[] args, [CallerMemberName] string methodName = "")
        {
            return new ExceptionContext
            {
                Exception = ex,
                Arguments = args,
                MethodName = methodName,
                ClassName = ex.TargetSite?.DeclaringType?.Name ?? "ClaseDesconocida"
            };
        }
    }
}
