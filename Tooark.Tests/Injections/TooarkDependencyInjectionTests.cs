using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using Tooark.Dtos;
using Tooark.Extensions.Factories;
using Tooark.Injections;

namespace Tooark.Tests.Injections;

public class TooarkDependencyInjectionTests
{
  [Fact]
  public void AddTooarkService_ShouldRegisterServices()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkService();
    var serviceProvider = services.BuildServiceProvider();

    // Assert
    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);

    var localizer = serviceProvider.GetService<IStringLocalizer>();
    Assert.NotNull(localizer);

    var dtoLocalizer = serviceProvider.GetService<IStringLocalizer<Dto>>();
    Assert.NotNull(dtoLocalizer);
  }

  [Fact]
  public void AddTooarkService_ShouldUseDefaultOptions_WhenOptionsNotProvided()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkService();
    var serviceProvider = services.BuildServiceProvider();

    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    var localizer = serviceProvider.GetService<IStringLocalizer>();
    var dtoLocalizer = serviceProvider.GetService<IStringLocalizer<Dto>>();

    // Assert
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);
    Assert.NotNull(localizer);
    Assert.NotNull(dtoLocalizer);
  }

  [Fact]
  public void AddTooarkService_ShouldUseProvidedOptions()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkService();
    var serviceProvider = services.BuildServiceProvider();

    // Assert
    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);

    var localizer = serviceProvider.GetService<IStringLocalizer>();
    Assert.NotNull(localizer);

    var dtoLocalizer = serviceProvider.GetService<IStringLocalizer<Dto>>();
    Assert.NotNull(dtoLocalizer);
  }

  [Fact]
  public void AddTooarkService_ShouldRegisterServices_WithMock()
  {
    // Arrange
    var services = new ServiceCollection();
    var mockDistributedCache = new Mock<IDistributedCache>();
    services.AddSingleton(mockDistributedCache.Object);

    // Act
    services.AddTooarkService();
    var serviceProvider = services.BuildServiceProvider();

    // Assert
    var localizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
    Assert.NotNull(localizerFactory);
    Assert.IsType<JsonStringLocalizerFactory>(localizerFactory);

    var localizer = serviceProvider.GetService<IStringLocalizer>();
    Assert.NotNull(localizer);

    var dtoLocalizer = serviceProvider.GetService<IStringLocalizer<Dto>>();
    Assert.NotNull(dtoLocalizer);
  }
}
