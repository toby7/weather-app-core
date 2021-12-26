using WeatherStation.Core.Constants;
using WeatherStation.Core.Interfaces;
using WeatherStation.Model.Enums;
using WeatherStation.Model.KeyFigure;
using WeatherStation.Model.Netamo;

namespace WeatherStation.Infra.Mappers
{
    public class NetamoIndoorMapper : IKeyFigureMapper<IndoorDashboardData>
    {
        public IEnumerable<KeyFigure> MapToMany(IndoorDashboardData item)
        {
            var keyFigures = new List<KeyFigure>
            {
                new KeyFigure
                {
                    Value = item.Temperature.ToString(),
                    Name = "Temperatur",
                    Unit = "°",
                    Trend = MapToTrend(item.temp_trend),
                    Type = MeasureType.IndoorTemperature
                },
                new KeyFigure
                {
                    Value = item.Pressure.ToString(),
                    Name = "Lufttryck",
                    Unit = "hPa",
                    Trend = MapToTrend(item.pressure_trend),
                    Type = MeasureType.IndoorPressure
                },
                new KeyFigure
                {
                    Value = item.CO2.ToString(),
                    Name = "CO2",
                    Unit = "ppm",
                    Type = MeasureType.IndoorCO2
                },
                new KeyFigure
                {
                    Value = item.Humidity.ToString(),
                    Name = "Luftfuktighet",
                    Unit = "%",
                    Type = MeasureType.IndoorHumidity
                }
            };

            //keyFigures.Add(new KeyFigure
            //{
            //    Value = item.Noise.ToString(),
            //    Name = "Ljudnivå inomhus stugan",
            //    Unit = "dB"
            //});

            return keyFigures;
        }

        public IndoorDashboardData MapFrom(KeyFigure keyFigure)
        {
            throw new NotImplementedException();
        }

        public KeyFigure MapTo(IndoorDashboardData item)
        {
            throw new NotImplementedException();
        }

        private static Trend MapToTrend(string trend)
        {
            if(trend == "stable")
            {
                return Trend.Stable;
            }
            else if(trend == "down")
            {
                return Trend.Down;
            }

            return Trend.Up;
        }
    }
}
