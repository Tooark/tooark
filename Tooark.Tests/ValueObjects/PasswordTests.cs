using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class PasswordTests
{
  // Testa o construtor da classe Password com uma senha válida
  [Fact]
  public void Constructor_ValidPassword_ShouldCreatePassword()
  {
    // Arrange
    var validPassword = "Senha@123";

    // Act
    Password password = new(validPassword);

    // Assert
    Assert.Equal(validPassword, password.Value);
    Assert.Equal(validPassword, (string)password);
  }

  // Testa o construtor da classe Password com uma senha inválida
  [Fact]
  public void Constructor_InvalidPassword_ShouldThrowArgumentException()
  {
    // Arrange
    var invalidPassword = "senha"; // Não atende aos critérios de complexidade

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => new Password(invalidPassword));
    Assert.Equal("InvalidField;Password", exception.Message);
  }

  // Testa o construtor da classe Password com uma senha vazia
  [Fact]
  public void Constructor_EmptyPassword_ShouldThrowArgumentException()
  {
    // Arrange
    var emptyPassword = "";

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => new Password(emptyPassword));
    Assert.Equal("InvalidField;Password", exception.Message);
  }

  // Testa o operador implícito de conversão de string para Password
  [Fact]
  public void ImplicitOperator_StringToPassword_ShouldConvert()
  {
    // Arrange
    var validPassword = "Senha@123";

    // Act
    Password password = validPassword;

    // Assert
    Assert.Equal(validPassword, password.Value);
    Assert.Equal(validPassword, (string)password);
  }

  // Testa o operador implícito de conversão de Password para string
  [Fact]
  public void ImplicitOperator_PasswordToString_ShouldConvert()
  {
    // Arrange
    var validPassword = "Senha@123";
    Password password = new(validPassword);

    // Act
    string passwordString = password;

    // Assert
    Assert.Equal(validPassword, passwordString);
  }

  // Testa o construtor da classe Password com parâmetros personalizados de validação
  [Theory]
  [InlineData("SENHA@12", false, true, true, true, 8, true)] // Sem minúscula, mas válido de acordo com os parâmetros
  [InlineData("senha@12", true, false, true, true, 8, true)] // Sem maiúscula, mas válido de acordo com os parâmetros
  [InlineData("Senha@ab", true, true, false, true, 8, true)] // Sem número, mas válido de acordo com os parâmetros
  [InlineData("Senha123", true, true, true, false, 8, true)] // Sem carácter especial, mas válido de acordo com os parâmetros
  [InlineData("1234!@#$", false, false, true, true, 8, true)] // Sem minúscula e maiúscula, mas válido de acordo com os parâmetros
  [InlineData("SENHA!@#", false, true, false, true, 8, true)] // Sem minúscula e número, mas válido de acordo com os parâmetros
  [InlineData("SENHA123", false, true, true, false, 8, true)] // Sem minúscula e carácter especial, mas válido de acordo com os parâmetros
  [InlineData("senha!@#", true, false, false, true, 8, true)] // Sem maiúscula e número, mas válido de acordo com os parâmetros
  [InlineData("senha123", true, false, true, false, 8, true)] // Sem maiúscula e carácter especial, mas válido de acordo com os parâmetros
  [InlineData("SENHAabc", true, true, false, false, 8, true)] // Sem número e carácter especial, mas válido de acordo com os parâmetros
  [InlineData("Senha@12", true, true, true, true, 9, false)] // Tamanho insuficiente
  public void Constructor_CustomParameters_ShouldValidateAccordingly(
    string passwordValue,
    bool useLowercase,
    bool useUppercase,
    bool useNumbers,
    bool useSymbols,
    int passwordLength,
    bool valid)
  {
    if (valid)
    {
      // Act & Assert para senha válida
      var password = new Password(passwordValue, useLowercase, useUppercase, useNumbers, useSymbols, passwordLength);
      Assert.Equal(passwordValue, password.Value);
    }
    else
    {
      // Act & Assert para senha inválida
      var exception = Assert.Throws<ArgumentException>(() => new Password(passwordValue, useLowercase, useUppercase, useNumbers, useSymbols, passwordLength));
      Assert.Equal("InvalidField;Password", exception.Message);
    }
  }
}
