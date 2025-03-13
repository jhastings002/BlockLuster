using Microsoft.Extensions.Configuration;

namespace BlockLuster.Common
{
    public static class Config
    {
        public static string SqlServerConnectionString => GetConfigValue("ConnectionStrings:Database");

        static IConfiguration? _cachedConfig;

        private static IConfiguration Configuration
        {
            get
            {
                if(_cachedConfig == null)
                {
                    var builder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile("appsettings.Development.json", true, true)
                        .AddJsonFile($"appsettings.{Environment.UserName}.json", true, true)
                        .AddEnvironmentVariables();

                    _cachedConfig = builder.Build();
                }

                return _cachedConfig;
            }
        }

        private static string GetConfigValue(string environmentVariable)
        {
            var result = Configuration[environmentVariable];
            return result;
        }
    }
}