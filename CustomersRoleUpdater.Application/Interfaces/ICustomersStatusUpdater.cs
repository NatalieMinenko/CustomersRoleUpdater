using Contract;
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomersStatusUpdater
{
    List<Guid> UpdateCustomerRoles(List<Customer> customers);

    public Task <ListCustomerId> GetAllCustomersAndUpdateRoleAsync();

    public List<Customer>? GetCustomersWithoutNull(List<Customer>[] customers);
}
