using Microsoft.AspNetCore.Mvc;
using Moq;
using NbpDataWebApp.Controllers;
using NbpDataWebApp.Models;
using NbpDataWebApp.Services;

public class NbpDataControllerTests
{
    private readonly NbpDataController _controller;

    public NbpDataControllerTests()
    {
        var mockService = new Mock<INbpDataService>();
        _controller = new NbpDataController(mockService.Object);
    }

    [Fact]
    public async Task Exchange_ReturnsViewResult()
    {
        // Arrange
        var currencyCode = "EUR";
        var exchangeDate = "2023-01-01";

        // Act
        var result = await _controller.Exchange(currencyCode, exchangeDate);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);
    }

    [Fact]
    public async Task MinMaxExchange_ReturnsViewResult()
    {
        // Arrange
        var currencyCode = "EUR";
        var count = 10;

        // Act
        var result = await _controller.MinMaxExchange(currencyCode, count);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);
    }

    [Fact]
    public async Task BiggestBuySellDifference_ReturnsViewResult()
    {
        // Arrange
        var currencyCode = "EUR";
        var count = 10;

        // Act
        var result = await _controller.BiggestBuySellDifference(currencyCode, count);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);
    }
    
    /*[Fact]
    public void Exchange_ReturnsViewResult2()
    {
        // Arrange
        var mockNbpDataService = new Mock<INbpDataService>();
        mockNbpDataService.Setup(s => s.GetSingleExchange("USD", "2022-01-03"))
            .ReturnsAsync(new ExchangeData
            {
                effectiveDate = new DateTime(2022, 01, 03),
                currencyCode = "USD",
                exchangeRate = 4.0424f
            });
        var controller = new NbpDataController(mockNbpDataService.Object);
        var expectedValue = 
            new ExchangeData
        {
            effectiveDate = new DateTime(2022, 01, 03),
            currencyCode = "USD",
            exchangeRate = 4.0424f
        };

        // Act
        var result = controller.Exchange("USD", "2022-01-01").Result as ViewResult;
        var actualValue = result.Model as string;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }*/
}