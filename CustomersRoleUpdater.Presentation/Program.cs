using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;
using MassTransit;

namespace WorkerService.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
            .AddCommandLine(args)
            .AddEnvironmentVariables();
            

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

        //var url = builder.Configuration.GetRequiredSection("RabbitMq").GetValue<string>("Host")?? string.Empty;
        //var name = builder.Configuration.GetRequiredSection("RabbitMq").GetValue<string>("Name") ?? string.Empty; 
        //var password = builder.Configuration.GetRequiredSection("RabbitMq").GetValue<string>("Password") ?? string.Empty;

        //builder.Services.AddMassTransit(x =>
        //{
        //    x.UsingRabbitMq((context, cfg) =>
        //{
        //    cfg.Host(url, h =>
        //    {
        //        h.Username(name);
        //        h.Password(password);
        //    });
        //});
        //});

        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost:5672/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });

        builder.Logging.AddConfiguration();
        builder.Configuration.GetSection("Logging");

        builder.Services.AddSingleton<ICustomersDataService, CustomersDataService>();
        builder.Services.AddSingleton<ICustomersStatusUpdater, CustomersStatusUpdater>();

        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}