using Tooark.Attributes;

namespace Tooark.Tests.Attributes;

public class EmailComplexityAttributeTests
{
  private readonly EmailComplexityAttribute _emailComplexityAttribute = new();

  [Theory]
  [InlineData("teste@example.com", true)] // Email válido
  [InlineData("te.te@example.com", true)] // Email válido
  [InlineData("te-te@example.com", true)] // Email válido
  [InlineData("te_te@example.com", true)] // Email válido
  [InlineData("teste@exa-ple.com", true)] // Email válido
  [InlineData(".test@example.com", false)] // Email iniciado com .
  [InlineData("-test@example.com", false)] // Email iniciado com -
  [InlineData("_test@example.com", false)] // Email iniciado com _
  [InlineData("test.@example.com", false)] // Email terminado com .
  [InlineData("test-@example.com", false)] // Email terminado com -
  [InlineData("test_@example.com", false)] // Email terminado com _
  [InlineData("te!st@example.com", false)] // Email que carácter especial !
  [InlineData("te#st@example.com", false)] // Email que carácter especial #
  [InlineData("te$st@example.com", false)] // Email que carácter especial $
  [InlineData("teçst@example.com", false)] // Email que carácter especial ç
  [InlineData("test@@example.com", false)] // Email com mais de um @
  [InlineData(".@example.com", false)] // Email com apenas .
  [InlineData("teste@.", false)] // Domínio com apenas .
  [InlineData("test@example@.com", false)] // Domínio com @
  [InlineData("test@.example.com", false)] // Domínio iniciado com .
  [InlineData("test@-example.com", false)] // Domínio iniciado com -
  [InlineData("test@_example.com", false)] // Domínio iniciado com _
  [InlineData("test@example-.com", false)] // Domínio terminado com -
  [InlineData("test@example_.com", false)] // Domínio terminado com _
  [InlineData("teste@exa!ple.com", false)] // Domínio que carácter especial !
  [InlineData("teste@exa#ple.com", false)] // Domínio que carácter especial #
  [InlineData("teste@exa$ple.com", false)] // Domínio que carácter especial $
  [InlineData("teste@exaçple.com", false)] // Domínio que carácter especial ç
  [InlineData("test@example.-com", false)] // Domínio iniciado com -
  [InlineData("test@example._com", false)] // Domínio iniciado com _
  [InlineData("test@example.com-", false)] // Domínio terminado com -
  [InlineData("test@example.com_", false)] // Domínio terminado com _
  [InlineData("test@example.com.", false)] // Domínio terminado com .
  [InlineData("test@exaple..com", false)] // Domínio com dois .
  [InlineData("test@example", false)] // Domínio sem extensão
  [InlineData("test@.com", false)] // Domínio ausente apenas extensão
  [InlineData("test@", false)] // Domínio ausente
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
