namespace WeatherStation.Model.Netamo
{

    public class Measure
    {
        public Body[] body { get; set; }
        public string status { get; set; }
        public float time_exec { get; set; }
        public int time_server { get; set; }
    }
}
