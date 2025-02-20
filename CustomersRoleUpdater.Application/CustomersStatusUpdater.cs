using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using Contract;

namespace CustomersRoleUpdater.Application;

public class CustomersStatusUpdater(
    ICustomersDataService customerDataRequest
    //IMapper mapper
) : ICustomersStatusUpdater
{
    private List<Guid> UpdateCustomerRoles(List<Customer>[] customers)
    {
        return customers.SelectMany(c => c).DistinctBy(p => p).Select(p => p.Id).ToList();
    }

    public async Task<ListCustomerId> GetAllCustomersAndUpdateRoleAsync()
    {
        var task1 = customerDataRequest.GetCustomersForUpdateByBirhtdayAsync();
        var task2 = customerDataRequest.GetCustomersForUpdateByCountTransactionAsync();
        var task3 = customerDataRequest.GetCustomersForUpdateBySumTransactionAsync();

        var customers = await Task.WhenAll(task1, task2, task3);

        ListCustomerId customerIds = new();
        customerIds.CustomerIds = UpdateCustomerRoles(customers);

        return customerIds;
    }
}






