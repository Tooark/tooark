using Tooark.Attributes;

namespace Tooark.Tests.Attributes;

public class PasswordComplexityAttributeTests
{
  // Teoria de testes para validar a complexidade da senha
  // Cada InlineData representa um caso de teste com a senha e o resultado esperado (true para válido, false para inválido)
  // Método de teste para validar a complexidade da senha
  [Theory]
  [InlineData("Senha@12", true)] // Senha válida
  [InlineData("senha@12", false)] // Sem letra maiúscula
  [InlineData("SENHA@12", false)] // Sem letra minúscula
  [InlineData("Senha!@#", false)] // Sem número
  [InlineData("Senha123", false)] // Sem símbolo
  [InlineData("Senha@1", false)] // Tamanho insuficiente
  public void IsValid_ShouldValidatePasswordsCorrectly(string password, bool expected)
  {
    // Arrange
    var attribute = new PasswordComplexityAttribute();

    // Act
    var result = attribute.IsValid(password);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teoria de testes com parâmetros customizados para validar a complexidade da senha
  // Cada InlineData representa um caso de teste com a senha e parâmetros customizados para a validação
  // Método de teste para validar a complexidade da senha com parâmetros customizados
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
  public void IsValid_CustomParameters_ShouldValidatePasswordsCorrectly(
    string password,
    bool useLowercase,
    bool useUppercase,
    bool useNumbers,
    bool useSymbols,
    int passwordLength,
    bool expected)
  {
    // Arrange
    var attribute = new PasswordComplexityAttribute(useLowercase, useUppercase, useNumbers, useSymbols, passwordLength);

    // Act
    var result = attribute.IsValid(password);

    // Assert
    Assert.Equal(expected, result);
  }
}
