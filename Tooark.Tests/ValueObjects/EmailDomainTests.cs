using Tooark.Exceptions;
using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class EmailDomainTests
{
  // Testa se o construtor da classe EmailDomain cria um objeto válido a partir de um domínio de email válido
  [Theory]
  [InlineData("@example.com")] // EmailDomain válido
  [InlineData("@exa-ple.com")] // EmailDomain válido
  [InlineData("@example.com.br")] // EmailDomain válido
  [InlineData("@exa-ple.com.br")] // EmailDomain válido
  public void Constructor_CreateValidEmailDomain_WhenGivenValidAddress(string domain)
  {
    // Arrange
    var validEmailDomain = domain;

    // Arrange & Act
    EmailDomain emailDomain = new(validEmailDomain);

    // Assert
    Assert.Equal(validEmailDomain, emailDomain.Value);
    Assert.Equal(validEmailDomain, (string)emailDomain);
  }

  // Testa se o construtor da classe EmailDomain lança uma exceção de argumento inválido a partir de domínio de email inválido
  [Theory]
  [InlineData("teste@example.com")] // EmailDomain iniciado com parâmetro de email válido
  [InlineData("te.te@example.com")] // EmailDomain iniciado com parâmetro de email válido
  [InlineData("te-te@example.com")] // EmailDomain iniciado com parâmetro de email válido
  [InlineData("te_te@example.com")] // EmailDomain iniciado com parâmetro de email válido
  [InlineData("teste@.")] // Domínio com apenas .
  [InlineData("test@example@.com")] // Domínio com @
  [InlineData("@.example.com")] // Domínio iniciado com .
  [InlineData("@-example.com")] // Domínio iniciado com -
  [InlineData("@_example.com")] // Domínio iniciado com _
  [InlineData("@example-.com")] // Domínio terminado com -
  [InlineData("@example_.com")] // Domínio terminado com _
  [InlineData("@exa!ple.com")] // Domínio que carácter especial !
  [InlineData("@exa#ple.com")] // Domínio que carácter especial #
  [InlineData("@exa$ple.com")] // Domínio que carácter especial $
  [InlineData("@exaçple.com")] // Domínio que carácter especial ç
  [InlineData("@example.-com")] // Domínio iniciado com -
  [InlineData("@example._com")] // Domínio iniciado com _
  [InlineData("@example.com-")] // Domínio terminado com -
  [InlineData("@example.com_")] // Domínio terminado com _
  [InlineData("@example.com.")] // Domínio terminado com .
  [InlineData("@example..com")] // Domínio com dois .
  [InlineData("@example")] // Domínio sem extensão
  [InlineData("@.com")] // Domínio ausente apenas extensão
  [InlineData("@.")] // Domínio com apenas ponto
  [InlineData("@")] // Domínio ausente
  [InlineData("")] // EmailDomain vazio
  [InlineData(null)] // EmailDomain nulo
  public void Constructor_ThrowsAppException_WhenGivenInvalidDomain(string? domain)
  {
    // Arrange
    var invalidEmailDomain = domain;

    // Act
    var exception = Assert.Throws<AppException>(() => new EmailDomain(invalidEmailDomain!));

    // Assert
    Assert.Equal("Field.Invalid;EmailDomain", exception.Message);
  }

  // Testa se o construtor da classe EmailDomain lança uma exceção de argumento inválido a partir de um domínio de email nulo
  [Fact]
  public void Constructor_ThrowsAppException_WhenGivenNullDomain()
  {
    // Arrange
    string nullEmailDomain = null!;

    // Act
    var exception = Assert.Throws<AppException>(() => new EmailDomain(nullEmailDomain));

    // Assert
    Assert.Equal("Field.Invalid;EmailDomain", exception.Message);
  }

  // Testa se o construtor da classe EmailDomain lança uma exceção de argumento inválido a partir de um domínio de email vazio
  [Fact]
  public void Constructor_ThrowsAppException_WhenGivenEmptyDomain()
  {
    // Arrange
    var emptyEmailDomain = "";

    // Act
    var exception = Assert.Throws<AppException>(() => new EmailDomain(emptyEmailDomain));

    // Assert
    Assert.Equal("Field.Invalid;EmailDomain", exception.Message);
  }

  // Testa se o método ToString retorna o valor do domínio de email
  [Fact]
  public void ToString_ShouldReturnValue()
  {
    // Arrange
    var validEmailDomain = "@example.com";

    // Act
    var email = new EmailDomain(validEmailDomain);
    var result = email.ToString();

    // Assert
    Assert.Equal(validEmailDomain, result);
  }

  // Testa se o operador implícito de conversão de string para EmailDomain funciona corretamente
  [Fact]
  public void ImplicitOperator_ConvertsStringToEmailDomain()
  {
    // Arrange
    var validEmailDomain = "@example.com";

    // Act
    EmailDomain email = validEmailDomain;

    // Assert
    Assert.Equal(validEmailDomain, email.Value);
    Assert.Equal(validEmailDomain, (string)email);
  }

  // Testa se o operador implícito de conversão de EmailDomain para string funciona corretamente
  [Fact]
  public void ImplicitOperator_ConvertsEmailDomainToString()
  {
    // Arrange
    string validEmailDomain = "@example.com";

    // Act
    string email = new EmailDomain(validEmailDomain);

    // Assert
    Assert.Equal(validEmailDomain, email);
  }
}
