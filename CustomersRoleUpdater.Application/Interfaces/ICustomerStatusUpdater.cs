using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomerStatusUpdater
{
    public List<Customer> UpdateCustomerRoles(IEnumerable<List<Customer>> customers);
}
