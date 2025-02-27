
using MYPBackendMicroserviceIntegrations.Messages;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomersStatusUpdater
{
    public Task <CustomerRoleUpdateIdsMessage> GetAllCustomersAndUpdateRoleAsync();
}
