
namespace Catsa.Utility.ConfigSettings
{
    public class LoggingSettings
    {
        public const string SectionName = "NLog";
        public string LogFile { get; set; }
        public string InternalLogFile { get; set; }
        public string LogConfigFile { get; set; }
    }
}
