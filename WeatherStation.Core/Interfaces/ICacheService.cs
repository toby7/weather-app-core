using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace WeatherStation.Core.Interfaces
{
    public interface ICacheService
    {
        public Task<TCachedItem> GetAndCache<TCachedItem>(
            string key,
            TimeSpan absoluteExpirationRelativeToNow,
            Func<Task<TCachedItem>> retriever);
    }
}
