using Catsa.Utility.ConfigSettings;
using Microsoft.Extensions.Options;
using NLog;

namespace Catsa.Infrastructure.Logging
{
    public class LoggerService : ILoggerService
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly LoggingSettings _loggingSettings;
        public LoggerService(IOptions<LoggingSettings> loggingSettings)
        {
            _loggingSettings = loggingSettings.Value;

            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = _loggingSettings.LogFile };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;
        }
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }
        public void LogError(string message)
        {
            _logger.Error(message);
        }
        public void LogInformation(string message)
        {
            _logger.Info(message);
        }
        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }

    }
}
