using Microsoft.Extensions.Caching.Memory;

namespace MyLedgerApp.Application.Services
{
    public class CacheService: ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            _cache.Set(key, value, expiration ?? TimeSpan.FromMinutes(5));
        }

        public T? Get<T>(string key)
        {
            _cache.TryGetValue(key, out T? value);
            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
