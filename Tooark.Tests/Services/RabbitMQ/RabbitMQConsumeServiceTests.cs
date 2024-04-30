using Moq;
using Tooark.Options;
using Tooark.Interfaces;

namespace Tooark.Tests.Services.RabbitMQ;

public class RabbitMQConsumeServiceTests
{
  private readonly Mock<IRabbitMQConsumeServiceFactory> _mockFactory;
  private readonly RabbitMQOptions _options;
  private readonly Action<string> _processMessageFunc;
  private bool _messageProcessed = false;

  public RabbitMQConsumeServiceTests()
  {
    _options = new()
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = "guest",
      Password = "guest"
    };

    _mockFactory = new Mock<IRabbitMQConsumeServiceFactory>();

    _processMessageFunc = (_) => { _messageProcessed = true; };

    _mockFactory
      .Setup(f => f.CreateRabbitMQConsumeService(It.IsAny<RabbitMQOptions>(), It.IsAny<Action<string>>()))
      .Throws(new Exception("Simulated exception"));
  }

  // ConsumeService deve consumir mensagens corretamente
  [Fact]
  public void ConsumeService_Should_ConsumeMessagesCorrectly()
  {
    // Arrange
    _messageProcessed = false;
    _mockFactory
      .Setup(f => f.CreateRabbitMQConsumeService(It.IsAny<RabbitMQOptions>(), It.IsAny<Action<string>>()))
      .Callback<RabbitMQOptions, Action<string>>((options, processFunc) => processFunc("test message"));

    // Act
    _mockFactory.Object.CreateRabbitMQConsumeService(_options, _processMessageFunc);

    // Assert
    Assert.True(_messageProcessed);
  }

  // ConsumeService deve lançar exceção quando a criação falha
  [Fact]
  public void ConsumeService_Should_ThrowException_WhenCreationFails()
  {
    // Arrange
    _mockFactory
      .Setup(f => f.CreateRabbitMQConsumeService(It.IsAny<RabbitMQOptions>(), It.IsAny<Action<string>>()))
      .Throws(new Exception("Simulated exception"));

    // Act & Assert
    var exception = Assert.Throws<Exception>(() => _mockFactory.Object.CreateRabbitMQConsumeService(_options, _processMessageFunc));
    Assert.Equal("Simulated exception", exception.Message);
  }
}
