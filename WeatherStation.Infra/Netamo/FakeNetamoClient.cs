using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStation.Core.Constants;
using WeatherStation.Core.Interfaces;
using WeatherStation.Model.Enums;
using WeatherStation.Model.KeyFigure;
using WeatherStation.Model.Netamo;

namespace WeatherStation.Infra.Netamo
{
    public class FakeNetamoClient : INetamoClient
    {
        public Task<IEnumerable<KeyFigure>> GetIndoorDataAsync()
        {
            var keyFigures = new List<KeyFigure>()
            {
                new KeyFigure
                {
                    Value = "21.3",
                    Name = "Temperatur inomhus stugan",
                    Unit = "°",
                    Trend = Trend.Up,
                    Type = MeasureType.IndoorTemperature
                },
                new KeyFigure
                {
                    Value = "1013.2",
                    Name = "Lufttryck inomhus stugan",
                    Unit = "hPa",
                    Trend = Trend.Down,
                    Type = MeasureType.IndoorPressure
                },
                new KeyFigure
                {
                    Value = "873",
                    Name = "CO2 inomhus stugan",
                    Unit = "ppm",
                    Type = MeasureType.IndoorCO2
                },
                new KeyFigure
                {
                    Value = "40",
                    Name = "Luftfuktighet inomhus stugan",
                    Unit = "%",
                    Type = MeasureType.IndoorHumidity
                }
            };

            return Task.FromResult(keyFigures.AsEnumerable());
        }

        public Task<IEnumerable<KeyFigure>> GetOutdoorDataAsync()
        {
            var keyFigures = new List<KeyFigure>()
            {
                new KeyFigure
                {
                    Value = "-10.3",
                    Name = "Temperatur utomhus stugan",
                    Unit = "°",
                    Trend = Trend.Up,
                    Type = MeasureType.OutdoorTemperature
                },
                new KeyFigure
                {
                    Value = "55",
                    Name = "Luftfuktighet inomhus stugan",
                    Unit = "%",
                    Type = MeasureType.OutdoorHumidity
                }
            };

            return Task.FromResult(keyFigures.AsEnumerable());
        }

        public Task<StationData> GetStationDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
