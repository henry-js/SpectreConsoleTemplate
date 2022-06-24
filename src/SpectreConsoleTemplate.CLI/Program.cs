
using System.Reflection.Metadata.Ecma335;
using D20Tek.Spectre.Console.Extensions.Injection;
using Microsoft.Extensions.Configuration;
using Refit;
using Serilog;
using Serilog.Context;
using Spectre.Console.Cli;
using SpectreConsoleTemplate.CLI.Commands;
using SpectreConsoleTemplate.CLI.Commands.Settings;
using SpectreConsoleTemplate.Infrastructure.Interfaces;

IConfiguration config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();
var settings = new RefitSettings();

services.AddScoped<IConfiguration>(_ => new ConfigurationBuilder().AddEnvironmentVariables().Build())
        .AddLogging(configure =>
        configure.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger()
        ))
        .AddRefitClient<IWeatherData>(settings)
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.openweathermap.org"));

var registrar = new DependencyInjectionTypeRegistrar(services);

var app = new CommandApp(registrar);

app.Configure(config =>
    {
        config.CaseSensitivity(CaseSensitivity.None)
              .SetApplicationName("SpectreConsoleTemplate")
              .ValidateExamples();
        config.AddBranch<WeatherSettings>("weather", weather =>
        {
            weather.AddCommand<WeatherGetCommand>("get");
        });
    });

return await app.RunAsync(args);