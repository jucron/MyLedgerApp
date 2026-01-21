using Microsoft.Extensions.Options;

namespace MyLedgerApp.Application.Properties
{
    public class AppProperties(IOptions<CacheSettings> cacheSettings, IOptions<JwtSettings> jwtSettings): IAppProperties
    {
        // Only at startup
        public const string DbSection = "Database";
        public const string AppSection = "App";

        // Runtime relevant
        public const string CacheSection = "Cache";
        public const string JwtSection = "Jwt";
        public CacheSettings CacheSettings { get; } = cacheSettings.Value;
        public JwtSettings JwtSettings { get; } = jwtSettings.Value;
    }
}
