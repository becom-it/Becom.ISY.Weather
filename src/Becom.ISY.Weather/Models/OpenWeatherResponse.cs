using System.Text.Json.Serialization;

namespace Becom.ISY.Weather.Models;

public partial class OpenWeatherResponse
{
    [JsonPropertyName("cnt")]
    public long Cnt { get; set; }

    [JsonPropertyName("list")]
    public List<OpenWeather> OpenWeatherList { get; set; } = new();
}

public partial class OpenWeather
{
    [JsonPropertyName("coord")]
    public Coord Coord { get; set; } = new();

    [JsonPropertyName("sys")]
    public Sys Sys { get; set; } = new();

    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; } = new();

    [JsonPropertyName("main")]
    public Main Main { get; set; } = new();

    [JsonPropertyName("visibility")]
    public long Visibility { get; set; }

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; } = new();

    [JsonPropertyName("clouds")]
    public Clouds Clouds { get; set; } = new();

    [JsonPropertyName("dt")]
    public long Dt { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}

public partial class Clouds
{
    [JsonPropertyName("all")]
    public long All { get; set; }
}

public partial class Coord
{
    [JsonPropertyName("lon")]
    public double Lon { get; set; }

    [JsonPropertyName("lat")]
    public double Lat { get; set; }
}

public partial class Main
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }

    [JsonPropertyName("temp_min")]
    public double TempMin { get; set; }

    [JsonPropertyName("temp_max")]
    public double TempMax { get; set; }

    [JsonPropertyName("pressure")]
    public long Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public long Humidity { get; set; }

    [JsonPropertyName("sea_level")]
    public long? SeaLevel { get; set; }

    [JsonPropertyName("grnd_level")]
    public long? GrndLevel { get; set; }
}

public partial class Sys
{
    [JsonPropertyName("country")]
    public string Country { get; set; } = "";

    [JsonPropertyName("timezone")]
    public long Timezone { get; set; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }
}

public partial class Weather
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("main")]
    public string Main { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = "";
}

public partial class Wind
{
    [JsonPropertyName("speed")]
    public double Speed { get; set; }

    [JsonPropertyName("deg")]
    public long Deg { get; set; }
}
