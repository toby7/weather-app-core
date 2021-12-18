using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStation.Model.KeyFigure;
using WeatherStation.Model.Netamo;

namespace WeatherStation.Core.Interfaces
{
    public interface INetamoClient
    {      
        Task<IEnumerable<KeyFigure>> GetIndoorDataAsync();
        Task<IEnumerable<KeyFigure>> GetOutdoorDataAsync();
        Task<StationData> GetStationDataAsync();
    }
}
