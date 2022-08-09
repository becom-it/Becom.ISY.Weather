namespace Becom.ISY.Weather.Contracts;

public class WeatherResponse
{
    public int MapId { get; set; }

    public string Description { get; set; } = "";

    public string City { get; set; } = "";

    public DateTime Sunrise { get; set; }

    public DateTime Sunset { get; set; }

    public double Temperature { get; set; }

    public string WeatherType { get; set; } = "";

    public string WeatherIcon { get; set; } = "";

    public DateTime LocalTime { get; set; }

    public bool Show { get; set; } = true;
}
