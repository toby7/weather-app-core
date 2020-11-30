namespace WeatherStation.Model.WaterLevel
{
    using Newtonsoft.Json;

    public class WaterLevelValue
    {
        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

    }
}