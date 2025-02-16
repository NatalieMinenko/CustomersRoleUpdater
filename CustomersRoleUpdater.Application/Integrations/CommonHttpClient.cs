
using System.Text.Json;

namespace CustomersRoleUpdater.Application.Integrations;

internal class CommonHttpClient
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _options;

    public CommonHttpClient(string baseUrl, HttpMessageHandler? handler = null)
    {
        if (handler != null)
        {
            _httpClient = new HttpClient(handler);
        }
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 5, 30);
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<T?> GetRequest<T>(string path)
    {
        var response = await _httpClient.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(content, _options);
            return result;
        }
        else
        {
            //response.EnsureSuccessStatusCode();
            return default(T);
        }
    }
}
