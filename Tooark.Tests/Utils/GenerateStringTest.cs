using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class GenerateStringTest
{
  // Testa se SequentialString retorna a string esperada
  [Theory]
  [InlineData(1, "a")]
  [InlineData(2, "b")]
  [InlineData(26, "z")]
  [InlineData(27, "aa")]
  [InlineData(28, "ab")]
  [InlineData(52, "az")]
  [InlineData(53, "ba")]
  [InlineData(54, "bb")]
  [InlineData(78, "bz")]
  [InlineData(703, "aaa")]
  [InlineData(704, "aab")]
  [InlineData(728, "aaz")]
  [InlineData(729, "aba")]
  [InlineData(730, "abb")]
  [InlineData(754, "abz")]
  public void SequentialStringGenerator_ShouldReturnExpectedString(int number, string expected)
  {
    // Arrange & Act
    var result = Util.SequentialString(number);

    // Assert
    Assert.Equal(expected, result);
  }

  // Testa se CriteriaString gera uma string com o comprimento correto
  [Theory]
  [InlineData(8)]
  [InlineData(12)]
  [InlineData(16)]
  [InlineData(20)]
  public void CriteriaStringGenerator_ShouldReturnStringWithCorrectLength(int length)
  {
    // Arrange & Act
    var result = Util.CriteriaString(length);

    // Assert
    Assert.Equal(length, result.Length);
  }

  // Testa se CriteriaString lança ArgumentException quando todos os tipos de caracteres estão desativados
  [Fact]
  public void CriteriaStringGenerator_ShouldThrowArgumentException_WhenAllCharacterTypesDisabled()
  {
    // Arrange, Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => Util.CriteriaString(12, false, false, false, false));
    Assert.Equal("MissingParameter", exception.Message);
  }

  // Testa se HexString gera uma string hexadecimal com o comprimento correto
  [Theory]
  [InlineData(16)]
  [InlineData(32)]
  [InlineData(64)]
  public void HexStringGenerator_ShouldReturnHexStringWithCorrectLength(int sizeToken)
  {
    // Arrange & Act
    var result = Util.HexString(sizeToken);

    // Assert
    Assert.Equal(sizeToken * 2, result.Length); // Cada byte é representado por 2 caracteres hexadecimais
  }
}
