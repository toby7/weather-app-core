namespace WeatherStation.Model.WaterLevel
{
    using Newtonsoft.Json;

    public class WaterLevelJsonObject
    {
        [JsonProperty("value")]
        public WaterLevelValue[] Values { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("parameter")]
        public Parameter Parameter { get; set; }

        [JsonProperty("station")]
        public Station Station { get; set; }

        //[JsonProperty("period")]
        //public Period Period { get; set; }

        //[JsonProperty("position")]
        //public Position[] Position { get; set; }

        //[JsonProperty("link")]
        //public Link[] Link { get; set; }
    }

    //public partial class Link
    //{
    //    [JsonProperty("rel")]
    //    public string Rel { get; set; }

    //    [JsonProperty("type")]
    //    public string Type { get; set; }

    //    [JsonProperty("href")]
    //    public Uri Href { get; set; }
    //}
}
