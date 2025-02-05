using CustomersRoleUpdater.Application.Interfaces;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Moq.Protected;
using System;
using System.Net;

namespace CustomersRoleUpdater.Application.Tests;

public class CustomerDataServiceTest
{
    private ICustomerDataService _sut;
    private Mock<HttpMessageHandler> _messageHandlerMock;
    private string _baseAddress = "";

    public CustomerDataServiceTest()
    {
        _messageHandlerMock = new Mock<HttpMessageHandler>();
        _sut = new CustomersDataService(_messageHandlerMock.Object);
    }

    [Fact]
    public void Test1()
    {
        // arrange
        var apiEndpoint = $"/count/";
        var mockedProtected = _messageHandlerMock.Protected();
        var response = "";
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
        var result = _sut.GetCustomersForUpdateByCountTransactionAsync();
        // assert
        //Assert.NotEmpty(result);
    }
}
