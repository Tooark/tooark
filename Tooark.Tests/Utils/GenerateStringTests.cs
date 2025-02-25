using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class GenerateStringTests
{
  // Testa se String retorna a string esperada
  [Theory]
  [InlineData(0, "")]
  [InlineData(1, "A")]
  [InlineData(2, "B")]
  [InlineData(26, "Z")]
  [InlineData(27, "AA")]
  [InlineData(28, "AB")]
  [InlineData(52, "AZ")]
  [InlineData(53, "BA")]
  [InlineData(54, "BB")]
  [InlineData(78, "BZ")]
  [InlineData(703, "AAA")]
  [InlineData(704, "AAB")]
  [InlineData(728, "AAZ")]
  [InlineData(729, "ABA")]
  [InlineData(730, "ABB")]
  [InlineData(754, "ABZ")]
  public void String_ShouldReturnExpectedString(int number, string expected)
  {
    // Arrange & Act
    var result = GenerateString.Sequential(number);

    // Assert
    Assert.Equal(expected, result);
  }

  // Testa se Password gera uma string com o comprimento correto
  [Theory]
  [InlineData(8)]
  [InlineData(12)]
  [InlineData(16)]
  [InlineData(20)]
  public void Password_ShouldReturnStringWithCorrectLength(int expected)
  {
    // Arrange & Act
    var result = GenerateString.Password(expected);

    // Assert
    Assert.Equal(expected, result.Length);
  }

  // Testa se Password gera uma string com o comprimento mínimo
  [Theory]
  [InlineData(0)]
  [InlineData(2)]
  [InlineData(4)]
  [InlineData(6)]
  public void Password_ShouldReturnStringWithMinLength(int expected)
  {
    // Arrange & Act
    var result = GenerateString.Password(expected);

    // Assert
    Assert.Equal(8, result.Length);
  }

  // Testa se Password lança ArgumentException quando todos os tipos de caracteres estão desativados
  [Fact]
  public void Password_ShouldReturnString_WhenAllCharacterTypesDisabled()
  {
    // Arrange & Act
    var result = GenerateString.Password(12, false, false, false, false);

    // Assert
    Assert.Equal(12, result.Length);
  }

  // Testa se Hexadecimal gera uma string hexadecimal com o comprimento correto
  [Theory]
  [InlineData(16)]
  [InlineData(32)]
  [InlineData(64)]
  public void Hexadecimal_ShouldReturnStringWithCorrectLength(int expected)
  {
    // Arrange & Act
    var result = GenerateString.Hexadecimal(expected);

    // Assert
    Assert.Equal(expected, result.Length);
  }

  // Testa se Hexadecimal gera uma string hexadecimal com o comprimento mínimo
  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(2)]
  public void Hexadecimal_ShouldReturnStringWithMinLength(int expected)
  {
    // Arrange & Act
    var result = GenerateString.Hexadecimal(expected);

    // Assert
    Assert.Equal(2, result.Length);
  }

  // Testa se GuidCode gera uma string com o comprimento correto
  [Fact]
  public void GuidCode_ShouldReturnStringWithCorrectLength()
  {
    // Arrange & Act
    var result = GenerateString.GuidCode();

    // Assert
    Assert.Equal(32, result.Length);
  }

  // Testa se Token gera uma string com o comprimento correto
  [Theory]
  [InlineData(260)]
  [InlineData(280)]
  [InlineData(300)]
  public void Token_ShouldReturnStringWithCorrectLength(int expected)
  {
    // Arrange & Act
    var result = GenerateString.Token(expected);

    // Assert
    Assert.Equal(expected, result.Length);
  }

  // Testa se Token gera uma string com o comprimento mínimo
  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(2)]
  public void Token_ShouldReturnStringWithMinLength(int expected)
  {
    // Arrange & Act
    var result = GenerateString.Token(expected);

    // Assert
    Assert.Equal(256, result.Length);
  }
}
