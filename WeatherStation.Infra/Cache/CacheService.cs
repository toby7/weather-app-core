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
            Func<Task<TCachedItem>> retriever)
        {
            var test = memoryCache.Get(key);
            var item = await memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10800);
                return retriever();
            });

            return item;
        }
    }
}
