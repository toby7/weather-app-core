using Microsoft.Extensions.Caching.Memory;
using WeatherStation.Core.Interfaces;

namespace WeatherStation.Infra.Cache;

public sealed class CacheService : ICacheService
{
    private readonly IMemoryCache memoryCache;
    static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

    public CacheService(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

    public async Task<TCachedItem> GetAndCache<TCachedItem>(
        string key,
        TimeSpan absoluteExpirationRelativeToNow,
        Func<Task<TCachedItem>> retriever)
    {
        var item = memoryCache.Get<TCachedItem>(key);

        if (item is not null)
        {
            return item;
        }

        await semaphoreSlim.WaitAsync();
        try
        {
            item = memoryCache.Get<TCachedItem>(key);

            if (item is not null)
            {
                return item;
            }

            item = await retriever();
            var cacheEntryOptions = new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow };
                
            memoryCache.Set(key, item, cacheEntryOptions);
        }
        finally
        {
            semaphoreSlim.Release();
        }

        return item;
    }
}