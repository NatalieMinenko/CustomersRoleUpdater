using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;

namespace CustomersRoleUpdater.Application;

public class CustomerStatusUpdater : ICustomerStatusUpdater

{
    private readonly ICustomerDataRequest _customerDataRequest;

    public CustomerStatusUpdater()
    {
        _customerDataRequest = new CustomersDataRequest();
    }

    public List<Customer> UpdateCustomerRoles(IEnumerable <List<Customer>> customers)
    {
        return customers.SelectMany(c => c).DistinctBy(p => p.Id).ToList();
    }
    public async Task RequestProcessingAsync()
    {
        var task1 = _customerDataRequest.GetCustomersForUpdateByBirhtdayAsync();
        var task2 = _customerDataRequest.GetCustomersForUpdateByCountTransactionAsync();
        var task3 = _customerDataRequest.GetCustomersForUpdateBySumTransactionAsync();
        var results = await Task.WhenAll(task1, task2, task3);
        if (results.Length > 0)
        {
            var resultCustomers = UpdateCustomerRoles(results);
        }
    }
}






