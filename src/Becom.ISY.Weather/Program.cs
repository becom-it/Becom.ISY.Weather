using Becom.ISY.Weather.Extensions;
using Becom.ISY.Weather.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWeather(builder.Configuration);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

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