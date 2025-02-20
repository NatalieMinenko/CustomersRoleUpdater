using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;
using Microsoft.Extensions.Logging;

namespace CustomersRoleUpdater.Application;

public class CustomersDataService(ILogger<CustomersDataService> logger) : ICustomersDataService
{
    private readonly CommonHttpClient _httpClient;
    private readonly string _baseUrl = "https://github.com/";

    public CustomersDataService(
        ILogger<CommonHttpClient>clientLogger,
        ILogger<CustomersDataService> logger,
        HttpMessageHandler? handler = null 
        ) : this(logger)
    {
        _httpClient = new CommonHttpClient(clientLogger, _baseUrl, handler);
    }

    Guid guid = Guid.NewGuid();

    public async Task<List<Customer>>GetCustomersForUpdateByBirhtdayAsync()
    {
        logger.LogInformation("started query by Birhtday");
        var query = new Dictionary<string, string>()
        {
            ["month"] = "2",
            ["count"] = "42",
        };

        var resultQuery = RequestUriUtil.GetUriWithQueryString(query);
        return await _httpClient.GetRequest<List<Customer>>($"/birthday/{resultQuery}");
        //return new List<Customer>() {new Customer(){ Id = guid, Role=Role.Regular}};
    }
    public async Task<List<Customer>> GetCustomersForUpdateByCountTransactionAsync()
    {
        //return await _httpClient.GetRequest<List<Customer>>?("count");
        return new List<Customer>() { new Customer() { Id = guid, Role = Role.Regular } };
    }
    public async Task<List<Customer>> GetCustomersForUpdateBySumTransactionAsync()
    {
        //return await _httpClient.GetRequest<List<Customer>>?("/sum/");
        return new List<Customer>() { new Customer() { Id = guid, Role = Role.Regular } };
    }
}

