using Becom.ISY.Weather.Handlers;
using Becom.ISY.Weather.Models;
using Becom.ISY.Weather.Services;
using FlintSoft.Tools.Exceptions;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Becom.ISY.Weather.Extensions;

public static class WeatherExtensions
{
    public static void AddWeather(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        var wconfig = new WeatherConfig();
        configuration.GetSection("WeatherConfig").Bind(wconfig);

        if (wconfig.Equals(new WeatherConfig()))
        {
            throw new MissingConfigurationException("ConfluenceConfig");
        }

        services.TryAddSingleton(wconfig);

        services.AddTransient<OpenWeatherMapHttpHandler>();
        services.AddHttpClient("owm", c =>
        {
            c.BaseAddress = new Uri(wconfig.Link);
        }).ConfigurePrimaryHttpMessageHandler<OpenWeatherMapHttpHandler>();

        services.TryAddScoped<IWeatherService, WeatherService>();
    }
}
