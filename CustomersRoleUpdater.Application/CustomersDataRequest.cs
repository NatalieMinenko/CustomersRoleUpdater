using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;

namespace CustomersRoleUpdater.Application;

public class CustomersDataRequest : ICustomersDataRequest
{
    private readonly CommonHttpClient<List<Customer>> _httpClient;
    private readonly string _baseUrl = "localhost:1111";

    public CustomersDataRequest(HttpMessageHandler? handler = null)
    {
        _httpClient = new CommonHttpClient<List<Customer>>(_baseUrl, handler);
    }

    Guid guid = Guid.NewGuid();

    public async Task<List<Customer>>GetCustomersForUpdateByBirhtdayAsync()
    {
        var query = new Dictionary<string, string>()
        {
            ["month"] = "2",
            ["count"] = "42",
        };

        var resultQuery = RequestUriUtil.GetUriWithQueryString(query);
        return await _httpClient.GetRequest($"/birthday/{resultQuery}");
        //return new List<Customer>() {new Customer(){ Id = guid, Role=Role.Regular}};
    }
    public async Task<List<Customer>> GetCustomersForUpdateByCountTransactionAsync()
    {
        //return await _httpClient.GetRequest("/count/");
        return new List<Customer>() { new Customer() { Id = guid, Role = Role.Regular } };
    }
    public async Task<List<Customer>> GetCustomersForUpdateBySumTransactionAsync()
    {
        //return await _httpClient.GetRequest("/sum/");
        return new List<Customer>() { new Customer() { Id = guid, Role = Role.Regular } };
    }
}

