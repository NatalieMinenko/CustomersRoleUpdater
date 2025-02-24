
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CustomersRoleUpdater.Application.Integrations;

public class CommonHttpClient
{
    private readonly HttpClient _httpClient = new();
    private readonly JsonSerializerOptions _options;
    private readonly ILogger<CommonHttpClient> _logger;

    public CommonHttpClient( ILogger<CommonHttpClient> logger, string baseUrl, HttpMessageHandler? handler = null)
    {
        if (handler != null)
        {
            _httpClient = new HttpClient(handler);
        }
        else
        {
            logger.LogInformation("let's do some magic now");
            var handler2 = new HttpClientHandler();
            handler2.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler2.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            _httpClient = new HttpClient(handler2);
        }
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 5, 30);
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _logger = logger;
    }

    public async Task<T?> GetRequest<T>(string path)
    {
        var response = await _httpClient.GetAsync(path);
        var g = response;
        if (!response.IsSuccessStatusCode)
            _logger.LogError($"api error: {(int)response.StatusCode}-{response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, _options);
        return result;
    }
}
