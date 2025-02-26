using CustomersRoleUpdater.Application.Integrations;
using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace CustomersRoleUpdater.Application.Tests;

public class CustomersDataServiceTest
{
    private ICustomersDataService _sut;
    private Mock<HttpMessageHandler> _messageHandlerMock;
    private readonly Mock<ILogger<CommonHttpClient>> _commonHttpClientLoggerMock;
    private readonly Mock<ILogger<CustomersDataService>> _customersDataServiceLoggerMock;
    private string _baseAddress = "https://localhost:7083/api/customers/";

    public CustomersDataServiceTest()
    {
        _messageHandlerMock = new();
        _commonHttpClientLoggerMock = new();
        _customersDataServiceLoggerMock = new();

        _sut = new CustomersDataService(
            _commonHttpClientLoggerMock.Object,
            _customersDataServiceLoggerMock.Object,
            _messageHandlerMock.Object);
    }

    Guid guid = Guid.NewGuid();

    [Fact]
    public async Task TaskGetCustomersForUpdateByBirhtdayAsync_CallMethod_GetCustomersSuccess() 
    {
        // arrange
        var dateStart = DateTime.Now.AddDays(-14);
        var dateEnd = DateTime.Now; 
        var apiEndpoint = $"birth-date?DateStart={dateStart}&DateEnd={dateEnd}";
        var listObj = new List<Customer>()
            { new Customer() {Id = guid}};
        var response = JsonSerializer.Serialize(listObj);

        var mockedProtected = _messageHandlerMock.Protected();
        var setupApiRequest = mockedProtected.Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),      //(m => m.RequestUri!.Equals(_baseAddress + apiEndpoint)),
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(response)
        });
        // act
        var result = await _sut.GetCustomersForUpdateByBirhtdayAsync ();
        // assert
        Assert.IsType<List<Guid>>(result);
    }

    [Fact]
    public async Task GetCustomersForUpdateByCountTransactionAsync_CallMethod_GetCustomersSuccess()
    {
        // arrange
        var apiEndpoint = $"count"; 
        var listObj = new List<Customer>()
            { new Customer() {Id = guid}};
        var response = JsonSerializer.Serialize(listObj);

        var mockedProtected = _messageHandlerMock.Protected();
        var setupApiRequest = mockedProtected.Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(), //(m => m.RequestUri!.Equals(_baseAddress + apiEndpoint)),
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(response)
        });
        // act
        var result = await _sut.GetCustomersForUpdateByCountTransactionAsync();
        // assert
        Assert.IsType<List<Guid>>(result);
    }
}
