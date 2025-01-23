using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;

namespace CustomersRoleUpdater.Application;

public class RequestService : IRequestService
{
    private readonly CommonHttpClient _httpClient;
    public RequestService(HttpMessageHandler? handler = null)
    {
        _httpClient = new CommonHttpClient("localhost:1111", handler);
    }

    Guid guid = Guid.NewGuid();

    public async Task<List<Customer>>GetCustomersForUpdateByBirhtdayAsync()
    {
        //return await _httpClient.GetRequest("/birthday/");
        return new List<Customer>() {new Customer(){ Id = guid, Role=Role.Regular}};
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

