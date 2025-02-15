using Contract;
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomersStatusUpdater
{
    ListCustomerId UpdateCustomerRoles(List<Customer> customers);

    public Task <ListCustomerId> GetAllCustomersAndUpdateRoleAsync();

    public List<Customer>? GetCustomerIdsWithoutNull(List<Customer>[] customers);
}
