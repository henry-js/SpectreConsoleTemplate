using System.Text.Json;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using SpectreConsoleTemplate.Infrastructure.Interfaces;

namespace SpectreConsoleTemplate.CLI;

public sealed class DefaultCommand : AsyncCommand
{
    private readonly IAnsiConsole _console;
    private readonly IWeatherData _openWeather;
    private readonly ILogger<DefaultCommand> _logger;

    public DefaultCommand(IAnsiConsole console, IWeatherData openWeather, ILogger<DefaultCommand> logger)
    {
        _console = console;
        _openWeather = openWeather;
        _logger = logger;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        _logger.LogInformation("In DefaultCommand");
        _console.MarkupLine("[yellow]Hello[/] [blue]World[/]!");
        var response = await _openWeather.GetWeather("Kidderminster", "0a36e5187006b980e1b4011ab3d8b331");
        Console.WriteLine(JsonSerializer.Serialize(response) ?? "null response received");
        Console.ReadLine();
        return 0;
    }
}