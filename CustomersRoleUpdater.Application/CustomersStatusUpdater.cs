using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;

namespace CustomersRoleUpdater.Application;

public class CustomersStatusUpdater(ICustomersDataRequest customerDataRequest) : ICustomersStatusUpdater
{
    public List<Customer> UpdateCustomerRoles(IEnumerable <List<Customer>> customers)
    {
        return customers.SelectMany(c => c).DistinctBy(p => p.Id).ToList();
    }
    public async Task RequestProcessingAsync()
    {
        var task1 = customerDataRequest.GetCustomersForUpdateByBirhtdayAsync();
        var task2 = customerDataRequest.GetCustomersForUpdateByCountTransactionAsync();
        var task3 = customerDataRequest.GetCustomersForUpdateBySumTransactionAsync();
        var results = await Task.WhenAll(task1, task2, task3);
        if (results.Length > 0)
        {
            var resultCustomers = UpdateCustomerRoles(results);
        }
    }
}






