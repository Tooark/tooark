using Tooark.Attributes;

namespace Tooark.Tests.Attributes;

public class UrlValidationAttributeTest
{
  // Instância do atributo de UrlValidationAttribute para ser testado
  private readonly UrlValidationAttribute _urlValidationAttribute = new();

  // Teste de url válida com url válida
  [Theory]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsValid_ShouldBeValid_WhenGivenValidUrl(string? document)
  {
    // Arrange & Act
    var result = _urlValidationAttribute.IsValid(document);

    // Assert
    Assert.True(result);
  }

  // Teste de url inválida com url inválida
  [Theory]
  [InlineData("example.com")]
  [InlineData("ftp:example.com")]
  [InlineData("sftp:example.com")]
  [InlineData("http:example.com")]
  [InlineData("https:example.com")]
  [InlineData("imap:example.com")]
  [InlineData("pop3:example.com")]
  [InlineData("smtp:example.com")]
  [InlineData("ws:example.com")]
  [InlineData("wss:example.com")]
  public void IsValid_ShouldBeInvalid_WhenGivenInvalidUrl(string? document)
  {
    // Arrange & Act
    var result = _urlValidationAttribute.IsValid(document);

    // Assert
    Assert.False(result);
    Assert.Equal("Field.Invalid;Url", _urlValidationAttribute.ErrorMessage);
  }

  // Teste de url nulo ou vazio
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void IsValid_NullUrl_SetsErrorMessage(string? document)
  {
    // Arrange & Act
    var result = _urlValidationAttribute.IsValid(document);

    // Assert
    Assert.False(result);
    Assert.Equal("Field.Required;Url", _urlValidationAttribute.ErrorMessage);
  }
}
