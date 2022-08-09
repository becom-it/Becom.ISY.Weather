using Becom.ISY.Weather.Models;

namespace Becom.ISY.Weather.Handlers;

public class OpenWeatherMapHttpHandler : DelegatingHandler
{
    private readonly WeatherConfig _config;

    public OpenWeatherMapHttpHandler(WeatherConfig config) : base(new HttpClientHandler())
    {
        _config = config;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var url = request.RequestUri?.ToString();
        url = $"{url}&units=metric&appid={_config.AppId}&lang=de";
        request.RequestUri = new Uri(url);
        return base.SendAsync(request, cancellationToken);
    }
}
