using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application;

public class UserStatusUpdater
{
//    private static readonly HttpClient httpClient = new HttpClient();

//    private const string BirthdayApiUrl = "https://jsonplaceholder.typicode.com";
//    private const string TransactionsApiUrl = "https://jsonplaceholder.typicode.com";
//    //private const string  = "https://jsonplaceholder.typicode.com";


//    public async Task<List<string>> GetCustomersWithDateOfBirthdayAsync()
//    {
//        List<string> customersWithDateOfBirthday = new List<string>();
//        DateTime tomorrow = DateTime.UtcNow.AddDays(1).Date;

//        var response = await httpClient.GetAsync($"{BirthdayApiUrl}?date={tomorrow:yyyy-MM-dd}");
//        if (response.IsSuccessStatusCode)
//        {
//            var customerList = await response.Content.ReadFromJsonAsync<List<CustomersWithDateOfBirthday>>();
//            foreach (var customer in customerList)
//            {
//                customersWithDateOfBirthday.Add(customer.Name); // или user.Username в зависимости от вашей модели
//            }
//        }
//        return customersWithDateOfBirthday;
//    }

//    public async Task<List<string>> GetUsersWithTransactionsAsync()
//    {
//        List<string> customersWithTransactions = new List<string>();

//        var response = await httpClient.GetAsync($"{TransactionsApiUrl}?minTransactions=42 & period=month");
//        if (response.IsSuccessStatusCode)
//        {
//            var customerList = await response.Content.ReadFromJsonAsync<List<CustomerTransaction>>();
//            foreach (var customer in customerList)
//            {
//                customersWithTransactions.Add(customer.Name);
//            }
//        }
//        return customersWithTransactions;
//    }

//    public async Task UpdateUserStatusesAsync()
//    {
//        var customersWithDateOfBirthday = await GetCustomersWithDateOfBirthdayAsync();
//        var customersWithTransactions = await GetUsersWithTransactionsAsync();

//        // Логика для обработки обновления статусов пользователей
//        // Например, отправка сообщений со статусами в очередь
//    }
//}


