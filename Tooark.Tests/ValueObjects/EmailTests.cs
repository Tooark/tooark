using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class EmailTests
{
  [Fact]
  public void Constructor_ValidEmail_ShouldCreateEmail()
  {
    // Arrange
    var validEmail = "test@example.com";

    // Act
    Email email = new (validEmail);

    // Assert
    Assert.Equal(validEmail, email.Value);
    Assert.Equal(validEmail, (string)email);
  }

  [Theory]
  [InlineData("test@@example.com")] // Email mais de um @
  [InlineData("test@example@.com")] // Email mais de um @
  [InlineData("test@example.com.")] // Email terminado com .
  [InlineData("test@example")] // Email sem domínio
  [InlineData("test@com.")] // Email terminado com .
  [InlineData("test@.com")] // Email com domínio inválido
  [InlineData("test@")] // Email com domínio inválido
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
