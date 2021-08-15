namespace WeatherStation.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Model.KeyFigure;
    using Settings;

    [ApiController]
    [Route("api/temperature")]
    public class TemperatureController : ControllerBase
    {
        private readonly AppSettings settings;
        private readonly IKeyFigureProvider provider;

        public TemperatureController(IOptions<AppSettings> settings, IEnumerable<IKeyFigureProvider> providers)
        {
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            this.provider = providers?.FirstOrDefault(x => x.Name.Equals(this.settings.OutdoorTemperature)) ?? throw new KeyNotFoundException(nameof(providers));
        }

        [HttpGet]
        public async Task<IActionResult> Get()//Task<ActionResult<TemperatureDto>> Get()
        {
            var model = await this.provider.Get() ?? new KeyFigure();

            return this.Ok(model);
        }
    }
}
