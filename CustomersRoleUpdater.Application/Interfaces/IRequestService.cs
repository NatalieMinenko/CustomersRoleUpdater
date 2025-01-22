
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface IRequestService
{
    public Task<List<CustomersWithDateOfBirthday>> GetCustomersFromJsonPlaceholderAsync();
}
