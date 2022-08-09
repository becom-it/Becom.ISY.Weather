using Becom.ISY.Weather.Contracts;
using Becom.ISY.Weather.Extensions;
using Becom.ISY.Weather.Models;
using FlintSoft.Tools.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Becom.ISY.Weather.Services;

public interface IWeatherService
{
    Task<ReturnModel<List<WeatherResponse>>> LoadWeather();
}

public class WeatherService : IWeatherService
{
    const string CACHKEY = "Weather";

    private readonly ILogger<WeatherService> _logger;
    private readonly WeatherConfig _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _memoryCache;

    public WeatherService(ILogger<WeatherService> logger, WeatherConfig config, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
    {
        _logger = logger;
        _config = config;
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
    }

    public async Task<ReturnModel<List<WeatherResponse>>> LoadWeather()
    {
        if (!_memoryCache.TryGetValue(CACHKEY, out List<WeatherResponse> data))
        {
            var semaphore = new SemaphoreSlim(1, 1);

            try
            {
                await semaphore.WaitAsync();

                if (!_memoryCache.TryGetValue(CACHKEY, out data))
                {
                    var newData = await loadData();
                    _memoryCache.Set(CACHKEY, newData, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
                    return new ReturnModel<List<WeatherResponse>>(newData);
                }
                else
                {
                    return new ReturnModel<List<WeatherResponse>>(data);
                }
            }
            catch (Exception ex)
            {
                return new ReturnModel<List<WeatherResponse>>(new(), ex);
            }
            finally
            {
                semaphore.Release();
            }
        }
        else
        {
            return new ReturnModel<List<WeatherResponse>>(data);
        }
    }

    private async Task<List<WeatherResponse>> loadData()
    {
        try
        {
            _logger.LogInformation("Loading data from the open weather map web service...");

            var client = _httpClientFactory.CreateClient("owm");

            var groups = _config.Companies.Select(x => x.MapId.ToString()).Aggregate((a, x) => $"{a},{x}");

            _logger.LogDebug($"Loading weather data for map ids: {groups}...");
            var res = await client.GetAsync($"group?id={groups}");

            if (res.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request successfull! Deserialyzing data...");
                var body = await res.Content.ReadAsStringAsync();
                var data = await JsonSerializer.DeserializeAsync<OpenWeatherResponse>(await res.Content.ReadAsStreamAsync());
                if (data != null && data.OpenWeatherList.Any())
                {
                    return data.MapToWeatherData(_config);
                }
                else
                {
                    throw new HttpRequestException("Didn't receive any data from open weather map web api!", null, System.Net.HttpStatusCode.NoContent);
                }
            }
            else
            {
                throw new HttpRequestException(String.Format("Error calling open weather map web api! Got status code {0}", res.StatusCode), null, res.StatusCode);
            }
        }
        catch (HttpRequestException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error when loading data from the open weather map web api: {ex.Message}", ex);
        }
    }
}
