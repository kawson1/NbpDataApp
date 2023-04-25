namespace UnitTests;

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
}