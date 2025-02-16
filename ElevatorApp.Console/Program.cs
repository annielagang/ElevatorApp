using ElevatorApp.Application.Interfaces;
using ElevatorApp.Core.Application;
using ElevatorApp.Core.Application.Interfaces;
using ElevatorApp.Core.Application.Managers;
using ElevatorApp.Core.Application.Services;
using ElevatorApp.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var cts = new CancellationTokenSource();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<Building>(provider
            => new Building(10, 4, provider.GetRequiredService<IElevatorMovementService>()));
        services.AddSingleton<IElevatorMovementService, ElevatorMovementService>();
        services.AddSingleton<IRequestGeneratorService, RequestGeneratorService>();
        services.AddSingleton<IElevatorDispatcherService, ElevatorDispatcherService>();
        services.AddSingleton<ElevatorManager>();
        services.AddLogging(config => config.AddConsole());
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
var manager = host.Services.GetRequiredService<ElevatorManager>();

logger.LogInformation("Starting Elevator Simulation... Type 'end' to stop.");

var simulationTask = manager.RunSimulationAsync();

// Listen for user input in the background
_ = Task.Run(() =>
{
    while (true)
    {
        string? input = Console.ReadLine();
        if (input?.Trim().ToLower() == "end")
        {
            cts.Cancel();
            manager.StopSimulation();
            logger.LogInformation("Stopping simulation after all pending requests are completed...");
            break;
        }
    }
});

await simulationTask;


