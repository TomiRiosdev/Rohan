using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public static class ExceptionLogger
    {
        private static readonly string LogDirectory = "Logs";
        private static readonly string LogFileName = "exceptions.log";
        private static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogDirectory, LogFileName);
        private static readonly object LockObj = new();

        public static void Log(ExceptionContext context)
        {
            try
            {
                lock (LockObj)
                {
                    // Aseguramos que la carpeta Logs exista en el directorio bin de ejecución
                    string? directory = Path.GetDirectoryName(LogPath);
                    if (directory != null && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Construimos un reporte de error robusto y estructurado
                    var sb = new StringBuilder();
                    sb.AppendLine($"TIMESTAMP: {context.Timestamp:dd/MM/yyyy HH:mm:ss}");
                    sb.AppendLine($"ORIGEN:    {context.ClassName}.{context.MethodName}()");
                    sb.AppendLine($"EXCEPCIÓN: {context.Exception.GetType().Name} -> {context.Exception.Message}");

                    if (context.Arguments != null && context.Arguments.Length > 0)
                    {
                        sb.AppendLine("ARGUMENTOS DE ENTRADA:");
                        for (int i = 0; i < context.Arguments.Length; i++)
                        {
                            var arg = context.Arguments[i];
                            sb.AppendLine($"   -> [{i}]: ({arg?.GetType().Name ?? "Null"}) = {arg ?? "NULL"}");
                        }
                    }

                    sb.AppendLine("STACK TRACE:");
                    sb.AppendLine(context.Exception.StackTrace);

                    if (context.Exception.InnerException != null)
                    {
                        sb.AppendLine($"INNER EXCEPTION: {context.Exception.InnerException.GetType().Name} -> {context.Exception.InnerException.Message}");
                        sb.AppendLine(context.Exception.InnerException.StackTrace);
                    }

                    File.AppendAllText(LogPath, sb.ToString());
                }
            }
            catch
            {
                // Un logger jamás debe hacer crasear la aplicación principal si falla el disco o los permisos de escritura
            }
        }
    }
}
