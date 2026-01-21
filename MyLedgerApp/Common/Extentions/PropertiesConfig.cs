using MyLedgerApp.Application.Properties;

namespace MyLedgerApp.Common.Extentions
{
    /// <summary>
    /// Configuration extentions for early setups.
    /// </summary>
    public static class PropertiesConfig
    {
        public static JwtSettings GetJwtSettings(this IConfiguration config)
        {
            return config
                .GetSection(AppProperties.JwtSection)
                .Get<JwtSettings>()
                ?? throw new InvalidOperationException(
                    "Jwt settings missing");
        }
        public static DbSettings GetDbSettings(this IConfiguration config)
        {
            return config
                .GetSection(AppProperties.DbSection)
                .Get<DbSettings>()
                ?? throw new InvalidOperationException(
                    "DB settings missing");
        }

        public static AppSettings GetAppSettings(this IConfiguration config)
        {
            return config
                .GetSection(AppProperties.AppSection)
                .Get<AppSettings>()
                ?? throw new InvalidOperationException(
                    "App settings missing");
        }
    }
}
