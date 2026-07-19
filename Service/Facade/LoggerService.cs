using Service.DateAccess.Interface;
using Service.DomainModel.Logging;
using Service.Logic;


namespace Service.Facade
{
    /// <summary>
    /// Servicio de factoría para obtener la implementación del logger.
    /// </summary>
    public class LoggerService
    {
        /// <summary>
        /// Crea y retorna una instancia del logger configurado.
        /// </summary>
        public static ILogger GetLogger()
        {
            var config = new LoggerConfiguration
            {
                LogFilePath = "Logs/mi_app.log",
                MinimumLogLevel = LogLevel.Debug
            };
            return config.CreateFileLogger();
        }
    }
}