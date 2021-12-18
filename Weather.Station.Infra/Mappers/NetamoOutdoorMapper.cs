using System;
using System.Collections.Generic;
using WeatherStation.Core.Interfaces;
using WeatherStation.Model.Enums;
using WeatherStation.Model.KeyFigure;
using WeatherStation.Model.Netamo;

namespace WeatherStation.Infra.Mappers
{
    public class NetamoOutdoorMapper : IKeyFigureMapper<OutdoorDashboardData>
    {
        public IEnumerable<KeyFigure> MapToMany(OutdoorDashboardData item)
        {
            var keyFigures = new List<KeyFigure>
            {
                new KeyFigure
                {
                    Value = item.Temperature.ToString(),
                    Name = "Temperatur utomhus stugan",
                    Unit = "°",
                    Trend = MapToTrend(item.temp_trend)
                },

                new KeyFigure
                {
                    Value = item.Humidity.ToString(),
                    Name = "Luftfuktighet utomhus stugan",
                    Unit = "%"
                }
            };

            return keyFigures;
        }

        OutdoorDashboardData IKeyFigureMapper<OutdoorDashboardData>.MapFrom(KeyFigure keyFigure)
        {
            throw new NotImplementedException();
        }

        public KeyFigure MapTo(OutdoorDashboardData item)
        {
            throw new NotImplementedException();
        }

        private static Trend MapToTrend(string trend)
        {
            if (trend == "stable")
            {
                return Trend.Stable;
            }
            else if (trend == "down")
            {
                return Trend.Down;
            }

            return Trend.Up;
        }
    }
}
