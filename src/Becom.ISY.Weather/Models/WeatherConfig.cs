namespace Becom.ISY.Weather.Models;

public class WeatherConfig
{
    public string AppId { get; set; } = "";
    public string Link { get; set; } = "";
    public Company[] Companies { get; set; } = default!;
}

public class Company
{
    public int MapId { get; set; }
    public string Description { get; set; } = "";
    public string City { get; set; } = "";
    public string CID { get; set; } = "";
    public string Country { get; set; } = "";
    public double Lat { get; set; }
    public double Lng { get; set; }
    public string TimeZone { get; set; } = "";
}
