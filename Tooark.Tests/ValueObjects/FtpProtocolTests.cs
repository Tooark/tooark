using Tooark.ValueObjects;
using Tooark.Exceptions;

namespace Tooark.Tests.ValueObjects;

public class FtpProtocolTests
{
  /// <summary>
  /// Testa se cria um objeto válido a partir de um valor de protocolo FTP válido.
  /// </summary>
  [Theory]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  public void Constructor_ValidFtpValue_ShouldSetValue(string ftp)
  {
    // Arrange
    var validFtp = ftp;

    // Act
    var ftpProtocol = new FtpProtocol(validFtp);

    // Assert
    Assert.Equal(validFtp, ftpProtocol.Value);
  }

  /// <summary>
  /// Testa se lança uma exceção de argumento inválido a partir de um valor de protocolo FTP inválido.
  /// </summary>
  [Fact]
  public void Constructor_InvalidFtpValue_ShouldThrowException()
  {
    // Arrange
    var invalidFtp = "test://example.com";

    // Act
    var exception = Assert.Throws<AppException>(() => new FtpProtocol(invalidFtp));

    // Assert
    Assert.Equal("Field.Invalid;FtpProtocol", exception.Message);
  }

  /// <summary>
  /// Testa o método ToString() para retornar o valor do protocolo FTP.
  /// </summary>
  [Fact]
  public void ToString_ShouldReturnValue()
  {
    // Arrange
    var validFtp = "ftp://example.com";
    var ftpProtocol = new FtpProtocol(validFtp);

    // Act
    var result = ftpProtocol.ToString();

    // Assert
    Assert.Equal(validFtp, result);
  }

  /// <summary>
  /// Testa a conversão implícita de FtpProtocol para string.
  /// </summary>
  [Fact]
  public void ImplicitConversion_ToString_ShouldReturnValue()
  {
    // Arrange
    var validFtp = "ftp://example.com";
    var ftpProtocol = new FtpProtocol(validFtp);

    // Act
    string result = ftpProtocol;

    // Assert
    Assert.Equal(validFtp, result);
  }

  /// <summary>
  /// Testa a conversão implícita de string para FtpProtocol.
  /// </summary>
  [Fact]
  public void ImplicitConversion_FromString_ShouldCreateFtpProtocol()
  {
    // Arrange
    var validFtp = "ftp://example.com";

    // Act
    FtpProtocol ftpProtocol = validFtp;

    // Assert
    Assert.Equal(validFtp, ftpProtocol.Value);
  }
}
