using Microsoft.Extensions.Options;

namespace MyLedgerApp.Application.Properties
{
    public class AppProperties(IOptions<CacheSettings> cacheSettings, IOptions<JwtSettings> jwtSettings, IOptions<DbSettings> dbSettings) : IAppProperties
    {
        public CacheSettings CacheSettings { get; } = cacheSettings.Value;
        public JwtSettings JwtSettings { get; } = jwtSettings.Value;
        public DbSettings DbSettings { get; } = dbSettings.Value;
    }
}
