using CustomersRoleUpdater.Application;
using CustomersRoleUpdater.Application.Interfaces;

namespace WorkerService.Presentation;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRequestService _requestService;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _requestService = new RequestService();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var result = await _requestService.GetCustomersFromJsonPlaceholderAsync();
            await Task.Delay(6000, stoppingToken);
        }
    }
}
