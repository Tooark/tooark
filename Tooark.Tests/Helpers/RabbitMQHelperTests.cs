using Moq;
using RabbitMQ.Client;
using Tooark.Exceptions;
using Tooark.Helpers;

namespace Tooark.Tests.Helpers;

public class RabbitMQHelperTests
{
  // Configurar Fanout Direct deve ser configurados corretamente
  [Fact]
  public void ConfigureFanoutDirect_ValidParameters_ShouldConfigureCorrectly()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act
    RabbitMQHelper.ConfigureFanoutDirect(mockChannel.Object, queueName, routingKey);

    // Assert
    mockChannel.Verify(channel => channel.ExchangeDeclare("exchange_fanout", ExchangeType.Fanout, true, false, null), Times.Once());
    mockChannel.Verify(channel => channel.ExchangeDeclare("exchange_direct", ExchangeType.Direct, true, false, null), Times.Once());
    mockChannel.Verify(channel => channel.QueueDeclare(queueName, true, false, false, null), Times.Exactly(2));
    mockChannel.Verify(channel => channel.QueueBind(queueName, "exchange_fanout", "", null), Times.Once());
    mockChannel.Verify(channel => channel.QueueBind(queueName, "exchange_direct", routingKey, null), Times.Once());
  }

  // Configurar exceção de Fanout Direct Throws quando o Channel é nulo
  [Fact]
  public void ConfigureFanoutDirect_ThrowsException_WhenChannelIsNull()
  {
    // Arrange
    IModel channel = null!;
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<RabbitMQServiceException>(() => RabbitMQHelper.ConfigureFanoutDirect(channel, queueName, routingKey));
    Assert.EndsWith("Variável channel é nula.", exception.Message);
  }

  // Configurar exceção de Fanout Direct Throws quando o Exchange Name é nulo
  [Fact]
  public void ConfigureFanoutDirect_ThrowsException_WhenQueueNameIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string queueName = null!;
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<RabbitMQServiceException>(() => RabbitMQHelper.ConfigureFanoutDirect(mockChannel.Object, queueName, routingKey));
    Assert.EndsWith("Variável queueName é nula.", exception.Message);
  }

  // Configurar parâmetros válidos da fila do Exchange do tipo Fanout devem ser configurados corretamente
  [Fact]
  public void ConfigureExchangeQueue_ValidParametersFanout_ShouldConfigureCorrectly()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "exchange_fanout";
    string exchangeType = ExchangeType.Fanout;
    string queueName = "test-queue";
    string routingKey = null!;

    // Act
    RabbitMQHelper.ConfigureExchangeQueue(mockChannel.Object, exchangeName, exchangeType, queueName, routingKey);

    // Assert
    mockChannel.Verify(channel => channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null), Times.Once());
    mockChannel.Verify(channel => channel.QueueDeclare(queueName, true, false, false, null), Times.Once());
    mockChannel.Verify(channel => channel.QueueBind(queueName, exchangeName, routingKey, null), Times.Once());
  }

  // Configurar parâmetros válidos da fila do Exchange do tipo Direct devem ser configurados corretamente
  [Fact]
  public void ConfigureExchangeQueue_ValidParametersDirect_ShouldConfigureCorrectly()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "exchange_direct";
    string exchangeType = ExchangeType.Direct;
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act
    RabbitMQHelper.ConfigureExchangeQueue(mockChannel.Object, exchangeName, exchangeType, queueName, routingKey);

    // Assert
    mockChannel.Verify(channel => channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null), Times.Once());
    mockChannel.Verify(channel => channel.QueueDeclare(queueName, true, false, false, null), Times.Once());
    mockChannel.Verify(channel => channel.QueueBind(queueName, exchangeName, routingKey, null), Times.Once());
  }

  // Configurar exceção de Exchange Queue Throws quando o Channel é nulo
  [Fact]
  public void ConfigureExchangeQueue_ThrowsException_WhenChannelIsNull()
  {
    // Arrange
    IModel channel = null!;
    string exchangeName = "exchange_direct";
    string exchangeType = ExchangeType.Direct;
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureExchangeQueue(channel, exchangeName, exchangeType, queueName, routingKey));
    Assert.Equal("channel", exception.ParamName);
  }

  // Configurar exceção de Exchange Queue Throws quando o Exchange Name é nulo
  [Fact]
  public void ConfigureExchangeQueue_ThrowsException_WhenExchangeNameIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = null!;
    string exchangeType = ExchangeType.Direct;
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureExchangeQueue(mockChannel.Object, exchangeName, exchangeType, queueName, routingKey));
    Assert.Equal("exchangeName", exception.ParamName);
  }

  // Configurar exceção de Exchange Queue Throws quando o Exchange Type é nulo
  [Fact]
  public void ConfigureExchangeQueue_ThrowsException_WhenExchangeTypeIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "exchange_direct";
    string exchangeType = null!;
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureExchangeQueue(mockChannel.Object, exchangeName, exchangeType, queueName, routingKey));
    Assert.Equal("exchangeType", exception.ParamName);
  }

  // Configurar exceção de Exchange Queue Throws quando o Queue Name é nulo
  [Fact]
  public void ConfigureExchangeQueue_ThrowsException_WhenQueueNameIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "exchange_direct";
    string exchangeType = ExchangeType.Direct;
    string queueName = null!;
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureExchangeQueue(mockChannel.Object, exchangeName, exchangeType, queueName, routingKey));
    Assert.Equal("queueName", exception.ParamName);
  }

  // Configurar exceção de Exchange Queue Throws quando o Routing Key é nulo
  [Fact]
  public void ConfigureExchangeQueue_ThrowsException_WhenRoutingKeyIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "exchange_direct";
    string exchangeType = ExchangeType.Direct;
    string queueName = "test-queue";
    string routingKey = null!;

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureExchangeQueue(mockChannel.Object, exchangeName, exchangeType, queueName, routingKey));
    Assert.Equal("routingKey", exception.ParamName);
  }

  // Configurar parâmetros válidos do Exchange devem ser configurados corretamente
  [Fact]
  public void ConfigureExchange_ValidParameters_ShouldConfigureCorrectly()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "test-exchange";
    string exchangeType = ExchangeType.Direct;

    // Act
    RabbitMQHelper.ConfigureExchange(mockChannel.Object, exchangeName, exchangeType);

    // Assert
    mockChannel.Verify(channel => channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null), Times.Once());
  }

  // Configurar exceção de Exchange Throws quando o Channel é nulo
  [Fact]
  public void ConfigureExchange_ThrowsException_WhenChannelIsNull()
  {
    // Arrange
    IModel channel = null!;
    string exchangeName = "exchange_direct";
    string exchangeType = ExchangeType.Direct;

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureExchange(channel, exchangeName, exchangeType));
    Assert.Equal("channel", exception.ParamName);
  }

  // Configurar exceção de Exchange Throws quando o Exchange Name é nulo
  [Fact]
  public void ConfigureExchange_ThrowsException_WhenExchangeNameIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = null!;
    string exchangeType = ExchangeType.Direct;

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureExchange(mockChannel.Object, exchangeName, exchangeType));
    Assert.Equal("exchangeName", exception.ParamName);
  }

  // Configurar exceção de Exchange Throws quando o Exchange Type é nulo
  [Fact]
  public void ConfigureExchange_ThrowsException_WhenExchangeTypeIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "exchange_direct";
    string exchangeType = null!;

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureExchange(mockChannel.Object, exchangeName, exchangeType));
    Assert.Equal("exchangeType", exception.ParamName);
  }

  // Configurar parâmetros válidos da Queue que devem ser configurados corretamente
  [Fact]
  public void ConfigureQueue_ValidParameters_ShouldConfigureCorrectly()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "test-exchange";
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act
    RabbitMQHelper.ConfigureQueue(mockChannel.Object, exchangeName, queueName, routingKey);

    // Assert
    mockChannel.Verify(channel => channel.QueueDeclare(queueName, true, false, false, null), Times.Once());
    mockChannel.Verify(channel => channel.QueueBind(queueName, exchangeName, routingKey, null), Times.Once());
  }

  // Configurar exceção de Queue Throws quando o Channel é nulo
  [Fact]
  public void ConfigureQueue_ThrowsException_WhenChannelIsNull()
  {
    // Arrange
    IModel channel = null!;
    string exchangeName = "exchange_direct";
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureQueue(channel, exchangeName, queueName, routingKey));
    Assert.Equal("channel", exception.ParamName);
  }

  // Configurar exceção de Queue Throws quando o Exchange Name é nulo
  [Fact]
  public void ConfigureQueue_ThrowsException_WhenExchangeNameIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = null!;
    string queueName = "test-queue";
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureQueue(mockChannel.Object, exchangeName, queueName, routingKey));
    Assert.Equal("exchangeName", exception.ParamName);
  }
  
  // Configurar exceção de Queue Throws quando o Queue Name é nulo
  [Fact]
  public void ConfigureQueue_ThrowsException_WhenQueueNameIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "exchange_direct";
    string queueName = null!;
    string routingKey = "test-routing";

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureQueue(mockChannel.Object, exchangeName, queueName, routingKey));
    Assert.Equal("queueName", exception.ParamName);
  }

  // Configurar exceção de Queue Throws quando o Routing Key é nulo
  [Fact]
  public void ConfigureQueue_ThrowsException_WhenRoutingKeyIsNull()
  {
    // Arrange
    var mockChannel = new Mock<IModel>();
    string exchangeName = "exchange_direct";
    string queueName = "test-queue";
    string routingKey = null!;

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => RabbitMQHelper.ConfigureQueue(mockChannel.Object, exchangeName, queueName, routingKey));
    Assert.Equal("routingKey", exception.ParamName);
  }
}
