
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomersDataService
{
    public Task<List<Guid>> GetCustomersForUpdateByBirhtdayAsync();
    public Task<List<Guid>> GetCustomersForUpdateByCountTransactionAsync();
    public Task<List<Guid>> GetCustomersForUpdateBySumTransactionAsync();
}
