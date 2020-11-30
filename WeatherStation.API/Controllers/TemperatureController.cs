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
            this.provider = providers?.FirstOrDefault(x => x.Name.Equals(this.settings.OutdoorTemperature)) ?? throw new ArgumentNullException(nameof(providers));
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(DateTimeOffset fromDate)//Task<ActionResult<TemperatureDto>> Get()
        //{
        //    var model = await this.providers?.SingleOrDefault(x => x.Name.Equals("outdoorTemperature"))?.Get(fromDate) ?? Enumerable.Empty<KeyFigure>();
        //    //await this.tableService.GetLatestEntities("Temperature");
        //    //await this.temperatureRepository.Add("test", "test");//.GetLastObservation();
        //    return this.Ok(model);
        //}

        [HttpGet]
        public async Task<IActionResult> Get()//Task<ActionResult<TemperatureDto>> Get()
        {
            var model = await this.provider.Get() ?? new KeyFigure();

            return this.Ok(model);
        }
    }

    //public interface ITemperatureRepository
    //{
    //    Task<TemperatureDto> GetLastObservation();
    //}

    //public class MockedTemperatureRepository : ITemperatureRepository
    //{
    //    public Task<TemperatureDto> GetLastObservation()
    //    {
    //        return Task.FromResult(new TemperatureDto()
    //        {
    //            CurrentTemperature = 22.3,
    //            OldTemperature = 22.1
    //        });
    //    }
    //}

    //public class TemperatureDto
    //{
    //    public double CurrentTemperature { get; set; }

    //    public double OldTemperature { get; set; }

    //    public Trend Trend => this.CurrentTemperature > this.OldTemperature ? Trend.Positive : Trend.Negative;
    //}

    //public enum Trend
    //{
    //    Positive,
    //    Negative,
    //    Unchanged
    //}
}
