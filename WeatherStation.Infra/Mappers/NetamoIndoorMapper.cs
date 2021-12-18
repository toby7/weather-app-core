﻿using System;
using System.Collections.Generic;
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
            var keyFigures = new List<KeyFigure>();

            keyFigures.Add(new KeyFigure
            {
                Value = item.Temperature.ToString(),
                Name = "Temperatur inomhus stugan",
                Unit = "°",
                Trend = MapToTrend(item.temp_trend)
            });

            keyFigures.Add(new KeyFigure
            {
                Value = item.Pressure.ToString(),
                Name = "Lufttryck inomhus stugan",
                Unit = "hPa",
                Trend = MapToTrend(item.pressure_trend)
            });

            keyFigures.Add(new KeyFigure
            {
                Value = item.CO2.ToString(),
                Name = "CO2 inomhus stugan",
                Unit = "ppm"
            });

            keyFigures.Add(new KeyFigure
            {
                Value = item.Humidity.ToString(),
                Name = "Luftfuktighet inomhus stugan",
                Unit = "%"
            });

            keyFigures.Add(new KeyFigure
            {
                Value = item.Noise.ToString(),
                Name = "Ljudnivå inomhus stugan",
                Unit = "dB"
            });

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