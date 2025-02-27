using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Integrations;
using Microsoft.Extensions.Logging;


namespace CustomersRoleUpdater.Application;

public class CustomersDataService(ILogger<CustomersDataService> logger) : ICustomersDataService
{
    private readonly CommonHttpClient? _httpClient;
    private readonly string _baseUrl = "https://194.87.210.5:12000/"; 

    public CustomersDataService(
        ILogger<CommonHttpClient> clientLogger,
        ILogger<CustomersDataService> logger,
        HttpMessageHandler? handler = null 
    ) : this(logger)
    {
        _httpClient = new CommonHttpClient(clientLogger, _baseUrl, handler);
    }

    public string DateBirthdayStart = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
    public string DateBirthdayEnd = DateTime.Now.ToString("yyyy-MM-dd");
    public string DateStartTransactionCount = DateTime.Now.AddDays(-42).ToString("yyyy-MM-dd");
    public string DateEndTransactionCount = DateTime.Now.ToString("yyyy-MM-dd");
    public DateTime DateStartTransactionSum = DateTime.Now.AddDays(-30);
    public DateTime DateEndTransactionSum = DateTime.Now;

    public async Task<List<Guid>>GetCustomersForUpdateByBirhtdayAsync()
    {
        logger.LogInformation($"started query by Birhtday, time: {DateTime.Now.Minute} minut");
 
        var query = new Dictionary<string, string>()
        {
            ["DateStart"] = $"{DateBirthdayStart}",
            ["DateEnd"] = $"{DateBirthdayEnd}",
        };

        var resultQuery = RequestUriUtil.GetUriWithQueryString(query);
        var response = await _httpClient.GetRequest<List<Customer>>($"api/customers/birth-date{resultQuery}");

        var customerIds = GetGuidFromCustomer(response);
        logger.LogInformation(
            $"finish query by Birhtday, time:{DateTime.Now.Minute} minut, count {response.Count}");
        return customerIds;
    }
    public async Task<List<Guid>> GetCustomersForUpdateByCountTransactionAsync()
    {
        logger.LogInformation($"started query by transactions count, time: {DateTime.Now.Minute} minut");

        var query = new Dictionary<string, string>()
        {
            ["DateStart"] = $"{DateStartTransactionCount}",
            ["DateEnd"] = $"{DateEndTransactionCount}",
        };

        var resultQuery = RequestUriUtil.GetUriWithQueryString(query);
        var response = await _httpClient.GetRequest<List<Transaction>>($"api/transactions/by-period{resultQuery}");
        var copyList = new List<Transaction>(response);
        var listGuids = FilterTransactionBySumTransaction(copyList, DateStartTransactionSum, DateEndTransactionSum);
        var customerIds = FilterTransactionByCount(response);
        var result = customerIds.Union(listGuids).ToList();
        logger.LogInformation(
            $"finish query by transactions, time: {DateTime.Now.Minute} minut, count without filter {response.Count}, \n" +
            $" count after filters {result.Count}");
        return result;
    }
   
    private List<Guid> FilterTransactionByCount(List<Transaction>transaction)
    {
        return transaction.Where(t => t.TransactionType != (TransactionType)2)
            .GroupBy(c => c.CustomerId).Where(g => g.Count() > 5).Select(i => i.Key).ToList();
    }

    private List<Guid> FilterTransactionBySumTransaction(List<Transaction> response, DateTime dateStart, DateTime dateEnd)
    {
         return response.Where(t => t.Date >= dateStart && t.Date <= dateEnd).
                GroupBy(g => g.CustomerId).Where(w =>
                    w.Where(y => y.TransactionType != (TransactionType)2).Sum(s => s.Amount) -
                    w.Where(y => y.TransactionType == (TransactionType)2).Sum(s => s.Amount) > 13000).
                    Select(k => k.Key).ToList();   
    }

    private List<Guid> GetGuidFromCustomer(List<Customer> customers)
    {
        return customers.Select(c => c.Id).ToList();
    }
}

