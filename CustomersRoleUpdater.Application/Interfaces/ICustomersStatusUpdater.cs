using CustomersRoleUpdater.Application.Models;
using RoleRenewalContract;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomersStatusUpdater
{
    public Task<CustomerIdsModel> UpdateCustomerRoles(IEnumerable<List<Customer>> customers);
    public Task<CustomerIdsModel> GetAllCustomersAndUpdateRoleAsync();
}
