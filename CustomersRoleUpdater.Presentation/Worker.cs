
using CustomersRoleUpdater.Application.Interfaces;
using MassTransit;
using MYPBackendMicroserviceIntegrations.Messages;

namespace WorkerService.Presentation;

public class Worker(
    ILogger<Worker> logger,
    ICustomersStatusUpdater customerStatusUpdater,
    IBus bus
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(1000);
        while (!stoppingToken.IsCancellationRequested)
        {
            try 
            {
                logger.LogInformation("Customers RoleUpdater running at: {time}", DateTime.Now);

                var list = await customerStatusUpdater.GetAllCustomersAndUpdateRoleAsync();
                 
                await bus.Publish<CustomerRoleUpdateIdsMessage>(list);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Message}", ex.Message);
            }
            await Task.Delay(60000, stoppingToken);
        }
    }
}