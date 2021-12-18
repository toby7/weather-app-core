namespace WeatherStation.Core.Models.Settings
{
    public class NetamoApiSettings
    {
        public string BaseUrl { get; set; }
        public string TokenUrl { get; set; }
        public string DeviceId { get; set; }
        public string OutdoorModuleId { get; set; }
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Scope { get; set; }
    }
}
