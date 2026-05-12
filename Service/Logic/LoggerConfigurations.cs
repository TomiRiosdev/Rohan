using Service.DateAccess.Implementations;
using Service.DateAccess.Interface;
using Service.DomainModel.Logging;


namespace Service.Logic
{
    public class LoggerConfiguration
    {
        public string LogFilePath { get; set; } = "Logs/app.log"; //Por defecto
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information; //Por defecto

        //Se puede aplicar un factory a futuro si tengo varias implementaciones de logs
        public ILogger CreateFileLogger()
        {
            return new FileLogger(LogFilePath, MinimumLogLevel);
        }
    }
}
