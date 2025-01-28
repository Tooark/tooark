using Tooark.Exceptions;
using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class PasswordTests
{
  // Testa se a senha é válida a partir de uma senha válida
  [Theory]
  [InlineData("zzZZ11##")]
  [InlineData("ZZzz11##")]
  [InlineData("11zzZZ##")]
  [InlineData("##zzZZ11")]
  public void Password_ShouldBeValid_WhenGivenValidPassword(string valueParam)
  {
    // Arrange
    var expectedValue = valueParam;

    // Act
    Password password = new(valueParam);

    // Assert
    Assert.True(password.IsValid);
    Assert.Equal(expectedValue, password.Value);
  }

  // Testa se a senha é inválida a partir de uma senha inválida
  [Theory]
  [InlineData("zzzzzzzz")]
  [InlineData("ZZZZZZZZ")]
  [InlineData("11111111")]
  [InlineData("!!!!!!!!")]
  [InlineData("zZzZzZzZ")]
  [InlineData("z1z1z1z1")]
  [InlineData("z!z!z!z!")]
  [InlineData("Z1Z1Z1Z1")]
  [InlineData("Z!Z!Z!Z!")]
  [InlineData("1!1!1!1!")]
  [InlineData("zZ1zZ1zZ")]
  [InlineData("zZ!zZ!zZ")]
  [InlineData("z1!z1!z1")]
  [InlineData("Z1!Z1!Z1")]
  [InlineData("zZ1!zZ1")]
  [InlineData("")]
  [InlineData(null)]
  public void Password_ShouldBeInvalid_WhenGivenInvalidPassword(string? valueParam)
  {
    // Arrange & Act
    var password = new Password(valueParam!);

    // Assert
    Assert.False(password.IsValid);
    Assert.Null(password.Value);
  }

  // Testa se a senha é válida a partir de uma senha válida para os parâmetros de validação
  [Theory]
  [InlineData("Senha@12", true, true, true, true, 8)] // Utiliza todos os parâmetros padrão
  [InlineData("Senha123", true, true, true, false, 8)] // Permite não ter carácter especial
  [InlineData("Senha!@#", true, true, false, true, 8)] // Permite não ter número
  [InlineData("SenhaSen", true, true, false, false, 8)] // Permite não ter número e carácter especial
  [InlineData("senha@12", true, false, true, true, 8)] // Permite não ter maiúscula
  [InlineData("senha123", true, false, true, false, 8)] // Permite não ter maiúscula e carácter especial
  [InlineData("senha!@#", true, false, false, true, 8)] // Permite não ter maiúscula e número
  [InlineData("senhasen", true, false, false, false, 8)] // Permite não ter maiúscula, número e carácter especial
  [InlineData("SENHA@12", false, true, true, true, 8)] // Permite não ter minúscula
  [InlineData("SENHA123", false, true, true, false, 8)] // Permite não ter minúscula e carácter especial
  [InlineData("SENHA!@#", false, true, false, true, 8)] // Permite não ter minúscula e número
  [InlineData("SENHASEN", false, true, false, false, 8)] // Permite não ter minúscula, número e carácter especial
  [InlineData("1234!@#$", false, false, true, true, 8)] // Permite não ter minúscula e maiúscula
  [InlineData("12341234", false, false, true, false, 8)] // Permite não ter minúscula e maiúscula e carácter especial
  [InlineData("!@#$!@#$", false, false, false, true, 8)] // Permite não ter minúscula e maiúscula e número
  [InlineData("Senha@12", false, false, false, false, 8)] // Todos os parâmetros desativados, considera regra padrão
  [InlineData("Senha@123", true, true, true, true, 9)] // Tamanho maior que o padrão
  [InlineData("Senha@12", true, true, true, true, 7)] // Tamanho menor que o padrão, considera tamanho padrão
  public void Password_ShouldBeValid_WhenGivenParams(string value, bool lower, bool upper, bool number, bool symbol, int length)
  {
    // Arrange
    var expectedValue = value;

    // Act
    Password password = new(value, lower, upper, number, symbol, length);

    // Assert
    Assert.True(password.IsValid);
    Assert.Equal(expectedValue, password.Value);
  }

  // Testa se a senha é inválida a partir de uma senha inválida para os parâmetros de validação
  [Theory]
  [InlineData("!@#$!@#$", true, true, true, false, 8)] // Só permite não ter carácter especial
  [InlineData("12341234", true, true, false, true, 8)] // Só permite não ter número
  [InlineData("1234!@#$", true, true, false, false, 8)] // Só permite não ter número e carácter especial
  [InlineData("SENHASEN", true, false, true, true, 8)] // Só permite não ter maiúscula
  [InlineData("SENHA!@#", true, false, true, false, 8)] // Só permite não ter maiúscula e carácter especial
  [InlineData("SENHA123", true, false, false, true, 8)] // Só permite não ter maiúscula e número
  [InlineData("SENHA@12", true, false, false, false, 8)] // Só permite não ter maiúscula, número e carácter especial
  [InlineData("senhasen", false, true, true, true, 8)] // Só permite não ter minúscula
  [InlineData("senha!@#", false, true, true, false, 8)] // Só permite não ter minúscula e carácter especial
  [InlineData("senha123", false, true, false, true, 8)] // Só permite não ter minúscula e número
  [InlineData("senha@12", false, true, false, false, 8)] // Só permite não ter minúscula, número e carácter especial
  [InlineData("senhaSEN", false, false, true, true, 8)] // Só permite não ter minúscula e maiúscula
  [InlineData("Senha!@#", false, false, true, false, 8)] // Só permite não ter minúscula e maiúscula e carácter especial
  [InlineData("Senha123", false, false, false, true, 8)] // Só permite não ter minúscula e maiúscula e número
  [InlineData("", false, false, false, false, 8)] // Todos os parâmetros desativados, considera regra padrão
  [InlineData("Senha@12", true, true, true, true, 9)] // Tamanho maior que o padrão
  [InlineData("Senha@1", true, true, true, true, 7)] // Tamanho menor que o padrão, considera tamanho padrão
  public void Password_ShouldBeInvalid_WhenGivenParams(string value, bool lower, bool upper, bool number, bool symbol, int length)
  {
    // Arrange & Act
    Password password = new(value, lower, upper, number, symbol, length);

    // Assert
    Assert.False(password.IsValid);
    Assert.Null(password.Value);
  }

// Testa se o método ToString retorna o código do idioma
  [Fact]
  public void Password_ShouldReturnCorrectStringRepresentation()
  {
    // Arrange
    var passwordValue = "Senha@12";
    var expectedCode = passwordValue;
    var password = new Password(passwordValue);

    // Act
    var passwordString = password.ToString();

    // Assert
    Assert.Equal(expectedCode, passwordString);
  }

  // Testa se o endereço de password está sendo convertido para string implicitamente
  [Fact]
  public void Password_ShouldConvertToStringImplicitly()
  {
    // Arrange
    var passwordValue = "Senha@12";
    var expectedCode = passwordValue;
    var password = new Password(passwordValue);

    // Act
    string passwordString = password;

    // Assert
    Assert.Equal(expectedCode, passwordString);
  }

  // Testa se o endereço de password está sendo convertido de string implicitamente
  [Fact]
  public void Password_ShouldConvertFromStringImplicitly()
  {
    // Arrange
    var passwordValue = "Senha@12";
    var expectedCode = passwordValue;

    // Act
    Password password = passwordValue;

    // Assert
    Assert.True(password.IsValid);
    Assert.Equal(expectedCode, password.Value);
  }
}
