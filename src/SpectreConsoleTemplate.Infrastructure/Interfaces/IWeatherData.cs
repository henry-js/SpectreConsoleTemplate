using Refit;
using SpectreConsoleTemplate.Infrastructure.Models;

namespace SpectreConsoleTemplate.Infrastructure.Interfaces;

public interface IWeatherData
{
    [Get(path: "/data/2.5/weather?q={cityName}&appid={apiKey}")]
    Task<WeatherModel> GetWeather(string cityName, string apiKey);
}