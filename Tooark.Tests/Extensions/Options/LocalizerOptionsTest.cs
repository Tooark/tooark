using Tooark.Extensions.Options;
using Tooark.Extensions.ValueObjects;

namespace Tooark.Tests.Extensions.Options;

public class LocalizerOptionsTest
{
  // Teste para verificar se as propriedades de LocalizerOptions são nulas por padrão.
  [Fact]
  public void LocalizerOptions_ShouldBeNull_ByDefault()
  {
    // Arrange
    var options = new LocalizerOptions();

    // Act
    var paths = options.ResourceAdditionalPath;
    var streams = options.ResourceAdditionalStream;

    // Assert
    Assert.Equal("LocalizerResource", LocalizerOptions.Section);
    Assert.Null(paths);
    Assert.Null(streams);
  }

  // Teste para verificar se a propriedade ResourceAdditionalPath é configurável.
  [Fact]
  public void LocalizerOptions_ResourceAdditionalPath_ShouldBeSettable()
  {
    // Arrange
    var options = new LocalizerOptions();
    List<ResourcePath> resourcePaths = 
    [
      new ResourcePath("en-US", "path1"),
      new ResourcePath("pt-BR", "path2")
    ];

    // Act
    options.ResourceAdditionalPath = resourcePaths;

    // Assert
    Assert.Equal(resourcePaths, options.ResourceAdditionalPath);
  }

  // Teste para verificar se a propriedade ResourceAdditionalStream é configurável.
  [Fact]
  public void LocalizerOptions_ResourceAdditionalStream_ShouldBeSettable()
  {
    // Arrange
    var options = new LocalizerOptions();
    List<ResourceStream> resourceStreams =
    [
      new ResourceStream("en-US", new MemoryStream()),
      new ResourceStream("pt-BR", new MemoryStream())
    ];

    // Act
    options.ResourceAdditionalStream = resourceStreams;

    // Assert
    Assert.Equal(resourceStreams, options.ResourceAdditionalStream);
  }
}
