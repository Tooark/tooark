using Moq;
using Tooark.Exceptions;
using Tooark.Interfaces;
using Tooark.Options;

namespace Tooark.Tests.Services.RabbitMQ;

public class RabbitMQPublishServiceTests
{
  private readonly Mock<IRabbitMQPublishServiceFactory> _mockFactory;
  private readonly Mock<IRabbitMQPublishService> _mockService;
  private readonly RabbitMQOptions _options;

  public RabbitMQPublishServiceTests()
  {
    _options = new()
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = "guest",
      Password = "guest"
    };

    _mockFactory = new Mock<IRabbitMQPublishServiceFactory>();
    _mockService = new Mock<IRabbitMQPublishService>();

    _mockFactory
      .Setup(f => f.CreateRabbitMQPublishService(_options))
      .Returns(_mockService.Object);
  }

  // Publicar mensagem com Message publicada corretamente
  [Fact]
  public void PublishMessage_WithMessage_PublishesCorrectly()
  {
    // Arrange
    var message = "Test message";
    _mockService.Setup(s => s.PublishMessage(message));

    // Act
    var service = _mockFactory.Object.CreateRabbitMQPublishService(_options);
    service.PublishMessage(message);

    // Assert
    _mockService.Verify(s => s.PublishMessage(message), Times.Once);
  }

  // Publicar mensagem com Message e RoutingKey publica corretamente
  [Fact]
  public void PublishMessage_WithMessageAndRoutingKey_PublishesCorrectly()
  {
    // Arrange
    var message = "Test message";
    var routingKey = "test-routing";
    _mockService.Setup(s => s.PublishMessage(message, routingKey));

    // Act
    var service = _mockFactory.Object.CreateRabbitMQPublishService(_options);
    service.PublishMessage(message, routingKey);

    // Assert
    _mockService.Verify(s => s.PublishMessage(message, routingKey), Times.Once);
  }

  // Publicar mensagem com Message, RoutingKey e ExchangeName publica corretamente
  [Fact]
  public void PublishMessage_WithMessageAndRoutingKeyAndExchangeName_PublishesCorrectly()
  {
    // Arrange
    var message = "Test message";
    var routingKey = "test-routing";
    var exchangeName = "test-exchange";
    _mockService.Setup(s => s.PublishMessage(message, routingKey, exchangeName));

    // Act
    var service = _mockFactory.Object.CreateRabbitMQPublishService(_options);
    service.PublishMessage(message, routingKey, exchangeName);

    // Assert
    _mockService.Verify(s => s.PublishMessage(message, routingKey, exchangeName), Times.Once);
  }

  // Publicar mensagem quando o corretor inacessível lança RabbitMQServiceException
  [Fact]
  public void PublishMessage_WhenBrokerUnreachable_ThrowsRabbitMQPublishServiceException()
  {
    // Arrange
    var message = "Test message";
    _mockService
      .Setup(s => s.PublishMessage(message))
      .Throws(new RabbitMQServiceException("Broker unreachable"));

    // Act & Assert
    var service = _mockFactory.Object.CreateRabbitMQPublishService(_options);
    var exception = Assert.Throws<RabbitMQServiceException>(() => service.PublishMessage(message));
    Assert.Equal("Broker unreachable", exception.Message);
  }
}
