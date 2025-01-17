using Tooark.ValueObjects;
using Tooark.Exceptions;

namespace Tooark.Tests.ValueObjects;

public class LanguageCodeTests
{
  // Teste de construtor com código válido
  [Fact]
  public void Constructor_WithValidCode_ShouldSetCode()
  {
    // Arrange
    var validCode = "en-US";

    // Act
    var languageCode = new LanguageCode(validCode);

    // Assert
    Assert.Equal("en-US", languageCode.Code);
  }

  // Teste de construtor com código minúsculo
  [Fact]
  public void Constructor_WithLowerCode_ShouldSetCode()
  {
    // Arrange
    var validCode = "en-us";

    // Act
    var languageCode = new LanguageCode(validCode);

    // Assert
    Assert.Equal("en-US", languageCode.Code);
  }

  // Teste de construtor com código maiúsculo
  [Fact]
  public void Constructor_WithUpperCode_ShouldSetCode()
  {
    // Arrange
    var validCode = "EN-US";

    // Act
    var languageCode = new LanguageCode(validCode);

    // Assert
    Assert.Equal("en-US", languageCode.Code);
  }

  // Teste de construtor com código inválido por tamanho
  [Theory]
  [InlineData("enUS")]
  [InlineData("enus")]
  [InlineData("ENUS")]
  public void Constructor_InvalidLength_ShouldThrowException(string code)
  {
    // Arrange
    var invalidCode = code;

    // Act
    var exception = Assert.Throws<AppException>(() => new LanguageCode(invalidCode));

    // Assert
    Assert.Equal("LanguageCode.ErrorLength;5", exception.Message);
  }

  // Teste de construtor com código inválido por formato
  [Theory]
  [InlineData("enUS-")]
  [InlineData("enus-")]
  [InlineData("ENUS-")]
  public void Constructor_InvalidFormat_ShouldThrowException(string code)
  {
    // Arrange
    var invalidCode = code;

    // Act
    var exception = Assert.Throws<AppException>(() => new LanguageCode(invalidCode));

    // Assert
    Assert.Equal("LanguageCode.ErrorFormat", exception.Message);
  }

  // Teste do método ToString
  [Fact]
  public void ToString_ShouldReturnCode()
  {
    // Arrange
    var validCode = "en-US";
    var languageCode = new LanguageCode(validCode);

    // Act
    var result = languageCode.ToString();

    // Assert
    Assert.Equal(validCode, result);
  }

  // Teste de conversão implícita de LanguageCode para string
  [Fact]
  public void ImplicitConversion_ToString_ShouldReturnCode()
  {
    // Arrange
    var validCode = "en-US";
    var languageCode = new LanguageCode(validCode);

    // Act
    string result = languageCode;

    // Assert
    Assert.Equal(validCode, result);
  }

  // Teste de conversão implícita de string para LanguageCode
  [Fact]
  public void ImplicitConversion_FromString_ShouldCreateLanguageCode()
  {
    // Arrange
    var validCode = "en-US";

    // Act
    LanguageCode languageCode = validCode;

    // Assert
    Assert.Equal(validCode, languageCode.Code);
  }
}
