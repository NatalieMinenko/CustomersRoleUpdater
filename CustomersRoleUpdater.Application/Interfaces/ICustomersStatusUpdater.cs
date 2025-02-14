using CustomersRoleUpdater.Application.Models;
using Contract;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomersStatusUpdater
{
    public CustomerIdsModel UpdateCustomerRoles(IEnumerable<List<Customer>> customers);
    public Task<CustomerIdsModel> GetAllCustomersAndUpdateRoleAsync();
}
