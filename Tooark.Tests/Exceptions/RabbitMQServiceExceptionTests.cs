using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class RabbitMQServiceExceptionTests
{
  // Construtor com exceção interna define mensagem e exceção interna
  [Fact]
  public void Constructor_WithInnerException_SetsMessageAndInnerException()
  {
    // Arrange
    var message = "Test Message";
    var innerException = new InvalidOperationException();

    // Act
    var exception = new RabbitMQServiceException(message, innerException);

    // Assert
    Assert.Equal(message, exception.Message);
    Assert.Equal(innerException, exception.InnerException);
  }

  // Construtor com nome do Exchange define a propriedade do nome do Exchange
  [Fact]
  public void Constructor_WithExchangeName_SetsExchangeNameProperty()
  {
    // Arrange
    var message = "Test Message";
    var exchangeName = "TestExchange";
    var innerException = new InvalidOperationException();

    // Act
    var exception = new RabbitMQServiceException(message, exchangeName, innerException);

    // Assert
    Assert.Contains(exchangeName, exception.Message);
  }

  // Construtor com nome de fila define propriedade de nome de fila
  [Fact]
  public void Constructor_WithQueueName_SetsQueueNameProperty()
  {
    // Arrange
    var message = "Test Message";
    var exchangeName = "TestExchange";
    var queueName = "TestQueue";
    var innerException = new InvalidOperationException();

    // Act
    var exception = new RabbitMQServiceException(message, exchangeName, queueName, innerException);

    // Assert
    Assert.Contains(queueName, exception.Message);
  }
}
