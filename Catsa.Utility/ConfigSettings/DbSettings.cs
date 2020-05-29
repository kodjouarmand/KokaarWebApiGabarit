
namespace Catsa.Utility.ConfigSettings
{
    public class DbSettings
    {
        public const string SectionName = "ConnectionStrings";

        public string CatsaConnectionString { get; set; }
        public string MigrationsAssembly { get; set; }
    }
}
