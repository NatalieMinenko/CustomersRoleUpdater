
using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;

namespace CustomersRoleUpdater.Application;

public class RequestService : IRequestService
{
    private readonly CommonHttpClient<User> _httpClient;
    public RequestService(HttpMessageHandler? handler = null)
    {
        _httpClient = new CommonHttpClient<User>("https://jsonplaceholder.typicode.com", handler);
    }

    public async Task<List<User?>> GetCustomersFromJsonPlaceholderAsync()
    {
        return await _httpClient.GetRequest("/users/");
    }
}

