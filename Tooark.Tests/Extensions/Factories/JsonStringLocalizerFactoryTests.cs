using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Tooark.Extensions;
using Tooark.Extensions.Factories;

namespace Tooark.Tests.Extensions.Factories;

public class JsonStringLocalizerFactoryTests
{
  // Teste para verificar se o método Create retorna um JsonStringLocalizerExtension
  [Fact]
  public void Create_WithTypeResourceSource_ReturnsJsonStringLocalizer()
  {
    // Arrange
    Mock<IDistributedCache> distributedCacheMock = new();
    JsonStringLocalizerFactory factory = new(distributedCacheMock.Object);
    var resourceSource = typeof(JsonStringLocalizerFactoryTests);

    // Act
    var localizer = factory.Create(resourceSource);

    // Assert
    Assert.NotNull(localizer);
    Assert.IsType<JsonStringLocalizerExtension>(localizer);
  }

  // Teste para verificar se o método Create retorna um JsonStringLocalizerExtension
  [Fact]
  public void Create_WithBaseNameAndLocation_ReturnsJsonStringLocalizer()
  {
    // Arrange
    Mock<IDistributedCache> distributedCacheMock = new();
    JsonStringLocalizerFactory factory = new(distributedCacheMock.Object);
    var baseName = "BaseName";
    var location = "Location";

    // Act
    var localizer = factory.Create(baseName, location);

    // Assert
    Assert.NotNull(localizer);
    Assert.IsType<JsonStringLocalizerExtension>(localizer);
  }
}
