using MediatR;

namespace mediator.Weather;

public class WeatherHandler : IRequestHandler<WeatherNowCommand, string>, IRequestHandler<WeatherWeekCommand, string>
{
    private WeatherService _ws;

    public WeatherHandler(WeatherService ws)
    {
        _ws = ws;
    }
    
    public async Task<string> Handle(WeatherNowCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.cityName);
        return (await _ws.GetCurrentWeather(request.cityName)).Main.Temp.ToString();
    }

    public async Task<string> Handle(WeatherWeekCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.cityName);
        return string.Join("\n", (await _ws.GetWeekWeather(request.cityName)).List
            .Select(s => $"{s.Main.Temp.ToString()}   {s.DateTime.ToString()}"));
    }
}