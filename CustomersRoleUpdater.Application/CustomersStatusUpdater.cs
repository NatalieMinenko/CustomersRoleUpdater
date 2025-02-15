using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;
using Contract;
using System.Linq;
using AutoMapper;
using static MassTransit.ValidationResultExtensions;

namespace CustomersRoleUpdater.Application;

public class CustomersStatusUpdater(
    ICustomerDataService customerDataRequest,
    IMapper mapper
) : ICustomersStatusUpdater
{
    public ListCustomerId UpdateCustomerRoles(List<Customer> customers)
    {
        var result = customers.Select(p => p.Id).DistinctBy(p => p).ToList();
        return mapper.Map<ListCustomerId>(result);
    }

    public List<Customer> GetCustomerIdsWithoutNull(List<Customer>[] customers)
    {
        return customers.SelectMany(c => c).Where(c => c != null).ToList();
    }

    public async Task<ListCustomerId>? GetAllCustomersAndUpdateRoleAsync()
    {
        var task1 = customerDataRequest.GetCustomersForUpdateByBirhtdayAsync();
        var task2 = customerDataRequest.GetCustomersForUpdateByCountTransactionAsync();
        var task3 = customerDataRequest.GetCustomersForUpdateBySumTransactionAsync();
        var results = await Task.WhenAll(task1, task2, task3);
        if (results.Length > 0)
        {
            var customers = GetCustomerIdsWithoutNull(results);
            if(customers.Count() > 0)
            return  UpdateCustomerRoles(customers);
        }
        return null;
    }
}






