namespace mediator.Weather;

public interface IWeatherService
{
    Task<WeatherData> GetCurrentWeather(string city);
    
    Task<WeatherForecastData> GetWeekWeather(string city);
}