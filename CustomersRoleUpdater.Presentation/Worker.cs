
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
            try 
            {
                logger.LogInformation("Customers RoleUpdater running at: {time}", DateTimeOffset.Now);

                var list = await customerStatusUpdater.GetAllCustomersAndUpdateRoleAsync();

                await bus.Publish<ListCustomerId>(list);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Message}", ex.Message);
            }
            await Task.Delay(6000, stoppingToken);
        }
    }
}