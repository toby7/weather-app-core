using System;

namespace WeatherStation.Model.KeyFigure
{
    using Enums;

    public class KeyFigure
    {
        public string Name { get; set; }

        public string Unit { get; set; }

        public string Value { get; set; }

        public Trend Trend { get; set; }

        public DateTime Updated { get; set; }
    }
}
