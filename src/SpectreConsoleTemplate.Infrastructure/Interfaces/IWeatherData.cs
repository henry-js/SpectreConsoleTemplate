using Refit;
using SpectreConsoleTemplate.Infrastructure.Models;

namespace SpectreConsoleTemplate.Infrastructure.Interfaces;

public interface IWeatherData
{
    [Get(path: "/data/2.5/weather?q={cityName}&appid={apiKey}")]
    Task<WeatherModel> GetWeather(string cityName, string apiKey);

    [Get(path: "/geo/1.0/direct?q={cityName},gb&limit=1&appid={apiKey}")]
    Task<List<LocationModel>> GetLocation(string cityName, string apiKey);
}