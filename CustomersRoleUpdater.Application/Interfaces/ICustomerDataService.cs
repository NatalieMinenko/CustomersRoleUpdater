
using CustomersRoleUpdater.Application.Models;

namespace CustomersRoleUpdater.Application.Interfaces;

public interface ICustomerDataService
{
    public Task<List<Customer>> GetCustomersForUpdateByBirhtdayAsync();
    public Task<List<Customer>> GetCustomersForUpdateByCountTransactionAsync();
    public Task<List<Customer>> GetCustomersForUpdateBySumTransactionAsync();
}
