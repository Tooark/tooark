using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using Tooark.Extensions.Factories;
using Tooark.Extensions.Injections;

namespace Tooark.Tests.Extensions.Injections;

public class TooarkJsonStringLocalizerDependencyInjectionTests
{
  // Teste para verificar se o método AddJsonStringLocalizer adiciona os serviços corretamente.
  [Fact]
  public void AddJsonStringLocalizer_ShouldRegisterServices()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddJsonStringLocalizer();
    var serviceProvider = services.BuildServiceProvider();
    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    var localizer = serviceProvider.GetService<IStringLocalizer>();

    // Assert
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);
    Assert.NotNull(localizer);
  }

  // Teste para verificar se o método AddJsonStringLocalizer adiciona os serviços corretamente com o uso de um mock.
  [Fact]
  public void AddJsonStringLocalizer_ShouldRegisterServices_WithMock()
  {
    // Arrange
    var services = new ServiceCollection();
    var mockDistributedCache = new Mock<IDistributedCache>();
    services.AddSingleton(mockDistributedCache.Object);

    // Act
    services.AddJsonStringLocalizer();
    var serviceProvider = services.BuildServiceProvider();
    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    var localizer = serviceProvider.GetService<IStringLocalizer>();

    // Assert
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);
    Assert.NotNull(localizer);
  }
}
