
using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;

namespace CustomersRoleUpdater.Application;

public class RequestService : IRequestService
{
    private readonly CommonHttpClient<CustomersWithDateOfBirthday> _httpClient;
    public RequestService(HttpMessageHandler? handler = null)
    {
        _httpClient = new CommonHttpClient<CustomersWithDateOfBirthday>("https://jsonplaceholder.typicode.com", handler);
    }

    public async Task<List<CustomersWithDateOfBirthday?>> GetCustomersFromJsonPlaceholderAsync()
    {
        return await _httpClient.GetRequest("/users/");
    }

}

