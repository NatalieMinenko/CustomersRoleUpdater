
using CustomersRoleUpdater.Application.Interfaces;
using MassTransit;
using RoleRenewalContract;
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
            var listCustomerId = await customerStatusUpdater.GetAllCustomersAndUpdateRoleAsync();
            await bus.Publish<CustomerIdsModel>(listCustomerId);
            await Task.Delay(60000, stoppingToken);
        }
    }
}