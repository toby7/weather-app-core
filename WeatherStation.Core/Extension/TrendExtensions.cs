using System.Linq;
using System.Collections.Generic;
using WeatherStation.Model.Enums;

namespace WeatherStation.Core.Extension
{
    public static class TrendExtensions
    {
        public static Trend ToTrend(this IEnumerable<double> values, double currentValue)
        {
            var quotient = values.Sum() / values.Count();

            if (currentValue > quotient)
            {
                return Trend.Up;
            }

            return currentValue < quotient ? Trend.Down : Trend.Unchanged;
        }
    }
}
