using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using SpectreConsoleTemplate.CLI.Commands.Settings;
using SpectreConsoleTemplate.Infrastructure.Interfaces;

namespace SpectreConsoleTemplate.CLI.Commands;

public class NewBaseType : AsyncCommand<WeatherGetSettings>
{
    protected IAnsiConsole console;
    protected IWeatherData openWeather;
    protected ILogger<WeatherGetCommand> logger;
    protected IConfiguration config;

    public async override Task<int> ExecuteAsync(CommandContext context, WeatherGetSettings settings)
    {

        return 0;
    }
}

public class WeatherGetCommand : NewBaseType
{
    public WeatherGetCommand(IAnsiConsole console, IWeatherData openWeather, ILogger<WeatherGetCommand> logger, IConfiguration config)
    {
        this.console = console;
        this.openWeather = openWeather;
        this.logger = logger;
        this.config = config;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, WeatherGetSettings settings)
    {
        if ((settings.Latitude == null) || (settings.Longitude == null))
        {
            Console.ReadLine();
            // 1. Ask host for city & country and save to file
            // 2. Use geocoding api to get lat/lon and save to file
            // 3. 
        }

        // logger.LogInformation("In DefaultCommand");
        // console.MarkupLine("[yellow]Hello[/] [blue]World[/]!");
        // var response = await openWeather.GetWeather("Kidderminster", "0a36e5187006b980e1b4011ab3d8b331");
        // console.WriteLine(JsonSerializer.Serialize(response) ?? "null response received");
        settings.ApiKey ??= config.GetValue<string>("OPENWEATHER:APPID");
        var response = await openWeather.GetLocation("London", settings.ApiKey);
        console.WriteLine(JsonSerializer.Serialize(response));
        Console.ReadLine();

        return 0;
    }
}