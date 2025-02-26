using CustomersRoleUpdater.Application.Integrations;
using CustomersRoleUpdater.Application.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace CustomersRoleUpdater.Application.Tests;

public class CommonHttpClientTest
{
    private Mock<HttpMessageHandler> _messageHandlerMock;
    private readonly Mock<ILogger<CommonHttpClient>> _commonHttpClientLoggerMock;
    private CommonHttpClient _sut;
    private string _baseAddress = "https://localhost:7083/api/customers/";

    public CommonHttpClientTest()
    {
        _messageHandlerMock = new();
        _commonHttpClientLoggerMock = new();
        _sut = new(_commonHttpClientLoggerMock.Object, _baseAddress, _messageHandlerMock.Object);
    }

    [Fact]
    public async Task GetRequest_CallMethod_GetCustomersSuccess()
    {
        // arrange
        var listObj = new List<Customer>()
            { new Customer() {}};
        var response = JsonSerializer.Serialize(listObj);

        var mockedProtected = _messageHandlerMock.Protected();
        var setupApiRequest = mockedProtected.Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(response)
        });
        // act
        var result = await _sut.GetRequest<List<Customer>>("");
        // assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetRequest_CallMethod_GetDoesNotIndicateSuccess()
    {
        // arrange
        var listObj = new List<Customer>()
            { new Customer() {}};
        var response = JsonSerializer.Serialize(listObj);

        var mockedProtected = _messageHandlerMock.Protected();
        var setupApiRequest = mockedProtected.Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent(response)
        });
        // act 
        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            async () => await _sut.GetRequest<List<Customer>>(""));
        // assert
        Assert.IsType<HttpRequestException>(exception);
    }
}

