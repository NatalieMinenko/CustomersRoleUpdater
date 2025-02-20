using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Models;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace CustomersRoleUpdater.Application.Tests;

public class CustomersDataServiceTest
{
    private ICustomersDataService _sut;
    private Mock<HttpMessageHandler> _messageHandlerMock;
    private string _baseAddress = "https://github.com/";

    public CustomersDataServiceTest()
    {
        _messageHandlerMock = new Mock<HttpMessageHandler>();
        _sut = new CustomersDataService(_messageHandlerMock.Object);
    }

    [Fact]
    public async Task GetCustomersForUpdateByCountTransactionAsync_CallMethod_GetCustomersSuccess()
    {
        // arrange
        var apiEndpoint = $"count";
        var mockedProtected = _messageHandlerMock.Protected();
        var obj = new List<Customer>() 
            { new Customer() { Id = Guid.NewGuid(), Role = Role.Regular} };

        var response = JsonSerializer.Serialize(obj); 

        var setupApiRequest = mockedProtected.Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.Is<HttpRequestMessage>(m => m.RequestUri!.Equals(_baseAddress + apiEndpoint)),
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(response)
        });
        // act
        var result = await _sut.GetCustomersForUpdateByCountTransactionAsync();
        // assert
        Assert.Equivalent(result, obj);
    }
}
