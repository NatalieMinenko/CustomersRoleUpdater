using Contract;
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomersStatusUpdater
{
    public Task <ListCustomerId> GetAllCustomersAndUpdateRoleAsync();
}
