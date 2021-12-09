using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using WeatherStation.Model.KeyFigure;
using WeatherStation.API.Settings;
using WeatherStation.Core.Extension;
using WeatherStation.Core.Interfaces;

namespace WeatherStation.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/solarEnergy")]
    public class SolarEnergyController : ControllerBase
    {
        private readonly AppSettings settings;
        private readonly IKeyFigureProvider provider;

        public SolarEnergyController(IOptions<AppSettings> settings, IEnumerable<IKeyFigureProvider> providers)
        {
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            this.provider = providers.Resolve(this.settings.SolarEnergyLastMonth);
        }

        [Route("LastMonth")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await this.provider.Get() ?? new KeyFigure();

            return this.Ok(model);
        }
    }
}
