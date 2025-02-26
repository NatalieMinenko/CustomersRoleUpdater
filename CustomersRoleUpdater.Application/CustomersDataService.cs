using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;

namespace CustomersRoleUpdater.Application;

public class CustomersDataService(ILogger<CustomersDataService> logger) : ICustomersDataService
{
    private readonly CommonHttpClient? _httpClient;
    private readonly string _baseUrl = "https://localhost:7083/"; //"https://194.87.210.5:12000/api/customers/"; //

    public CustomersDataService(
        ILogger<CommonHttpClient> clientLogger,
        ILogger<CustomersDataService> logger,
        HttpMessageHandler? handler = null 
        ) : this(logger)
    {
        _httpClient = new CommonHttpClient(clientLogger, _baseUrl, handler);
    }

    public async Task<List<Guid>>GetCustomersForUpdateByBirhtdayAsync()
    {
        logger.LogInformation($"started query by Birhtday, time: {DateTime.Now.Minute} minut");

        var dateStart = DateTime.Now.AddDays(-70).ToString("yyyy-MM-dd");
        var dateEnd = DateTime.Now.AddDays(-65).ToString("yyyy-MM-dd");
  
        var query = new Dictionary<string, string>()
        {
            ["DateStart"] = $"{dateStart}",
            ["DateEnd"] = $"{dateEnd}",
        };

        var resultQuery = RequestUriUtil.GetUriWithQueryString(query);
        var response = await _httpClient.GetRequest<List<Customer>>($"api/customers/birth-date{resultQuery}");
        var customerIds = GetGuidFromCustomer(response);
        logger.LogInformation($"finish query by Birhtday, time:{DateTime.Now.Minute} minut");
        return customerIds;

        //return new List<Guid>() {};
    }
    public async Task<List<Guid>> GetCustomersForUpdateByCountTransactionAsync()
    {
        logger.LogInformation($"started query by transactions count, time: {DateTime.Now.Minute} minut");

        var dateStart = DateTime.Now.AddDays(-70).ToString("yyyy-MM-dd");
        var dateEnd = DateTime.Now.AddDays(-65).ToString("yyyy-MM-dd"); 
        Console.WriteLine(dateStart);
        Console.WriteLine(dateEnd);

        var query = new Dictionary<string, string>()
        {
            ["DateStart"] = $"{dateStart}",
            ["DateEnd"] = $"{dateEnd}",
        };

        var resultQuery = RequestUriUtil.GetUriWithQueryString(query);
        var response = await _httpClient.GetRequest<List<Transaction>>($"api/customers/{resultQuery}");
        var customerIds = FilterTransactionToGetCustomerId(response);
        logger.LogInformation($"finish query by transactions count, time:{DateTime.Now.Minute} minut");
        return customerIds;

        //return new List<Guid>() {};
    }
    public async Task<List<Guid>> GetCustomersForUpdateBySumTransactionAsync()
    {
        //return await _httpClient.GetRequest<List<Customer>>?("api/customers/sum/");
        return new List<Guid>() {};
    }

    private List<Guid> FilterTransactionToGetCustomerId(List<Transaction>transaction)
    {
        return transaction.Where(t => t.TransactionType != (TransactionType)2)
            .GroupBy(c => c.CustomerId).Where(g => g.Count() > 5).Select(i => i.Key).ToList();
    }

    private List<Guid> GetGuidFromCustomer(List<Customer> customers)
    {
        return customers.Select(c => c.Id).ToList();
    }
}

