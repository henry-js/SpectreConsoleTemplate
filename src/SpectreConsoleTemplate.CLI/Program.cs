using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using Serilog;
using Serilog.Events;
using Spectre.Console.Cli;
using SpectreConsoleTemplate.CLI.DependencyInjection;
using SpectreConsoleTemplate.Infrastructure.Interfaces;

namespace SpectreConsoleTemplate.CLI;
public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        // The initial "bootstrap" logger is able to log errors during start-up. It's completely replaced by the
        // logger configured in `UseSerilog()` below, once configuration and dependency-injection have both been
        // set up successfully.
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var host = CreateHostBuilder()
                   .Build();
        // Run the application.
        return await host.RunAsync(args);
    }

    static IHostBuilder CreateHostBuilder()
    {
        var host = Host.CreateDefaultBuilder()
            .UseSerilog((context, services, configuration) =>
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning))
            .ConfigureServices((services) =>
            {
                // Register services here
                services.AddRefitClient<IWeatherData>()
                    .ConfigureHttpClient((config) => config.BaseAddress = new Uri("https://api.openweathermap.org"));
                // Add command line with default command
                services.AddCommandLine<DefaultCommand>(config =>
                {
                    config.SetApplicationName("comply");
                });
            });
        return host;
    }
}