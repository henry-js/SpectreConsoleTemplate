using henry_js.Cli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Spectre.Console.Cli;


var builder = Host.CreateApplicationBuilder(args);
// IConfiguration config = new ConfigurationBuilder()
//     .AddEnvironmentVariables()
//     .Build();

var services = new ServiceCollection();

builder.Services.AddScoped<IConfiguration>(_ => new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build())
    .AddLogging(configure =>
        configure.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger()
        ));

// var registrar = new DependencyInjectionTypeRegistrar(services);
var registrar = new TypeRegistrar(builder);

var app = new CommandApp(registrar);

app.Configure(config =>
    {
        config.PropagateExceptions();
        config.CaseSensitivity(CaseSensitivity.None)
              .SetApplicationName("henry-js.Cli")
              .ValidateExamples();
    });

return await app.RunAsync(args);