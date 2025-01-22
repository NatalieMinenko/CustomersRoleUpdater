using CustomersRoleUpdater.Application;
using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using Microsoft.Extensions.Options;

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
            _logger.LogInformation("Customers RoleUpdater running at: {time}", DateTimeOffset.Now);
            var task1 = _requestService.GetCustomersForUpdateByBirhtdayAsync();
            var task2 = _requestService.GetCustomersForUpdateByCountTransactionAsync();
            var task3 = _requestService.GetCustomersForUpdateBySumTransactionAsync();
            var results = await Task.WhenAll(task1, task2, task3);
            var customers = new List<Customer>(); 
            if (results.Length>0)
            {
                foreach (var items in results)
                {
                    foreach (var item in items)
                    {
                        customers.Add(item);
                    }
                }
            }
            var T = customers;
            await Task.Delay(60000, stoppingToken);//TODO
        }
    }
}
