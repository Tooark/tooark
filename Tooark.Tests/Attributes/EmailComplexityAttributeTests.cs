using Tooark.Attributes;

namespace Tooark.Tests.Attributes;

public class EmailComplexityAttributeTests
{
  private readonly EmailComplexityAttribute _emailComplexityAttribute = new EmailComplexityAttribute();

  [Theory]
  [InlineData("test@example.com", true)] // Email válido
  [InlineData("test@@example.com", false)] // Email mais de um @
  [InlineData("test@example@.com", false)] // Email mais de um @
  [InlineData("test@example.com.", false)] // Email terminado com .
  [InlineData("test@example", false)] // Email sem domínio
  [InlineData("test@com.", false)] // Email terminado com .
  [InlineData("test@.com", false)] // Email com domínio inválido
  [InlineData("test@", false)] // Email com domínio inválido
  [InlineData("", false)] // Email vazio
  [InlineData(null, false)] // Email nulo
  public void IsValid_ShouldValidateEmailsCorrectly(string email, bool expected)
  {
    // Act
    var result = _emailComplexityAttribute.IsValid(email);

    // Assert
    Assert.Equal(expected, result);
  }
}
