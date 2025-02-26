
using CustomersRoleUpdater.Application.Interfaces;
using MYPBackendMicroserviceIntegrations.Messages;

namespace CustomersRoleUpdater.Application;

public class CustomersStatusUpdater(
    ICustomersDataService customerDataRequest
) : ICustomersStatusUpdater
{
    private List<Guid> UpdateCustomerRoles(List<Guid>[] customers)
    {
        return customers.SelectMany(c => c).DistinctBy(p => p).ToList();
    }

    public async Task<CustomerRoleUpdateIdsMessage> GetAllCustomersAndUpdateRoleAsync()
    {
        var task1 = customerDataRequest.GetCustomersForUpdateByBirhtdayAsync();
        var task2 = customerDataRequest.GetCustomersForUpdateByCountTransactionAsync();
        var task3 = customerDataRequest.GetCustomersForUpdateBySumTransactionAsync();

        var customers = await Task.WhenAll(task1, task2, task3);

        CustomerRoleUpdateIdsMessage customerIds = new();
        customerIds.VipCustomerIds = UpdateCustomerRoles(customers);

        return customerIds;
    }
}






