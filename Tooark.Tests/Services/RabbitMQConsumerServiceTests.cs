using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client.Events;
using Tooark.Interfaces;
using Tooark.Services.RabbitMQ;

namespace Tooark.Tests.Services.RabbitMQ;

public class RabbitMQConsumerServiceTests
{
  // Consumir mensagem com chamadas de nome de fila válidas método de consumo
  [Fact]
  public void ConsumeMessage_WithValidQueueName_CallsConsumeMethod()
  {
    // Arrange
    var mockRabbitMQService = new Mock<IRabbitMQService>();
    var loggerMock = new Mock<ILogger<RabbitMQConsumerService>>();
    var processMessageFunc = new Action<string>(message => { /* Process message */ });
    var rabbitMQConsumerService = new RabbitMQConsumerService(loggerMock.Object, mockRabbitMQService.Object, "test-queue", processMessageFunc);

    // Act
    rabbitMQConsumerService.StartAsync(new CancellationToken());

    // Assert
    mockRabbitMQService.Verify(service => service.ConsumeMessage("test-queue", It.IsAny<EventingBasicConsumer>()), Times.Once);
  }
}
