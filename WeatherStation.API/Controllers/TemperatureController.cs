namespace WeatherStation.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Model.KeyFigure;
    using Settings;
    using WeatherStation.Core.Extension;
    using WeatherStation.Core.Interfaces;

    [ApiController]
    [Produces("application/json")]
    [Route("api/temperature")]
    public class TemperatureController : ControllerBase
    {
        private readonly AppSettings settings;
        private readonly IKeyFigureProvider provider;

        public TemperatureController(IOptions<AppSettings> settings, IEnumerable<IKeyFigureProvider> providers)
        {
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            this.provider = providers.Resolve(this.settings.OutdoorTemperature);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await provider.Get() ?? new KeyFigure();

            return this.Ok(model);
        }
    }
}
