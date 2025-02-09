using Tooark.Extensions.ValueObjects;

namespace Tooark.Tests.Extensions.Dtos;

public class ResourceStreamTest
{
  // Teste quando o construtor é chamado com parâmetros válidos
  [Fact]
  public void ResourceStream_ShouldInitializeProperties()
  {
    // Arrange
    string languageCode = "en";
    MemoryStream stream = new();

    // Act
    var resourceStream = new ResourceStream(languageCode, stream);

    // Assert
    Assert.Equal(languageCode, resourceStream.LanguageCode);
    Assert.Equal(stream, resourceStream.Stream);
  }

  // Teste quando o código do idioma é nulo
  [Fact]
  public void ResourceStream_ShouldThrowArgumentNullException_WhenLanguageCodeIsNull()
  {
    // Arrange
    string languageCode = null!;
    MemoryStream stream = new();

    // Act
    var exception = Assert.Throws<ArgumentNullException>(() => new ResourceStream(languageCode, stream));

    // Assert
    Assert.Equal("Value cannot be null. (Parameter 'languageCode')", exception.Message);
  }

  // Teste quando o stream é nulo
  [Fact]
  public void ResourceStream_ShouldThrowArgumentNullException_WhenStreamIsNull()
  {
    // Arrange
    string languageCode = "en";
    MemoryStream? stream = null!;

    // Act
    var exception = Assert.Throws<ArgumentNullException>(() => new ResourceStream(languageCode, stream));

    // Assert
    Assert.Equal("Value cannot be null. (Parameter 'stream')", exception.Message);
  }
}
