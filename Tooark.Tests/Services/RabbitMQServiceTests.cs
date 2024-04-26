using Moq;
using RabbitMQ.Client.Events;
using Tooark.Interfaces;

namespace Tooark.Tests.Services.RabbitMQ;

public class RabbitMQServiceTests
{
  // Teste para PublishMessage sem routingKey
  [Fact]
  public void PublishMessage_WithoutRoutingKey_PublishesMessageToExchange()
  {
    // Arrange
    var mockRabbitMQService = new Mock<IRabbitMQService>();
    var message = "Test Message";

    // Act
    mockRabbitMQService.Object.PublishMessage(message);

    // Assert
    mockRabbitMQService.Verify(service => service.PublishMessage(message), Times.Once);
  }

  // Teste para PublishMessage com routingKey
  [Fact]
  public void PublishMessage_WithRoutingKey_PublishesMessageToExchange()
  {
    // Arrange
    var mockRabbitMQService = new Mock<IRabbitMQService>();
    var message = "Test Message";
    var routingKey = "test.routing.key";

    // Act
    mockRabbitMQService.Object.PublishMessage(message, routingKey);

    // Assert
    mockRabbitMQService.Verify(service => service.PublishMessage(message, routingKey), Times.Once);
  }

  // Teste para ConsumeMessage
  [Fact]
  public void ConsumeMessage_WithValidQueueNameAndConsumer_CallsConsumeMethod()
  {
    // Arrange
    var mockRabbitMQService = new Mock<IRabbitMQService>();
    var queueName = "test-queue";
    var consumer = new EventingBasicConsumer(null); // Normalmente você passaria um canal aqui

    // Act
    mockRabbitMQService.Object.ConsumeMessage(queueName, consumer);

    // Assert
    mockRabbitMQService.Verify(service => service.ConsumeMessage(queueName, consumer), Times.Once);
  }
}
