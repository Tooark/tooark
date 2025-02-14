using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using Tooark.Extensions.Factories;
using Tooark.Extensions.Injections;

namespace Tooark.Tests.Extensions.Injections;

public class TooarkDependencyInjectionTests
{

  [Fact]
  public void AddJsonStringLocalizer_ShouldRegisterServices()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddJsonStringLocalizer();
    var serviceProvider = services.BuildServiceProvider();

    // Assert
    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);

    var localizer = serviceProvider.GetService<IStringLocalizer>();
    Assert.NotNull(localizer);
  }

  [Fact]
  public void AddJsonStringLocalizer_ShouldUseDefaultOptions_WhenOptionsNotProvided()
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

  [Fact]
  public void AddJsonStringLocalizer_ShouldUseProvidedOptions()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddJsonStringLocalizer();
    var serviceProvider = services.BuildServiceProvider();

    // Assert
    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);

    var localizer = serviceProvider.GetService<IStringLocalizer>();
    Assert.NotNull(localizer);
  }

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

    // Assert
    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);

    var localizer = serviceProvider.GetService<IStringLocalizer>();
    Assert.NotNull(localizer);
  }
}
