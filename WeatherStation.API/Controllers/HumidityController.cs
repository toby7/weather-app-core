using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherStation.API.Controllers
{   
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HumidityController : ControllerBase
    {
        //private readonly INetamoApi netamoApi;

        //public HumidityController(INetamoApi netamoApi)
        //{
        //    this.netamoApi = netamoApi;
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var netamoApi = RestService.For<INetamoApi>("https://api.netatmo.com/api/", new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => {
                    var token = "61ab561fbc9b7a5a63743d60|b80e1ce48ddd3d85fcf797fd2b183ac4";
                    return Task.FromResult(token);
                }
            });

            try
            {
                var test = await netamoApi.GetMeasure("70:ee:50:84:23:24");

                return this.Ok(test);
            }
            catch (ApiException ex)
            {
                return this.StatusCode(500);
            }
        }
    }

    public interface INetamoApi
    {
        [Headers("Authorization: Bearer")]
        [Get("/getmeasure?device_id={deviceId}&scale=30min&type=temperature")]
        Task<NetamoResponse> GetMeasure(string deviceId);
    }

    public class Body
    {
        public int beg_time { get; set; }
        public int step_time { get; set; }
        public List<List<double>> value { get; set; }
    }

    public class NetamoResponse
    {
        public List<Body> body { get; set; }
        public string status { get; set; }
        public double time_exec { get; set; }
        public int time_server { get; set; }
    }
}
