using Tooark.Extensions.ValueObjects;

namespace Tooark.Tests.Extensions.Dtos;

public class ResourcePathTest
{
  // Teste quando o construtor é chamado com parâmetros válidos
  [Fact]
  public void Constructor_ShouldInitializeProperties()
  {
    // Arrange
    string languageCode = "en";
    string path = "/resources/en/resource.json";

    // Act
    var resourcePath = new ResourcePath(languageCode, path);

    // Assert
    Assert.Equal(languageCode, resourcePath.LanguageCode);
    Assert.Equal(path, resourcePath.Path);
  }

  // Teste quando o código do idioma é nulo
  [Fact]
  public void Constructor_ShouldThrowArgumentNullException_WhenLanguageCodeIsNull()
  {
    // Arrange
    string languageCode = null!;
    string path = "/resources/en/resource.json";

    // Act
    var exception = Assert.Throws<ArgumentNullException>(() => new ResourcePath(languageCode, path));

    // Assert
    Assert.Equal("Value cannot be null. (Parameter 'languageCode')", exception.Message);
  }

  // Teste quando o path é nulo
  [Fact]
  public void Constructor_ShouldThrowArgumentNullException_WhenPathIsNull()
  {
    // Arrange
    string languageCode = "en";
    string path = null!;

    // Act
    var exception = Assert.Throws<ArgumentNullException>(() => new ResourcePath(languageCode, path));

    // Assert
    Assert.Equal("Value cannot be null. (Parameter 'path')", exception.Message);
  }
}
