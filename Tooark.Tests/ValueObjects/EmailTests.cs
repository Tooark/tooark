using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class EmailTests
{
  [Theory]
  [InlineData("teste@example.com")] // Email válido
  [InlineData("te.te@example.com")] // Email válido
  [InlineData("te-te@example.com")] // Email válido
  [InlineData("te_te@example.com")] // Email válido
  [InlineData("teste@exa-ple.com")] // Email válido
  public void Constructor_ValidEmail_ShouldCreateEmail(string validEmail)
  {
    // Act
    Email email = new(validEmail);

    // Assert
    Assert.Equal(validEmail, email.Value);
    Assert.Equal(validEmail, (string)email);
  }

  [Theory]
  
  [InlineData(".test@example.com")] // Email iniciado com .
  [InlineData("-test@example.com")] // Email iniciado com -
  [InlineData("_test@example.com")] // Email iniciado com _
  [InlineData("test.@example.com")] // Email terminado com .
  [InlineData("test-@example.com")] // Email terminado com -
  [InlineData("test_@example.com")] // Email terminado com _
  [InlineData("te!st@example.com")] // Email que carácter especial !
  [InlineData("te#st@example.com")] // Email que carácter especial #
  [InlineData("te$st@example.com")] // Email que carácter especial $
  [InlineData("teçst@example.com")] // Email que carácter especial ç
  [InlineData("test@@example.com")] // Email com mais de um @
  [InlineData(".@example.com")] // Email com apenas .
  [InlineData("teste@.")] // Domínio com apenas .
  [InlineData("test@example@.com")] // Domínio com @
  [InlineData("test@.example.com")] // Domínio iniciado com .
  [InlineData("test@-example.com")] // Domínio iniciado com -
  [InlineData("test@_example.com")] // Domínio iniciado com _
  [InlineData("test@example-.com")] // Domínio terminado com -
  [InlineData("test@example_.com")] // Domínio terminado com _
  [InlineData("teste@exa!ple.com")] // Domínio que carácter especial !
  [InlineData("teste@exa#ple.com")] // Domínio que carácter especial #
  [InlineData("teste@exa$ple.com")] // Domínio que carácter especial $
  [InlineData("teste@exaçple.com")] // Domínio que carácter especial ç
  [InlineData("test@example.-com")] // Domínio iniciado com -
  [InlineData("test@example._com")] // Domínio iniciado com _
  [InlineData("test@example.com-")] // Domínio terminado com -
  [InlineData("test@example.com_")] // Domínio terminado com _
  [InlineData("test@example.com.")] // Domínio terminado com .
  [InlineData("test@exaple..com")] // Domínio com dois .
  [InlineData("test@example")] // Domínio sem extensão
  [InlineData("test@.com")] // Domínio ausente apenas extensão
  [InlineData("test@")] // Domínio ausente
  [InlineData("")] // Email vazio
  [InlineData(null)] // Email nulo
  public void Constructor_InvalidEmail_ShouldThrowArgumentException(string email)
  {
    // Arrange
    var invalidEmail = email;

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
    Assert.Equal("InvalidField;Email", exception.Message);
  }

  [Fact]
  public void Constructor_NullEmail_ShouldThrowArgumentException()
  {
    // Arrange
    string nullEmail = null!;

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => new Email(nullEmail));
    Assert.Equal("InvalidField;Email", exception.Message);
  }

  [Fact]
  public void Constructor_EmptyEmail_ShouldThrowArgumentException()
  {
    // Arrange
    var emptyEmail = "";

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => new Email(emptyEmail));
    Assert.Equal("InvalidField;Email", exception.Message);
  }

  [Fact]
  public void ImplicitOperator_StringToEmail_ShouldConvert()
  {
    // Arrange
    var validEmail = "test@example.com";

    // Act
    Email email = validEmail;

    // Assert
    Assert.Equal(validEmail, email.Value);
    Assert.Equal(validEmail, (string)email);
  }

  [Fact]
  public void ImplicitOperator_EmailToString_ShouldConvert()
  {
    // Arrange
    string validEmail = "test@example.com";

    // Act
    string email = new Email(validEmail);

    // Assert
    Assert.Equal(validEmail, email);
  }
}
