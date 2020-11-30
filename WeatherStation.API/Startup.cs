

namespace WeatherStation.API
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.OpenApi.Models;
    using Providers;
    using Services;
    using Settings;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        // Not a permanent solution, but just trying to isolate the problem
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
            //services.AddTransient<IKeyFigureProvider, WaterLevelProvider>();
            //services.AddTransient<IKeyFigureProvider, OutdoorTemperatureProvider>();

            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IKeyFigureProvider, WaterLevelProvider>(),
                ServiceDescriptor.Scoped<IKeyFigureProvider, OutdoorTemperatureProvider>(),
                ServiceDescriptor.Scoped<IKeyFigureProvider, SolarEnergyMonthlyProvider>()
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<SolarEdgeSettings>(Configuration.GetSection("SolarEdge"));

            services.AddControllers();
        }

        // Configures the HTTP request pipeline.
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


            //app.UseCors(options => options.AllowAnyOrigin());
            
        }
    }
}
