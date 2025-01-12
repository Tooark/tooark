using Tooark.ValueObjects;
using Tooark.Exceptions;

namespace Tooark.Tests.ValueObjects;

public class HttpProtocolTests
{
  /// <summary>
  /// Testa se cria um objeto válido a partir de um valor de protocolo HTTP válido.
  /// </summary>
  [Theory]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  public void Constructor_ValidHttpValue_ShouldSetValue(string http)
  {
    // Arrange
    var validHttp = http;

    // Act
    var httpProtocol = new HttpProtocol(validHttp);

    // Assert
    Assert.Equal(validHttp, httpProtocol.Value);
  }

  /// <summary>
  /// Testa se lança uma exceção de argumento inválido a partir de um valor de protocolo HTTP inválido.
  /// </summary>
  [Fact]
  public void Constructor_InvalidHttpValue_ShouldThrowException()
  {
    // Arrange
    var invalidHttp = "test://example.com";

    // Act
    var exception = Assert.Throws<AppException>(() => new HttpProtocol(invalidHttp));

    // Assert
    Assert.Equal("Field.Invalid;HttpProtocol", exception.Message);
  }

  /// <summary>
  /// Testa o método ToString() para retornar o valor do protocolo HTTP.
  /// </summary>
  [Fact]
  public void ToString_ShouldReturnValue()
  {
    // Arrange
    var validHttp = "http://example.com";
    var httpProtocol = new HttpProtocol(validHttp);

    // Act
    var result = httpProtocol.ToString();

    // Assert
    Assert.Equal(validHttp, result);
  }

  /// <summary>
  /// Testa a conversão implícita de HttpProtocol para string.
  /// </summary>
  [Fact]
  public void ImplicitConversion_ToString_ShouldReturnValue()
  {
    // Arrange
    var validHttp = "http://example.com";
    var httpProtocol = new HttpProtocol(validHttp);

    // Act
    string result = httpProtocol;

    // Assert
    Assert.Equal(validHttp, result);
  }

  /// <summary>
  /// Testa a conversão implícita de string para HttpProtocol.
  /// </summary>
  [Fact]
  public void ImplicitConversion_FromString_ShouldCreateHttpProtocol()
  {
    // Arrange
    var validHttp = "http://example.com";

    // Act
    HttpProtocol httpProtocol = validHttp;

    // Assert
    Assert.Equal(validHttp, httpProtocol.Value);
  }
}
