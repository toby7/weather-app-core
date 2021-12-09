using System;
using System.Collections.Generic;
using System.Linq;
using WeatherStation.Core.Interfaces;

namespace WeatherStation.Core.Extension
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source is null || !source.Any();
        }

        public static IKeyFigureProvider Resolve(this IEnumerable<IKeyFigureProvider> providers, string providerName)
        {
            if (providers == null)
            {
                throw new ArgumentNullException(nameof(providers));
            }

            var provider = providers
                .SingleOrDefault(x => x.Name.Equals(providerName)) ?? throw new KeyNotFoundException(nameof(providerName));

            return provider;
        }
    }
}
