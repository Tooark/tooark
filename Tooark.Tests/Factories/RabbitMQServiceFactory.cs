using Tooark.Factories;
using Tooark.Options;
using Tooark.Interfaces;
using Moq;

namespace Tooark.Tests.Factories;

public class RabbitMQServiceFactoryTests
{
  [Fact]
  public void CreateRabbitMQService_WithValidOptions_ReturnsServiceInstance()
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
    var mockFactory = new Mock<IRabbitMQServiceFactory>();
    var mockService = new Mock<IRabbitMQService>();
    mockFactory
      .Setup(f => f.CreateRabbitMQService(options))
      .Returns(mockService.Object);

    // Act
    var service = mockFactory.Object.CreateRabbitMQService(options);

    // Assert
    Assert.NotNull(service);
    Assert.IsAssignableFrom<IRabbitMQService>(service);
  }

  [Fact]
  public void CreateRabbitMQService_WithNullOptions_ThrowsArgumentNullException()
  {
    // Arrange
    RabbitMQOptions options = null!;
    var mockFactory = new Mock<IRabbitMQServiceFactory>();
    var mockService = new Mock<IRabbitMQService>();
    mockFactory
      .Setup(f => f.CreateRabbitMQService(options))
      .Returns(mockService.Object);

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => mockFactory.Object.CreateRabbitMQService(options));
    Assert.Equal("options", exception.ParamName);
  }

  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQService_WithInvalidHostname_ThrowsArgumentException(string? hostname)
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
    var mockFactory = new Mock<IRabbitMQServiceFactory>();
    var mockService = new Mock<IRabbitMQService>();
    mockFactory
      .Setup(f => f.CreateRabbitMQService(options))
      .Returns(mockService.Object);

    // Act & Assert
    if (hostname != null)
    {
      Assert.Throws<ArgumentException>(() => mockFactory.Object.CreateRabbitMQService(options));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => mockFactory.Object.CreateRabbitMQService(options));
    }
  }

  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQService_WithInvalidUsername_ThrowsArgumentException(string? username)
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
    var mockFactory = new Mock<IRabbitMQServiceFactory>();
    var mockService = new Mock<IRabbitMQService>();
    mockFactory
      .Setup(f => f.CreateRabbitMQService(options))
      .Returns(mockService.Object);

    // Act & Assert
    if (username != null)
    {
      Assert.Throws<ArgumentException>(() => mockFactory.Object.CreateRabbitMQService(options));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => mockFactory.Object.CreateRabbitMQService(options));
    }
  }

  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void CreateRabbitMQService_WithInvalidPassword_ThrowsArgumentException(string? password)
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
    var mockFactory = new Mock<IRabbitMQServiceFactory>();
    var mockService = new Mock<IRabbitMQService>();
    mockFactory
      .Setup(f => f.CreateRabbitMQService(options))
      .Returns(mockService.Object);

    // Act & Assert
    if (password != null)
    {
      Assert.Throws<ArgumentException>(() => mockFactory.Object.CreateRabbitMQService(options));
    }
    else
    {
      Assert.Throws<ArgumentNullException>(() => mockFactory.Object.CreateRabbitMQService(options));
    }
  }

  // Additional tests for other parameters like Username, Password, and RecoveryInterval can be added similarly.
}
