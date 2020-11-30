using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.API.Controllers
{
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model.KeyFigure;
    using Newtonsoft.Json;

    // using Newtonsoft.Json;

    [ApiController]
    [Route("api/water")]
    public class WaterLevelController : ControllerBase
    {
        private readonly IEnumerable<IKeyFigureProvider> _keyFigureProviders;

        public WaterLevelController(IEnumerable<IKeyFigureProvider> keyFigureProviders)
        {
            this._keyFigureProviders = keyFigureProviders ?? throw new NullReferenceException(nameof(IKeyFigureProvider));
        }

        [Route("level")]
        [HttpGet]
        public async Task<ActionResult> Get()//Task<ActionResult<TemperatureDto>> Get()
        {
            //var model = null;//await this.tableService.GetLatestEntities("Temperature");
            //await this.temperatureRepository.Add("test", "test");//.GetLastObservation();

            var data = await _keyFigureProviders.SingleOrDefault(x => x.Name.Equals("waterLevel"))?.Get() ?? new KeyFigure();

            return this.Ok(data);
        }
    }
}
