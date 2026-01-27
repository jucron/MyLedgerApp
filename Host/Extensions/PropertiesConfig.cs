using MyLedgerApp.Application.Properties;
using Shared;

namespace Host.Extensions
{
    /// <summary>
    /// Configuration extentions for early setups.
    /// </summary>
    public static class PropertiesConfig
    {
        public static JwtSettings GetJwtSettings(this IConfiguration config)
        {
            return config
                .GetSection(Properties.JwtSection)
                .Get<JwtSettings>()
                ?? throw new InvalidOperationException(
                    "Jwt settings missing");
        }
        public static DbSettings GetDbSettings(this IConfiguration config)
        {
            return config
                .GetSection(Properties.DbSection)
                .Get<DbSettings>()
                ?? throw new InvalidOperationException(
                    "DB settings missing");
        }

        public static AppSettings GetAppSettings(this IConfiguration config)
        {
            return config
                .GetSection(Properties.AppSection)
                .Get<AppSettings>()
                ?? throw new InvalidOperationException(
                    "App settings missing");
        }

        public static AppSettings GetServiceBusSettings(this IConfiguration config)
        {
            return config
                .GetSection(Properties.ServiceBus)
                .Get<AppSettings>()
                ?? throw new InvalidOperationException(
                    "ServiceBus settings missing");
        }
    }
}
