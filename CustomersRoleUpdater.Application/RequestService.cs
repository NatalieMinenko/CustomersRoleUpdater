
using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;

namespace CustomersRoleUpdater.Application;

public class RequestService : IRequestService
{
    private readonly CommonHttpClient<Customer> _httpClient;
    public RequestService(HttpMessageHandler? handler = null)
    {
        _httpClient = new CommonHttpClient<Customer>("https://jsonplaceholder.typicode.com", handler);
    }

    public async Task<Customer?> GetCustomersFromJsonPlaceholderAsync()
    {
        return await _httpClient.GetRequest($"/customers");
    }
}

