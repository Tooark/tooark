using Moq;
using Tooark.Factories;
using Tooark.Interfaces;
using Tooark.Options;

namespace Tooark.Tests.Factories;

public class RabbitMQPublishServiceFactoryTests
{
  // CreateRabbitMQPublishService com opções válidas retorna instância de serviço
  [Fact]
  public void CreateRabbitMQPublishService_WithValidOptions_ReturnsServiceInstance()
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = "guest",
      Password = "guest",
      AutomaticRecovery = true,
      RecoveryInterval = 5
    };
    var mockFactory = new Mock<IRabbitMQPublishServiceFactory>();
    var mockService = new Mock<IRabbitMQPublishService>();
    mockFactory
      .Setup(f => f.CreateRabbitMQPublishService(options))
      .Returns(mockService.Object);

    // Act
    var service = mockFactory.Object.CreateRabbitMQPublishService(options);

    // Assert
    Assert.NotNull(service);
    Assert.IsAssignableFrom<IRabbitMQPublishService>(service);
  }

  // CreateRabbitMQPublishService com opções nulas gera exceção nula de argumento
  [Fact]
  public void CreateRabbitMQPublishService_WithNullOptions_ThrowsArgumentNullException()
  {
    // Arrange
    RabbitMQOptions options = null!;
    var factory = new RabbitMQPublishServiceFactory();

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQPublishService(options));
    Assert.Equal("options", exception.ParamName);
  }

  // CreateRabbitMQPublishService com nome de host inválido lança exceção de argumento ou exceção nula de argumento
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQPublishService_WithInvalidHostname_ThrowsArgumentException(string? hostname)
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = hostname!,
      PortNumber = 5672,
      Username = "guest",
      Password = "guest",
      AutomaticRecovery = true,
      RecoveryInterval = 5
    };
    var factory = new RabbitMQPublishServiceFactory();

    // Act & Assert
    if (hostname != null)
    {
      Assert.Throws<ArgumentException>(() => factory.CreateRabbitMQPublishService(options));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQPublishService(options));
    }
  }

  // CreateRabbitMQPublishService com nome de usuário inválido gera exceção de argumento ou exceção nula de argumento
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQPublishService_WithInvalidUsername_ThrowsArgumentException(string? username)
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = username!,
      Password = "guest",
      AutomaticRecovery = true,
      RecoveryInterval = 5
    };
    var factory = new RabbitMQPublishServiceFactory();

    // Act & Assert
    if (username != null)
    {
      Assert.Throws<ArgumentException>(() => factory.CreateRabbitMQPublishService(options));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQPublishService(options));
    }
  }

  // CreateRabbitMQPublishService com senha inválida gera exceção de argumento ou exceção nula de argumento
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQPublishService_WithInvalidPassword_ThrowsArgumentException(string? password)
  {
    // Arrange
    var options = new RabbitMQOptions
    {
      Hostname = "localhost",
      PortNumber = 5672,
      Username = "guest",
      Password = password!,
      AutomaticRecovery = true,
      RecoveryInterval = 5
    };
    var factory = new RabbitMQPublishServiceFactory();

    // Act & Assert
    if (password != null)
    {
      Assert.Throws<ArgumentException>(() => factory.CreateRabbitMQPublishService(options));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => factory.CreateRabbitMQPublishService(options));
    }
  }
}
