using Tooark.Options;

namespace Tooark.Tests.Options;

public class LocalizerOptionsTests
{
  // Testes recursos adicionais nulos quando não definidos
  [Fact]
  public void ResourceAdditionalPaths_ShouldBeNull_WhenNotSet()
  {
    // Arrange
    var options = new LocalizerOptions();

    // Act
    var result = options.ResourceAdditionalPaths;

    // Assert
    Assert.Null(result);
  }

  // Testes de recursos adicionais definidos corretamente
  [Fact]
  public void ResourceAdditionalPaths_ShouldSetAndGetCorrectly()
  {
    // Arrange
    var options = new LocalizerOptions();
    var paths = new Dictionary<string, string>
    {
      { "en", "path/to/english.json" },
      { "es", "path/to/spanish.json" }
    };

    // Act
    options.ResourceAdditionalPaths = paths;
    var result = options.ResourceAdditionalPaths;

    // Assert
    Assert.Equal(paths, result);
  }

  // Testes de arquivo Stream nulo quando não definido
  [Fact]
  public void FileStream_ShouldBeNull_WhenNotSet()
  {
    // Arrange
    var options = new LocalizerOptions();

    // Act
    var result = options.FileStream;

    // Assert
    Assert.Null(result);
  }

  // Testes de arquivo Stream definido corretamente
  [Fact]
  public void FileStream_ShouldSetAndGetCorrectly()
  {
    // Arrange
    var options = new LocalizerOptions();
    var stream = new MemoryStream();

    // Act
    options.FileStream = stream;
    var result = options.FileStream;

    // Assert
    Assert.Equal(stream, result);
  }
}
