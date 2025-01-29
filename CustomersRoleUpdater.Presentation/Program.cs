using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.DependencyInjection;

namespace WorkerService.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddWindowsService(options =>
        {
            options.ServiceName = "Worker for update role";
        });
        LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

        builder.Services.AddHostedService<Worker>();

        builder.Logging.AddConfiguration(
        builder.Configuration.GetSection("Logging"));

        builder.Services.AddSingleton<ICustomerDataService, CustomersDataService>();
        builder.Services.AddSingleton<ICustomersStatusUpdater, CustomersStatusUpdater>();

        var host = builder.Build();
        host.Run();
    }
}