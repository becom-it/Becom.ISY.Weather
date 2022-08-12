using Becom.ISY.Weather.Extensions;
using Becom.ISY.Weather.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, config) => {
    config.AddJsonFile("appsettings.k8s.json", true, true);
});

builder.Services.AddWeather(builder.Configuration);

var app = builder.Build();

app.MapGet("/", async (IWeatherService weatherService) =>
{
    var res = await weatherService.LoadWeather();
    if(res.HasException)
    {
        return Results.BadRequest(res.Exception);
    }

    return Results.Ok(res.ReturnedValue);
});

app.MapGet("/healthz", () => "OK");

app.Run();