using Refit;
using System.Threading.Tasks;
using WeatherStation.Model.Netamo;

namespace WeatherStation.Core.Interfaces
{
    public interface INetamoApi
    {
        [Headers("Authorization: Bearer")]
        [Get("/getmeasure?device_id={deviceId}&scale=30min&type=temperature")]
        Task<ApiResponse<Measure>> GetMeasure(string deviceId);

        [Headers("Authorization: Bearer")]
        [Get("/getstationsdata?device_id={deviceId}")]
        Task<ApiResponse<StationData>> GetStationsData(string deviceId);
    }
}
