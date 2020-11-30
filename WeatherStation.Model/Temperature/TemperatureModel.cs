using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherStation.Model.Temperature
{
    using Newtonsoft.Json;

    public class TemperatureModel
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("temperature")]
        public double Temperature { get; set; }
    }
}
