using Becom.ISY.Weather.Contracts;
using Becom.ISY.Weather.Models;

namespace Becom.ISY.Weather.Extensions;

public static class OpenWeatherResponseExtensions
{
    public static List<WeatherResponse> MapToWeatherData(this OpenWeatherResponse raw, WeatherConfig config)
    {
        var l = new List<WeatherResponse>();

        if (raw == null) return l;

        foreach (var r in raw.OpenWeatherList)
        {
            var c = config.Companies.First(x => x.MapId == r.Id);
            var vm = new WeatherResponse
            {
                MapId = c.MapId,
                Description = c.Description,
                City = c.City,
                Sunrise = DateTimeOffset.FromUnixTimeMilliseconds(r.Sys.Sunrise).DateTime,
                Sunset = DateTimeOffset.FromUnixTimeMilliseconds(r.Sys.Sunset).DateTime,
                LocalTime = DateTime.UtcNow.AddSeconds(r.Sys.Timezone),
                Temperature = r.Main.Temp,
                WeatherType = r.Weather.First().Main,
                WeatherIcon = r.Weather.First().Icon.TranslateIcon()
            };
            l.Add(vm);
        }

        return l;
    }

    public static string TranslateIcon(this string icon)
    {
        return icon switch
        {
            "11d" => "thunderstorm.png",
            "09d" => "shower_rain.png",
            "10d" => "rain.png",
            "10n" => "rain.png",
            "13d" => "snow.png",
            "50d" => "mist.png",
            "50n" => "mist.png",
            "01d" => "clear_day.png",
            "01n" => "clear_night.png",
            "02d" => "fewclouds_day.png",
            "02n" => "fewclouds_night.png",
            "03d" => "clouds_day.png",
            "03n" => "clouds_day.png",
            "04d" => "broken_clouds_day.png",
            "04n" => "broken_clouds_day.png",
            _ => "",
        };
    }
}
