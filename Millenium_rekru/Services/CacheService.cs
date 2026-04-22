using Microsoft.Extensions.Caching.Memory;

namespace Millenium_rekru.Services;

public class CacheService(IMemoryCache _cache) : ICacheService
{
    public void Set(string key, string value, int ttlInMinutes)
    {
        _cache.Set(
            key,
            value,
            TimeSpan.FromMinutes(ttlInMinutes)
            );
    }

    public string? Get(string key)
    {
        return (string?)_cache.Get(key);
    }
    
}