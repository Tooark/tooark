using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Tooark.Extensions;
using Tooark.Factories;

namespace Tooark.Tests.Factories;

public class JsonStringLocalizerFactoryTest
{
  private readonly Mock<IDistributedCache> _distributedCacheMock;
  private readonly JsonStringLocalizerFactory _factory;

  public JsonStringLocalizerFactoryTest()
  {
    _distributedCacheMock = new Mock<IDistributedCache>();
    _factory = new JsonStringLocalizerFactory(_distributedCacheMock.Object);
  }

  // Teste para verificar se o método Create retorna um JsonStringLocalizerExtension
  [Fact]
  public void Create_WithTypeResourceSource_ReturnsJsonStringLocalizer()
  {
    // Arrange
    var resourceSource = typeof(JsonStringLocalizerFactoryTest);

    // Act
    var localizer = _factory.Create(resourceSource);

    // Assert
    Assert.NotNull(localizer);
    Assert.IsType<JsonStringLocalizerExtension>(localizer);
  }

  // Teste para verificar se o método Create retorna um JsonStringLocalizerExtension
  [Fact]
  public void Create_WithBaseNameAndLocation_ReturnsJsonStringLocalizer()
  {
    // Arrange
    var baseName = "BaseName";
    var location = "Location";

    // Act
    var localizer = _factory.Create(baseName, location);

    // Assert
    Assert.NotNull(localizer);
    Assert.IsType<JsonStringLocalizerExtension>(localizer);
  }
}
