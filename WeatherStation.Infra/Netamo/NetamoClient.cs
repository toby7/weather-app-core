using Microsoft.Extensions.Options;
using Refit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStation.Core.Interfaces;
using WeatherStation.Core.Models.Auth;
using WeatherStation.Core.Models.Settings;
using WeatherStation.Model.KeyFigure;
using WeatherStation.Model.Netamo;

namespace WeatherStation.Infra.Netamo
{
    public sealed class NetamoClient : INetamoClient
    {
        private const int AccessTokenExpiration = 180 * 60;

        private readonly INetamoApi netamoApi;
        private readonly NetamoApiSettings netamoApiSettings;
        private readonly IKeyFigureMapper<IndoorDashboardData> indoorMapper;
        private readonly IKeyFigureMapper<OutdoorDashboardData> outdoorMapper;

        public NetamoClient(
            ICacheService cacheService,
            IOptions<NetamoApiSettings> netamoSettings,
            IKeyFigureMapper<IndoorDashboardData> indoorMapper,
            IKeyFigureMapper<OutdoorDashboardData> outdoorMapper)
        {
            this.indoorMapper = indoorMapper ?? throw new ArgumentNullException(nameof(indoorMapper));
            this.netamoApiSettings = netamoSettings?.Value ?? throw new ArgumentNullException(nameof(netamoSettings));
            //Separate and move elsewhere
            netamoApi = RestService.For<INetamoApi>(this.netamoApiSettings.BaseUrl, new RefitSettings
            {
                AuthorizationHeaderValueGetter = async () =>
                {
                    return await cacheService.GetAndCache(
                        nameof(this.netamoApiSettings.TokenUrl),
                        TimeSpan.FromSeconds(AccessTokenExpiration),
                        GetAccessToken);
                },
            });
            this.outdoorMapper = outdoorMapper;
        }

        public async Task<IEnumerable<KeyFigure>> GetIndoorDataAsync()
        {
            var stationData = await netamoApi.GetStationsData(netamoApiSettings.DeviceId);
            var indoorKeyFigures = indoorMapper.MapToMany(stationData.Content.Body.Devices[0].IndoorDashboardData);

            return indoorKeyFigures;
        }

        public async Task<IEnumerable<KeyFigure>> GetOutdoorDataAsync()
        {
            var stationData = await netamoApi.GetStationsData(netamoApiSettings.DeviceId);
            var outdoorKeyFigures = outdoorMapper.MapToMany(stationData.Content.Body.Devices[0].modules[0].OutdoorDashboardData);

            return outdoorKeyFigures;
        }

        public async Task<StationData> GetStationDataAsync()
        {
            var stationData = await netamoApi.GetStationsData(netamoApiSettings.DeviceId);

            return stationData.Content;
        }

        private async Task<string> GetAccessToken()
        {
            var client = new RestClient(netamoApiSettings.TokenUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", netamoApiSettings.GrantType);
            request.AddParameter("client_id", netamoApiSettings.ClientId);
            request.AddParameter("client_secret", netamoApiSettings.ClientSecret);
            request.AddParameter("username", netamoApiSettings.Username);
            request.AddParameter("password", netamoApiSettings.Password);

            var auth = await client.ExecuteAsync<Authentication>(request);

            return auth.Data.AccessToken;
        }
    }
}
