using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;
using MassTransit;
using CustomersRoleUpdater.Application.Mappings;

namespace WorkerService.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Logging.ClearProviders();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        builder.Logging.AddSerilog();

        builder.Services.AddWindowsService(options =>
        {
            options.ServiceName = "Worker for update role";
        });
        LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });

        builder.Services.AddHostedService<Worker>();

        builder.Logging.AddConfiguration();
        builder.Configuration.GetSection("Logging");

        builder.Services.AddSingleton<ICustomersDataService, CustomersDataService>();
        builder.Services.AddSingleton<ICustomersStatusUpdater, CustomersStatusUpdater>();

        builder.Services.AddAutoMapper(typeof(CustomersMapperProfile));

        var host = builder.Build();
        host.Run();
    }
}