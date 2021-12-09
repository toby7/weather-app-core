using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using WeatherStation.API.Providers;
using WeatherStation.API.Services;
using WeatherStation.API.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherStation.Core.Interfaces;

namespace WeatherStation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather station", Version = "v1" });
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddTransient<ITableService, TableService>();

            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Singleton<IKeyFigureProvider, WaterLevelProvider>(),
                ServiceDescriptor.Singleton<IKeyFigureProvider, OutdoorTemperatureProvider>(),
                ServiceDescriptor.Singleton<IKeyFigureProvider, SolarEnergyProvider>()
            });

            //RestService.For<INetamoApi>(
            //    "https://api.github.com",
            //    new RefitSettings 
            //    {
            //        AuthorizationHeaderValueGetter = () =>
            //        {
            //            Task.FromResult("hej");
            //        }
            //    });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<SolarEdgeSettings>(Configuration.GetSection("SolarEdge"));

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather station V1");
            });
        }
    }
}
