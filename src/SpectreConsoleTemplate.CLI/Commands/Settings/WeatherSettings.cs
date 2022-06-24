using Spectre.Console.Cli;

namespace SpectreConsoleTemplate.CLI.Commands.Settings;

public class WeatherSettings : BaseSettings
{

    [CommandOption("--latitude")]
    public double? Latitude { get; set; }
    [CommandOption("--longitude")]
    public double? Longitude { get; set; }
    [CommandOption("-s|--set-location")]
    public string Location { get; set; } = null!;
    [CommandOption("-a|--apikey")]
    public string ApiKey { get; set; } = null!;
    [CommandOption("-x|--exclude <categories>")]
    public List<ExcludeCategories> Exclude { get; set; } = new();
    [CommandOption("-u|--units <units>")]
    public Units Units { get; set; }
}

public enum Units
{
    Metric,
    Imperial,
}

[Flags]
public enum ExcludeCategories
{
    None = 0,
    Current = 1,
    Minutely = 2,
    Hourly = 4,
    Daily = 8,
    Alerts = 16,
}