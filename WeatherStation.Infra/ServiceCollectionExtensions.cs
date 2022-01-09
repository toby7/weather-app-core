using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WeatherStation.Infra.Netamo;
using WeatherStation.Core.Interfaces;
using WeatherStation.Core.Models.Settings;
using WeatherStation.Infra.Cache;
using WeatherStation.Infra.Mappers;
using WeatherStation.Model.Netamo;

namespace WeatherStation.Infra;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        services.AddOptions<NetamoApiSettings>().Configure<IConfiguration>((settings, configuration) =>
        {
            configuration.GetSection("NetamoApiSettings").Bind(settings);
        });

        services.AddSingleton<ICacheService, CacheService>();
        services.AddSingleton<INetamoClient, NetamoClient>();
        //services.AddSingleton<INetamoClient, FakeNetamoClient>();

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<IKeyFigureMapper<IndoorDashboardData>, NetamoIndoorMapper>();
        services.AddSingleton<IKeyFigureMapper<OutdoorDashboardData>, NetamoOutdoorMapper>();

        return services;          
    }
}