using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

namespace WorkerService.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddWindowsService(options =>
        {
            options.ServiceName = "Worcer for update role";
        });
        LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

        builder.Services.AddHostedService<Worker>();

        builder.Logging.AddConfiguration(
        builder.Configuration.GetSection("Logging"));

        builder.Services.AddScoped<IRequestService, RequestService>();
        builder.Services.AddScoped<ICustomerStatusUpdater, CustomerStatusUpdater>();

        var host = builder.Build();
        host.Run();
    }
}