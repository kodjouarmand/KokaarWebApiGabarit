using NLog;
using System.IO;

namespace Catsa.Infrastructure.Logging
{
    public class LoggerService : ILoggerService
    {
        public static string LOG_FILE_NAME = string.Concat(Directory.GetCurrentDirectory(), "/Logs/logs.txt");
        //public static string INTERNAL_LOG_FILE_NAME = string.Concat(Directory.GetCurrentDirectory(), "/Logs/internal_logs.txt");

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public LoggerService()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = LOG_FILE_NAME };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
        }
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }
        public void LogError(string message)
        {
            _logger.Error(message);
        }
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }

    }
}
