using Moq;
using Tooark.Options;
using Tooark.Services.RabbitMQ;
using Tooark.Interfaces;
using Tooark.Factories;
using Castle.Components.DictionaryAdapter.Xml;

namespace Tooark.Tests.Factories;

public class RabbitMQConsumeServiceFactoryTests
{
  private readonly Action<string> _validProcessMessageFunc;

  public RabbitMQConsumeServiceFactoryTests()
  {
    _validProcessMessageFunc = message => { /* process message */ };
  }

  // CreateRabbitMQConsumeService com opções válidas retorna instância de serviço
  [Fact]
  public void CreateRabbitMQConsumeService_WithValidOptions_ReturnsServiceInstance()
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = "guest",
      Password = "guest",
      AutomaticRecovery = true,
      RecoveryInterval = 5,
      QueueName = "test_queue"
    };
    var mockFactory = new Mock<IRabbitMQConsumeServiceFactory>();
    var mockService = new Mock<IRabbitMQConsumeService>();
    mockFactory
      .Setup(f => f.CreateRabbitMQConsumeService(options, _validProcessMessageFunc))
      .Returns(mockService.Object);

    // Act
    var service = mockFactory.Object.CreateRabbitMQConsumeService(options, _validProcessMessageFunc);

    // Assert
    Assert.NotNull(service);
    Assert.IsAssignableFrom<IRabbitMQConsumeService>(service);
  }

  // CreateRabbitMQConsumeService com opções nulas gera exceção nula de argumento
  [Fact]
  public void CreateRabbitMQConsumeService_WithNullOptions_ThrowsArgumentNullException()
  {
    // Arrange
    RabbitMQOptions options = null!;
    var factory = new RabbitMQConsumeServiceFactory();

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    Assert.Equal("options", exception.ParamName);
  }

  // CreateRabbitMQConsumeService com processMessageFunc nula gera exceção nula de argumento
  [Fact]
  public void CreateRabbitMQConsumeService_WithNullProcessMessageFunc_ThrowsArgumentNullException()
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = "guest",
      Password = "guest",
      AutomaticRecovery = true,
      RecoveryInterval = 5,
      QueueName = "test_queue"
    };
    var factory = new RabbitMQConsumeServiceFactory();

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQConsumeService(options, null!));
    Assert.Equal("processMessageFunc", exception.ParamName);
  }

  // CreateRabbitMQConsumeService com nome de host inválido lança exceção de argumento ou exceção nula de argumento
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQConsumeService_WithInvalidHostname_ThrowsArgumentException(string? hostname)
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = hostname!,
      PortNumber = 5672,
      Username = "guest",
      Password = "guest",
      AutomaticRecovery = true,
      RecoveryInterval = 5,
      QueueName = "test_queue"
    };
    var factory = new RabbitMQConsumeServiceFactory();

    // Act & Assert
    if (hostname != null)
    {
      Assert.Throws<ArgumentException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    }
  }

  // CreateRabbitMQConsumeService com nome de usuário inválido gera exceção de argumento ou exceção nula de argumento
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQConsumeService_WithInvalidUsername_ThrowsArgumentException(string? username)
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = username!,
      Password = "guest",
      AutomaticRecovery = true,
      RecoveryInterval = 5,
      QueueName = "test_queue"
    };
    var factory = new RabbitMQConsumeServiceFactory();

    // Act & Assert
    if (username != null)
    {
      Assert.Throws<ArgumentException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    }
  }

  // CreateRabbitMQConsumeService com senha inválida gera exceção de argumento ou exceção nula de argumento
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQConsumeService_WithInvalidPassword_ThrowsArgumentException(string? password)
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = "guest",
      Password = password!,
      AutomaticRecovery = true,
      RecoveryInterval = 5,
      QueueName = "test_queue"
    };
    var factory = new RabbitMQConsumeServiceFactory();

    // Act & Assert
    if (password != null)
    {
      Assert.Throws<ArgumentException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    }
  }

  // CreateRabbitMQConsumeService com queueName inválida gera exceção de argumento ou exceção nula de argumento
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQConsumeService_WithInvalidQueueName_ThrowsArgumentException(string? queueName)
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = "guest",
      Password = "guest",
      AutomaticRecovery = true,
      RecoveryInterval = 5,
      QueueName = queueName!
    };
    var factory = new RabbitMQConsumeServiceFactory();

    // Act & Assert
    if (queueName != null)
    {
      Assert.Throws<ArgumentException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQConsumeService(options, _validProcessMessageFunc));
    }
  }
}
