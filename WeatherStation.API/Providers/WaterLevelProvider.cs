namespace WeatherStation.API.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Model.KeyFigure;
    using Newtonsoft.Json;
    using Core.Extension;
    using Model.WaterLevel;
    using WeatherStation.Core.Interfaces;

    public class WaterLevelProvider : IKeyFigureProvider
    {
        public string Name => "waterLevel";

        public async Task<KeyFigure> Get()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://opendata-download-ocobs.smhi.se/api/version/latest/parameter/6/station/2056/period/latest-day/data.json");
            var json = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<WaterLevelJsonObject>(json);

            var model = new KeyFigure()
            {
                Name = data.Parameter.Name,
                Unit = data.Parameter.Unit,
                Updated = DateTimeOffset.FromUnixTimeMilliseconds(data.Updated).DateTime,
                Value = data.Values.LastOrDefault()?.Value.ToString() ?? "error",
                Trend = data.Values
                    .Reverse()
                    .Skip(1)
                    .Take(3)
                    .Select(y => y.Value)
                    .ToTrend(data.Values.LastOrDefault().Value),
            };

            return model;
        }

        public  Task<IEnumerable<KeyFigure>> Get(DateTimeOffset? fromDate)
        {
            return null;
            //using var client = new HttpClient();
            //var response = await client.GetAsync("https://opendata-download-ocobs.smhi.se/api/version/latest/parameter/6/station/2056/period/latest-day/data.json");
            //var json = await response.Content.ReadAsStringAsync();

            //var data = JsonConvert.DeserializeObject<WaterLevelJsonObject>(json);

            //var model = new KeyFigure()
            //{
            //    Name = data.Parameter.Name,
            //    Unit = data.Parameter.Unit,
            //    Updated = new DateTime(data.Updated),
            //    Value = data.Values.LastOrDefault()?.Value.ToString() ?? "error",
            //};

            //return model;
        }
    }
}