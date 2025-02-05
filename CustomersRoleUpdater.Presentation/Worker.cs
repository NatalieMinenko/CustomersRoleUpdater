
using CustomersRoleUpdater.Application.Interfaces;

namespace WorkerService.Presentation;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ICustomersStatusUpdater _customerStatusUpdater;

    public Worker(ILogger<Worker> logger, ICustomersStatusUpdater customerStatusUpdater)
    {
        _logger = logger;
        _customerStatusUpdater = customerStatusUpdater;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Customers RoleUpdater running at: {time}", DateTimeOffset.Now);
            await _customerStatusUpdater.GetAllCustomersAndUpdateRoleAsync();
            await Task.Delay(60000, stoppingToken);
        }
    }
}