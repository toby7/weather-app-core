using System;

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
