
using Contract;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Models;
using Moq;

namespace CustomersRoleUpdater.Application.Tests;

public class CustomersStatusUpdaterTest
{
    private CustomersStatusUpdater _sut;
    private Mock<ICustomersDataService> _customersDataService;

    public CustomersStatusUpdaterTest()
    {
        _customersDataService = new Mock<ICustomersDataService>();
        _sut = new CustomersStatusUpdater(_customersDataService.Object);
    }

    [Fact]

    public async Task GetAllCustomersAndUpdateRoleAsync_Call_GetListIdsSuccess()
    {
        // arrange
        var customers = new List<Customer>
            { new Customer { Id = Guid.NewGuid(), Role = Role.Regular } };
        var customersTask = Task.Run(() => null);
        _customersDataService.Setup(t => t.GetCustomersForUpdateBySumTransactionAsync()).
            ReturnsAsync(customers);
        _customersDataService.Setup(t => t.GetCustomersForUpdateByCountTransactionAsync()).
            ReturnsAsync(customers);
        _customersDataService.Setup(t => t.GetCustomersForUpdateByBirhtdayAsync()).
            ReturnsAsync(customers);

        //var massListCustomer = await Task.WhenAll(customersTask, customersTask, customersTask);
        //var d = _sut.GetCustomersWithoutNull(new List<Customer>[] {new List<Customer>{ null} });
        //ListCustomerId customerIds = new();
        //customerIds.CustomerIds = _sut.UpdateCustomerRoles(customers);
        // act
        var listId = await _sut.GetAllCustomersAndUpdateRoleAsync();
        var u = listId;
        //assert
        Assert.IsType<ListCustomerId>(listId);
    }
}
