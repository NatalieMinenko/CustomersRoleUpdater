using CustomersRoleUpdater.Application.Interfaces;
using CustomersRoleUpdater.Application.Models;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Text.Json;

namespace CustomersRoleUpdater.Application.Tests;

public class CustomerDataServiceTest
{
    private ICustomerDataService _sut;
    private Mock<HttpMessageHandler> _messageHandlerMock;
    private string _baseAddress = "https://github.com/";

    public CustomerDataServiceTest()
    {
        _messageHandlerMock = new Mock<HttpMessageHandler>();
        _sut = new CustomersDataService(_messageHandlerMock.Object);
    }

    [Fact]
    public async Task GetCustomersForUpdateByCountTransactionAsync_GetSuccessAndCorrectType()
    {
        // arrange
        var apiEndpoint = "count";

        var response = new List<Customer>() {
            new Customer{Id=Guid.NewGuid(), Role = Role.Regular }};
        var json = JsonSerializer.Serialize(response);

        var setupApiRequest = _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.Is<HttpRequestMessage>(m => m.RequestUri!.Equals(_baseAddress + apiEndpoint)),
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(json),
        });
        // act
        var result = await _sut.GetCustomersForUpdateByCountTransactionAsync();
        // assert
        Assert.NotEmpty(result);
        Assert.Equivalent(result, response);
    }
}
