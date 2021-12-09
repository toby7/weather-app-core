using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.API.Providers
{
    using System.Threading;
    using Core.Extension;
    using Microsoft.Azure.Cosmos.Table;
    using Microsoft.Azure.Cosmos.Table.Queryable;
    using Microsoft.Extensions.Configuration;
    using Model.KeyFigure;
    using Model.Temperature;
    using Newtonsoft.Json;
    using Services;
    using WeatherStation.Core.Interfaces;

    public class OutdoorTemperatureProvider : IKeyFigureProvider
    {
        private readonly IConfiguration configuration;

        public OutdoorTemperatureProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Name => "outdoorTemperature";

        public async Task<KeyFigure> Get()
        {
            var conString = this.configuration.GetValue<string>("ConnectionStrings:TableStorage");
            var storageAccount = CloudStorageAccount.Parse(conString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("Temperature");

            var query = table.CreateQuery<TemperatureTableModel>()
                .AsQueryable()
                .Take(4)
                .AsTableQuery();
            var tableModel = await query.ExecuteAsync(CancellationToken.None);
            var json = tableModel.Select(x => x.Json);
            var content = json.Select(x =>
                JsonConvert.DeserializeObject<TemperatureModel>(x, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            if (content.IsNullOrEmpty())
            {
                return new KeyFigure();
            }

            var lastObservation = content.FirstOrDefault();

            var model = new KeyFigure()
            {
                Name = "Utomhustemperatur",
                Unit = "°C",
                Updated = lastObservation.Time.ToLocalTime(),
                Value = lastObservation.Temperature
                .ToRounded()
                .ToString(),
                Trend = content
                .Select(x => x.Temperature)
                .Skip(1)
                .Take(3)
                .ToTrend(lastObservation.Temperature),
            };

            return model;
        }

        public async Task<IEnumerable<KeyFigure>> Get(DateTimeOffset? fromDate)
        {
            throw new NotImplementedException();

            var conString = this.configuration.GetValue<string>("ConnectionStrings:TableStorage");
            var storageAccount = CloudStorageAccount.Parse(conString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("Temperature");
            var query = (from entity in table.CreateQuery<TemperatureTableModel>()
                where entity.Timestamp > new DateTimeOffset(DateTime.UtcNow.AddDays(-7))
                select entity).AsTableQuery();


            var test = await query.ExecuteAsync(CancellationToken.None);
            var json = test.Where((x, i) => i % 6 == 0).Select(y => y.Json);
            var content = json.Select(x => JsonConvert.DeserializeObject<TemperatureModel>(x, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            var model = content.Select(x => new KeyFigure()
            {
                Name = "Utomhustemperatur Stureby",
                Unit = "°C",
                Updated = x.Time,
                Value = Math
                .Round(x.Temperature, 1, MidpointRounding.AwayFromZero)
                .ToString()
                .Replace(',', '.') ?? "error"
            });

            return model;
        }
    }
}
