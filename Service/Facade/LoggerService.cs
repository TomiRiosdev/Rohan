using Service.DateAccess.Interface;
using Service.DomainModel.Logging;
using Service.Logic;


namespace Service.Facade
{
    public class LoggerService
    {
        public static ILogger GetLogger()
        {
            var config = new LoggerConfiguration
            {
                LogFilePath = "Logs/mi_app.log", //Leer desde app.config
                MinimumLogLevel = LogLevel.Debug //Leer desde app.config
            };

            return config.CreateFileLogger();
        }
    }
}
