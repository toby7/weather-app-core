namespace WeatherStation.Model.WaterLevel
{
    using Newtonsoft.Json;

    public class Parameter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
}