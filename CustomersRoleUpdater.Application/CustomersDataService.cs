using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;
using Microsoft.Extensions.Logging;

namespace CustomersRoleUpdater.Application;

public class CustomersDataService(ILogger<CustomersDataService> logger) : ICustomersDataService
{
    private readonly CommonHttpClient? _httpClient;
    private readonly string _baseUrl = "https://194.87.210.5:12000/api/customers/";

    public CustomersDataService(
        ILogger<CommonHttpClient> clientLogger,
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

        var date = new DateTime(2010, 6, 1);
        var datePlus = new DateTime(2010, 6, 2);// date.AddDays(100);
        var query = new Dictionary<string, string>()
        {
            ["DateStart"] = $"{date}",
            ["DateEnd"] = $"{datePlus}",
        };

        var resultQuery = RequestUriUtil.GetUriWithQueryString(query);
        var r = resultQuery;
        return await _httpClient.GetRequest<List<Customer>>($"birth-date{resultQuery}");
        //return new List<Customer>() {new Customer(){ Id = guid, Role=Role.Regular}};
    }
    public async Task<List<Customer>> GetCustomersForUpdateByCountTransactionAsync()
    {
        return await _httpClient.GetRequest<List<Customer>>("count");
        //return new List<Customer>() { new Customer() { } };
    }
    public async Task<List<Customer>> GetCustomersForUpdateBySumTransactionAsync()
    {
        //return await _httpClient.GetRequest<List<Customer>>?("/sum/");
        return new List<Customer>() { new Customer() {  } };
    }
}

