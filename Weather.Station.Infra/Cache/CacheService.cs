using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using WeatherStation.Core.Interfaces;

namespace WeatherStation.Infra.Cache
{
    public sealed class CacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task<TCachedItem> GetAndCache<TCachedItem>(
            string key,
            TimeSpan absoluteExpirationRelativeToNow,
            Func<Task<TCachedItem>> retriever)
        {
            var test = memoryCache.Get(key);
            var item = await memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
                return retriever();
            });

            return item;
        }
    }
}
