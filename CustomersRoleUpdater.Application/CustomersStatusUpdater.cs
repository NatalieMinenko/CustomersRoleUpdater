using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using Contract;
using AutoMapper;

namespace CustomersRoleUpdater.Application;

public class CustomersStatusUpdater(
    ICustomerDataService customerDataRequest,
    IMapper mapper
) : ICustomersStatusUpdater
{
    public CustomerIdsModel UpdateCustomerRoles(IEnumerable <List<Customer>> customers)
    {
        var result = customers.SelectMany(c => c).DistinctBy(p => p.Id).ToList();
        return mapper.Map<CustomerIdsModel>(result);
    }
    public async Task<CustomerIdsModel> GetAllCustomersAndUpdateRoleAsync()
    {
        var task1 = customerDataRequest.GetCustomersForUpdateByBirhtdayAsync();
        var task2 = customerDataRequest.GetCustomersForUpdateByCountTransactionAsync();
        var task3 = customerDataRequest.GetCustomersForUpdateBySumTransactionAsync();
        var listCustomers = await Task.WhenAll(task1, task2, task3);
        if (listCustomers.Length > 0)
        {
            return UpdateCustomerRoles(listCustomers);
        }
        return null;
    }
}






