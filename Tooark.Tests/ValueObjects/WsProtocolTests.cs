using Tooark.ValueObjects;
using Tooark.Exceptions;

namespace Tooark.Tests.ValueObjects;

public class WsProtocolTests
{
  /// <summary>
  /// Testa se cria um objeto válido a partir de um valor de protocolo WS válido.
  /// </summary>
  [Theory]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void Constructor_ValidWsValue_ShouldSetValue(string ws)
  {
    // Arrange
    var validWs = ws;

    // Act
    var wsProtocol = new WsProtocol(validWs);

    // Assert
    Assert.Equal(validWs, wsProtocol.Value);
  }

  /// <summary>
  /// Testa se lança uma exceção de argumento inválido a partir de um valor de protocolo WS inválido.
  /// </summary>
  [Fact]
  public void Constructor_InvalidWsValue_ShouldThrowException()
  {
    // Arrange
    var invalidWs = "test://example.com";

    // Act
    var exception = Assert.Throws<AppException>(() => new WsProtocol(invalidWs));

    // Assert
    Assert.Equal("Field.Invalid;WsProtocol", exception.Message);
  }

  /// <summary>
  /// Testa o método ToString() para retornar o valor do protocolo WS.
  /// </summary>
  [Fact]
  public void ToString_ShouldReturnValue()
  {
    // Arrange
    var validWs = "ws://example.com";
    var wsProtocol = new WsProtocol(validWs);

    // Act
    var result = wsProtocol.ToString();

    // Assert
    Assert.Equal(validWs, result);
  }

  /// <summary>
  /// Testa a conversão implícita de WsProtocol para string.
  /// </summary>
  [Fact]
  public void ImplicitConversion_ToString_ShouldReturnValue()
  {
    // Arrange
    var validWs = "ws://example.com";
    var wsProtocol = new WsProtocol(validWs);

    // Act
    string result = wsProtocol;

    // Assert
    Assert.Equal(validWs, result);
  }

  /// <summary>
  /// Testa a conversão implícita de string para WsProtocol.
  /// </summary>
  [Fact]
  public void ImplicitConversion_FromString_ShouldCreateWsProtocol()
  {
    // Arrange
    var validWs = "ws://example.com";

    // Act
    WsProtocol wsProtocol = validWs;

    // Assert
    Assert.Equal(validWs, wsProtocol.Value);
  }
}
