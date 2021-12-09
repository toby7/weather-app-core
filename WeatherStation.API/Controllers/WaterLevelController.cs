using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherStation.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Model.KeyFigure;
    using WeatherStation.Core.Extension;
    using WeatherStation.Core.Interfaces;

    // using Newtonsoft.Json;

    [ApiController]
    [Produces("application/json")]
    [Route("api/water")]
    public class WaterLevelController : ControllerBase
    {
        private readonly IKeyFigureProvider provider;

        public WaterLevelController(IEnumerable<IKeyFigureProvider> providers)
        {
            this.provider = providers.Resolve("waterLevel");
        }

        [Route("level")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var data = await provider.Get() ?? new KeyFigure();

            return this.Ok(data);
        }
    }
}
