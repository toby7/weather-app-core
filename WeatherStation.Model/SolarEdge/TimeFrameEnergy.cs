using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherStation.Model.SolarEdge
{
    using Newtonsoft.Json;

    public class StartLifetimeEnergy
    {
        public string date { get; set; }
        public double energy { get; set; }
        public string unit { get; set; }
    }

    public class EndLifetimeEnergy
    {
        public string date { get; set; }
        public double energy { get; set; }
        public string unit { get; set; }
    }

    public class TimeFrameEnergy
    {
        public double energy { get; set; }
        public string unit { get; set; }
        public string measuredBy { get; set; }
        public StartLifetimeEnergy startLifetimeEnergy { get; set; }
        public EndLifetimeEnergy endLifetimeEnergy { get; set; }
    }

    public class Root
    {
        public TimeFrameEnergy timeFrameEnergy { get; set; }
    }
}
