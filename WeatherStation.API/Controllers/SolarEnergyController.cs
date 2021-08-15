using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeatherStation.API.Controllers
{
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.Extensions.Options;
    using Model.KeyFigure;
    using Settings;

    [ApiController]
    [Route("api/solarEnergy")]
    public class SolarEnergyController : ControllerBase
    {
        private readonly AppSettings settings;
        private readonly IEnumerable<IKeyFigureProvider> providers;

        public SolarEnergyController(IOptions<AppSettings> settings, IEnumerable<IKeyFigureProvider> providers)
        {
            this.providers = providers ?? throw new ArgumentNullException(nameof(providers)); ;
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        [Route("LastMonth")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await this.providers.FirstOrDefault(x => x.Name.Equals(this.settings.SolarEnergyLastMonth)).Get() ?? new KeyFigure();

            return this.Ok(model);
        }
    }
}
