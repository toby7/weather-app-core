using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherStation.Core.Extension
{
    using System.ComponentModel.DataAnnotations;

    public static class DoubleExtensions
    {
        public static double ToKiloWattHour(this double value)
        {
            return value * 0.001;
        }
    }
}
