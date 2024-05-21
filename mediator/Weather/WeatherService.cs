using mediator.Weather;

namespace mediator;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private const string AppId = "9af89291e5e450b347fddca88530a82c";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherData> GetCurrentWeather(string city)
    {
        var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={AppId}&units=metric");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherData>(content);
        }

        return null;
    }
    
    public async Task<WeatherForecastData> GetWeekWeather(string city)
    {
        var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={AppId}&units=metric");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherForecastData>(content);
        }

        return null;
    }
}
public class WeatherData
{
    public MainData Main { get; set; }
    public string Name { get; set; }
}

public class MainData
{
    public double Temp { get; set; }
}

public class WeatherForecastData
{
    public List<WeatherForecastItem> List { get; set; }
}

public class WeatherForecastItem
{
    [JsonProperty("dt")]
    public long Timestamp { get; set; }

    public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime;
    
    public MainData Main { get; set; }
}
