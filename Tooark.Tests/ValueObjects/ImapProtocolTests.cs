using Tooark.ValueObjects;
using Tooark.Exceptions;

namespace Tooark.Tests.ValueObjects;

public class ImapProtocolTests
{
  /// <summary>
  /// Testa se cria um objeto válido a partir de um valor de protocolo IMAP/POP3 válido.
  /// </summary>
  [Theory]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  public void Constructor_ValidImapValue_ShouldSetValue(string imap)
  {
    // Arrange
    var validImap = imap;

    // Act
    var imapProtocol = new ImapProtocol(validImap);

    // Assert
    Assert.Equal(validImap, imapProtocol.Value);
  }

  /// <summary>
  /// Testa se lança uma exceção de argumento inválido a partir de um valor de protocolo IMAP/POP3 inválido.
  /// </summary>
  [Fact]
  public void Constructor_InvalidImapValue_ShouldThrowException()
  {
    // Arrange
    var invalidImap = "test://example.com";

    // Act
    var exception = Assert.Throws<AppException>(() => new ImapProtocol(invalidImap));

    // Assert
    Assert.Equal("Field.Invalid;ImapProtocol", exception.Message);
  }

  /// <summary>
  /// Testa o método ToString() para retornar o valor do protocolo IMAP/POP3.
  /// </summary>
  [Fact]
  public void ToString_ShouldReturnValue()
  {
    // Arrange
    var validImap = "imap://example.com";
    var imapProtocol = new ImapProtocol(validImap);

    // Act
    var result = imapProtocol.ToString();

    // Assert
    Assert.Equal(validImap, result);
  }

  /// <summary>
  /// Testa a conversão implícita de ImapProtocol para string.
  /// </summary>
  [Fact]
  public void ImplicitConversion_ToString_ShouldReturnValue()
  {
    // Arrange
    var validImap = "imap://example.com";
    var imapProtocol = new ImapProtocol(validImap);

    // Act
    string result = imapProtocol;

    // Assert
    Assert.Equal(validImap, result);
  }

  /// <summary>
  /// Testa a conversão implícita de string para ImapProtocol.
  /// </summary>
  [Fact]
  public void ImplicitConversion_FromString_ShouldCreateImapProtocol()
  {
    // Arrange
    var validImap = "imap://example.com";

    // Act
    ImapProtocol imapProtocol = validImap;

    // Assert
    Assert.Equal(validImap, imapProtocol.Value);
  }
}
