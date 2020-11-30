namespace WeatherStation.Model.WaterLevel
{
    using Newtonsoft.Json;

    public class Station
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }
    }
}