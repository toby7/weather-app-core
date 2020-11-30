using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherStation.Core.Extension
{
    using System.Linq;
    using WeatherStation.Model.Enums;

    public static class TrendExtensions
    {
        public static Trend ToTrend(this IEnumerable<double> values, double currentValue)
        {
            return currentValue > values.Sum() / values.Count() ? Trend.Up : Trend.Down;
        }
    }
}
