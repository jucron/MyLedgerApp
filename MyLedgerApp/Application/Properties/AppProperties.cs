using Microsoft.Extensions.Options;

namespace MyLedgerApp.Application.Properties
{
    public class AppProperties(IOptions<CacheSettings> cacheSettings, IOptions<JwtSettings> jwtSettings): IAppProperties
    {
        
        public CacheSettings CacheSettings { get; } = cacheSettings.Value;
        public JwtSettings JwtSettings { get; } = jwtSettings.Value;
    }
}
