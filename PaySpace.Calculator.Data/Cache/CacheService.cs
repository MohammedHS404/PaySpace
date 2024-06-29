using Microsoft.Extensions.Caching.Memory;
using PaySpace.Calculator.Data.Abstractions;

namespace PaySpace.Calculator.Data.Cache;

internal sealed class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        if (_memoryCache.TryGetValue(key, out T? value))
        {
            if (value != null)
            {
                return value;
            }
        }

        value = await factory();

        expiration ??= TimeSpan.FromDays(1);

        _memoryCache.Set(key, value, new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.Add(expiration.Value)
        });

        return value;
    }
}