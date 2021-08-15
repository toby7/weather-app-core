using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherStation.API.Interfaces;
using WeatherStation.API.Settings;
using WeatherStation.Core.Extension;
using WeatherStation.Model.KeyFigure;
using WeatherStation.Model.SolarEdge;

namespace WeatherStation.API.Providers
{
    public class SolarEnergyProvider : IKeyFigureProvider
    {
        private readonly SolarEdgeSettings settings;
        private readonly string baseUrl; 

        public SolarEnergyProvider(IOptions<SolarEdgeSettings> settings)
        {
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            this.baseUrl = $"{this.settings.BaseUrl}/{this.settings.SiteId}";
        }

        public string Name => nameof(SolarEnergyProvider);

        public async Task<KeyFigure> Get()
        {
            using var client = new HttpClient(); // TODO Create httpClientFactory to resuse clients
            var url = $"{this.baseUrl}/energy.json?" +
                      $"timeUnit=DAY&" +
                      $"startDate={DateTime.Now:yyyy-MM-dd}&" +
                      $"endDate={DateTime.Now:yyyy-MM-dd}&" +
                      $"api_key={this.settings.ApiKey}";

            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Root>(json);

            var keyFigure = new KeyFigure()
            {
                Name = "Solenergi idag",
                Unit = "kWh",
                Updated = DateTime.UtcNow,
                Value = data.timeFrameEnergy.energy
                    .ToKiloWattHour()
                    .ToRounded(2)
                    .ToString()
            };

            return keyFigure;
        }

        public Task<IEnumerable<KeyFigure>> Get(DateTimeOffset? fromDate = null)
        {
            throw new NotImplementedException();
        }
    }
}
