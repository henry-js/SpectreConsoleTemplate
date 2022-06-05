using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;
using SpectreConsoleTemplate.CLI.Commands.Setting;

namespace SpectreConsoleTemplate.CLI.Commands;

public class ConsoleCommand : AsyncCommand<ConsoleSettings>
{
    private ILogger<ConsoleCommand> _logger;
    public override async Task<int> ExecuteAsync(CommandContext context, ConsoleSettings settings)
    {
        // Logger.LogInformation("Mandatory: {Mandatory}", settings.Mandatory);
        // Logger.LogInformation("Optional: {Optional}", settings.Optional);
        // Logger.LogInformation("CommandOptionFlag: {CommandOptionFlag}", settings.CommandOptionFlag);
        // Logger.LogInformation("CommandOptionValue: {CommandOptionValue}", settings.CommandOptionValue);
        return await Task.FromResult(0);
    }

    public ConsoleCommand(ILogger<ConsoleCommand> logger)
    {
        _logger = logger;
    }
}