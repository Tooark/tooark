using Tooark.ValueObjects;
using Tooark.Exceptions;

namespace Tooark.Tests.ValueObjects;

public class SmtpProtocolTests
{
  /// <summary>
  /// Testa se cria um objeto válido a partir de um valor de protocolo SMTP válido.
  /// </summary>
  [Fact]
  public void Constructor_ValidSmtpValue_ShouldSetValue()
  {
    // Arrange
    var validSmtp = "smtp://example.com";

    // Act
    var smtpProtocol = new SmtpProtocol(validSmtp);

    // Assert
    Assert.Equal(validSmtp, smtpProtocol.Value);
  }

  /// <summary>
  /// Testa se lança uma exceção de argumento inválido a partir de um valor de protocolo SMTP inválido.
  /// </summary>
  [Fact]
  public void Constructor_InvalidSmtpValue_ShouldThrowException()
  {
    // Arrange
    var invalidSmtp = "test://example.com";

    // Act
    var exception = Assert.Throws<AppException>(() => new SmtpProtocol(invalidSmtp));

    // Assert
    Assert.Equal("Field.Invalid;SmtpProtocol", exception.Message);
  }

  /// <summary>
  /// Testa o método ToString() para retornar o valor do protocolo SMTP.
  /// </summary>
  [Fact]
  public void ToString_ShouldReturnValue()
  {
    // Arrange
    var validSmtp = "smtp://example.com";
    var smtpProtocol = new SmtpProtocol(validSmtp);

    // Act
    var result = smtpProtocol.ToString();

    // Assert
    Assert.Equal(validSmtp, result);
  }

  /// <summary>
  /// Testa a conversão implícita de SmtpProtocol para string.
  /// </summary>
  [Fact]
  public void ImplicitConversion_ToString_ShouldReturnValue()
  {
    // Arrange
    var validSmtp = "smtp://example.com";
    var smtpProtocol = new SmtpProtocol(validSmtp);

    // Act
    string result = smtpProtocol;

    // Assert
    Assert.Equal(validSmtp, result);
  }

  /// <summary>
  /// Testa a conversão implícita de string para SmtpProtocol.
  /// </summary>
  [Fact]
  public void ImplicitConversion_FromString_ShouldCreateSmtpProtocol()
  {
    // Arrange
    var validSmtp = "smtp://example.com";

    // Act
    SmtpProtocol smtpProtocol = validSmtp;

    // Assert
    Assert.Equal(validSmtp, smtpProtocol.Value);
  }
}
