using System.ComponentModel;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using SpectreConsoleTemplate.CLI.Commands.Settings;
using SpectreConsoleTemplate.Infrastructure.Interfaces;

namespace SpectreConsoleTemplate.CLI.Commands;

public sealed class WeatherCommand : AsyncCommand<WeatherSettings>
{
    private readonly IAnsiConsole console;
    private readonly IWeatherData openWeather;
    private readonly ILogger<WeatherCommand> logger;

    public async override Task<int> ExecuteAsync(CommandContext context, WeatherSettings settings)
    {
        logger.LogInformation("In DefaultCommand");
        console.MarkupLine("[yellow]Hello[/] [blue]World[/]!");
        var response = await openWeather.GetWeather("Kidderminster", "0a36e5187006b980e1b4011ab3d8b331");
        console.WriteLine(JsonSerializer.Serialize(response) ?? "null response received");
        Console.ReadLine();
        return 0;
    }
}