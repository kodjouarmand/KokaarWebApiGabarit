using System.IO;

namespace Catsa.API
{
    public static class Constants
    {
        public static string CATSA_CONNECTION_STRING_NAME = "CatsaSqlConnection";
        public static string LOG_CONFIG_FILE_NAME = string.Concat(Directory.GetCurrentDirectory(), "/nlog.config");        
    }
}
