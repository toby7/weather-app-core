namespace WeatherStation.API.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Net.Http;
    using Core.Extension;
    using Interfaces;
    using Microsoft.Extensions.Options;
    using Model.KeyFigure;
    using Model.SolarEdge;
    using Newtonsoft.Json;
    using Settings;

    public class SolarEnergyMonthlyProvider : IKeyFigureProvider
    {
        private readonly SolarEdgeSettings settings;
        private readonly string baseUrl; 

        public SolarEnergyMonthlyProvider(IOptions<SolarEdgeSettings> settings)
        {
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            this.baseUrl = $"{this.settings.BaseUrl}/{this.settings.SiteId}";
        }

        public string Name => nameof(SolarEnergyMonthlyProvider);

        public async Task<KeyFigure> Get()
        {
            using var client = new HttpClient(); // TODO Create httpClientFactory to resuse clients
            var url = $"{this.baseUrl}/timeFrameEnergy.json?" +
                      $"startDate={DateTime.Now.AddMonths(-1):yyyy-MM-dd}&" +
                      $"endDate={DateTime.Now:yyyy-MM-dd}&" +
                      $"api_key={this.settings.ApiKey}";

            var response = await client.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Root>(json);

            var model = new KeyFigure()
            {
                Name = "Solenergi senaste månaden",
                Unit = "kWh",
                Updated = DateTime.Now,
                Value = data.timeFrameEnergy.energy
                    .ToKiloWattHour()
                    .ToRounded(2)
                    .ToString()
            };

            return model;
        }

        public Task<IEnumerable<KeyFigure>> Get(DateTimeOffset? fromDate = null)
        {
            throw new NotImplementedException();
        }
    }
}
