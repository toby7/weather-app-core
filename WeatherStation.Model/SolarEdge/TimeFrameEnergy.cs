
using System.Collections.Generic;


namespace WeatherStation.Model.SolarEdge
{
    public class Value
    {
        public string date { get; set; }
        public double value { get; set; }
    }

    public class Energy
    {
        public string timeUnit { get; set; }
        public string unit { get; set; }
        public string measuredBy { get; set; }
        public List<Value> values { get; set; }
    }

    //[JsonObject("Root")]
    public class Root
    {
        public Energy energy { get; set; }
    }
}
