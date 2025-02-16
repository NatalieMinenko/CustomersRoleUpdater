
using CustomersRoleUpdater.Application.Interfaces;
using MassTransit;
using Contract;

namespace WorkerService.Presentation;

public class Worker(
    ILogger<Worker> logger,
    ICustomersStatusUpdater customerStatusUpdater,
    IBus bus
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Customers RoleUpdater running at: {time}", DateTimeOffset.Now);

            var list = await customerStatusUpdater.GetAllCustomersAndUpdateRoleAsync();

            if (list is not null)
                await bus.Publish<ListCustomerId>(list);


            await Task.Delay(6000, stoppingToken);
        }
    }
}