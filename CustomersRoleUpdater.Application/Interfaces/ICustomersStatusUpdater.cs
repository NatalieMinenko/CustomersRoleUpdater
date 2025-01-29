using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomersStatusUpdater
{
    public List<Customer> UpdateCustomerRoles(IEnumerable<List<Customer>> customers);
    public Task GetAllCustomersAndUpdateRoleAsync();
}
