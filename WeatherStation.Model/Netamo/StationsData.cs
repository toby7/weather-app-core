using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherStation.Model.Netamo
{
    public class StationData
    {
        [JsonPropertyName("body")]
        public Body Body { get; set; }
        public string status { get; set; }
        public float time_exec { get; set; }
        public int time_server { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("devices")]
        public Device[] Devices { get; set; }
        public User user { get; set; }
    }

    public class User
    {
        public string mail { get; set; }
        public Administrative administrative { get; set; }
    }

    public class Administrative
    {
        public string lang { get; set; }
        public string reg_locale { get; set; }
        public string country { get; set; }
        public int unit { get; set; }
        public int windunit { get; set; }
        public int pressureunit { get; set; }
        public int feel_like_algo { get; set; }
    }

    public class Device
    {
        public string _id { get; set; }
        public int date_setup { get; set; }
        public int last_setup { get; set; }
        public string type { get; set; }
        public int last_status_store { get; set; }
        public string module_name { get; set; }
        public float firmware { get; set; }
        public float wifi_status { get; set; }
        public bool reachable { get; set; }
        public bool co2_calibrating { get; set; }
        public string[] data_type { get; set; }
        public Place place { get; set; }
        public string station_name { get; set; }
        public string home_id { get; set; }
        public string home_name { get; set; }

        [JsonPropertyName("dashboard_data")]
        public IndoorDashboardData IndoorDashboardData { get; set; }
        public Module[] modules { get; set; }
    }

    public class Place
    {
        public float altitude { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string timezone { get; set; }
        public float[] location { get; set; }
    }

    public class IndoorDashboardData
    {
        public int time_utc { get; set; }
        public float Temperature { get; set; }
        public float CO2 { get; set; }
        public float Humidity { get; set; }
        public float Noise { get; set; }
        public float Pressure { get; set; }
        public float AbsolutePressure { get; set; }
        public float min_temp { get; set; }
        public float max_temp { get; set; }
        public float date_max_temp { get; set; }
        public float date_min_temp { get; set; }
        public string temp_trend { get; set; }
        public string pressure_trend { get; set; }
    }

    public class Module
    {
        public string _id { get; set; }
        public string type { get; set; }
        public string module_name { get; set; }
        public int last_setup { get; set; }
        public string[] data_type { get; set; }
        public float battery_percent { get; set; }
        public bool reachable { get; set; }
        public int firmware { get; set; }
        public int last_message { get; set; }
        public int last_seen { get; set; }
        public int rf_status { get; set; }
        public int battery_vp { get; set; }
        [JsonPropertyName("dashboard_data")]
        public OutdoorDashboardData OutdoorDashboardData { get; set; }
    }

    public class OutdoorDashboardData
    {
        public int time_utc { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float min_temp { get; set; }
        public float max_temp { get; set; }
        public int date_max_temp { get; set; }
        public int date_min_temp { get; set; }
        public string temp_trend { get; set; }
    }
}
