using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.API.Settings
{
    public class AppSettings
    {
        public string OutdoorTemperature { get; set; }

        public string WaterLevel { get; set; }

        public string SolarEnergy { get; set; }

        public string SolarEnergyLastMonth { get; set; }
    }
}
