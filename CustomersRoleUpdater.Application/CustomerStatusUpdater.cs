using CustomersRoleUpdater.Application.Models;
using CustomersRoleUpdater.Application.Interfaces;

namespace CustomersRoleUpdater.Application;

public class CustomerStatusUpdater : ICustomerStatusUpdater
{
    public List<Customer> UpdateCustomerRoles(IEnumerable <List<Customer>> customers)
    {
        return customers.SelectMany(c => c).DistinctBy(p => p.Id).ToList();
    }
}



