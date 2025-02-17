using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using Contract;
using System.Linq;
using AutoMapper;
using static MassTransit.ValidationResultExtensions;

namespace CustomersRoleUpdater.Application;

public class CustomersStatusUpdater(
    ICustomersDataService customerDataRequest
    //IMapper mapper
) : ICustomersStatusUpdater
{
    public List<Guid> UpdateCustomerRoles(List<Customer> customers)
    {
        return customers.Select(p => p.Id).DistinctBy(p => p).ToList();
    }

    public List<Customer> GetCustomersWithoutNull(List<Customer>[] customers)
    {
        return customers.SelectMany(c => c).Where(c => c != null).ToList();
    }

    public async Task<ListCustomerId>? GetAllCustomersAndUpdateRoleAsync()
    {
        var task1 = customerDataRequest.GetCustomersForUpdateByBirhtdayAsync();
        var task2 = customerDataRequest.GetCustomersForUpdateByCountTransactionAsync();
        var task3 = customerDataRequest.GetCustomersForUpdateBySumTransactionAsync();

        var results = await Task.WhenAll(task1, task2, task3);

        var customers = GetCustomersWithoutNull(results);

        if (customers.Count > 0)
        {
            ListCustomerId customerIds = new();
            customerIds.CustomerIds = UpdateCustomerRoles(customers);
            return customerIds;
        }
        return null;
    }
}






