using Tooark.ValueObjects;
using Tooark.Exceptions;

namespace Tooark.Tests.ValueObjects;

public class UrlTests
{
  /// <summary>
  /// Testa se cria um objeto válido a partir de um valor de uma URL válida.
  /// </summary>
  [Theory]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void Constructor_ValidUrl_ShouldSetUrlValue(string urlParam)
  {
    // Arrange
    var validUrl = urlParam;

    // Act
    var url = new Url(validUrl);

    // Assert
    Assert.Equal(validUrl, url.Value);
  }

  /// <summary>
  /// Testa se lança uma exceção de argumento inválido a partir de um valor de uma URL inválida.
  /// </summary>
  [Theory]
  [InlineData("invalid-url")]
  [InlineData("htp://example.com")]
  [InlineData("://example.com")]
  public void Constructor_InvalidUrl_ShouldThrowAppException(string urlParam)
  {
    // Arrange
    var invalidUrl = urlParam;

    // Act
    var exception = Assert.Throws<AppException>(() => new Url(invalidUrl));

    // Assert
    Assert.Equal("Field.Invalid;Url", exception.Message);
  }

  /// <summary>
  /// Testa o método ToString() para retornar o valor da URL.
  /// </summary>
  [Fact]
  public void ToString_ShouldReturnUrlValue()
  {
    // Arrange
    var urlValue = "http://example.com";
    var url = new Url(urlValue);

    // Act
    var result = url.ToString();

    // Assert
    Assert.Equal(urlValue, result);
  }

  /// <summary>
  /// Testa a conversão implícita de Url para string.
  /// </summary>
  [Fact]
  public void ImplicitConversionToString_ShouldReturnUrlValue()
  {
    // Arrange
    var urlValue = "http://example.com";
    var url = new Url(urlValue);

    // Act
    string result = url;

    // Assert
    Assert.Equal(urlValue, result);
  }

  /// <summary>
  /// Testa a conversão implícita de string para Url.
  /// </summary>
  [Fact]
  public void ImplicitConversionFromString_ShouldCreateUrlInstance()
  {
    // Arrange
    var urlValue = "http://example.com";

    // Act
    Url url = urlValue;

    // Assert
    Assert.Equal(urlValue, url.Value);
  }
}
