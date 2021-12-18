using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherStation.Core.Interfaces;

namespace WeatherStation.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/station")]
    public class StationController : ControllerBase
    {
        private readonly INetamoClient netamoClient;

        public StationController(INetamoClient netamoClient)
        {
            this.netamoClient = netamoClient;
        }

        [Route("indoor")]
        [HttpGet]
        public async Task<IActionResult> GetIndoor()
        {
            var indoorKeyFigures = await netamoClient.GetIndoorDataAsync();

            return this.Ok(indoorKeyFigures);
        }

        [Route("outdoor")]
        [HttpGet]
        public async Task<IActionResult> GetOutdoor()
        {
            var outdoorKeyFigures = await netamoClient.GetOutdoorDataAsync();

            return this.Ok(outdoorKeyFigures);
        }

        [Route("status")]
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            return this.NoContent();

            //var stationData = await netamoClient.GetStationDataAsync();

            //return this.Ok(stationData);
        }
    }
}
