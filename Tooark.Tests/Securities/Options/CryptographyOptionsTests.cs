using Tooark.Securities.Options;

namespace Tooark.Tests.Securities.Options;

public class CryptographyOptionsTests
{
  // Teste para verificar se o algoritmo é mapeado corretamente
  [Theory]
  [InlineData("CBC", "CBC")]
  [InlineData("GCM", "GCM")]
  [InlineData("CBCUnsafe", "CBCZeroIv")]
  [InlineData("invalid", "GCM")]
  [InlineData(null, "GCM")]
  public void Algorithm_SetValue_MapsToExpectedAlgorithm(string? input, string expected)
  {
    // Arrange & Act
    var options = new CryptographyOptions
    {
      Algorithm = input!
    };

    // Assert
    Assert.Contains(expected, options.Algorithm, StringComparison.OrdinalIgnoreCase);
  }

  // Teste para verificar secret
  [Fact]
  public void Secret_SetAndGet_ReturnsSameValue()
  {
    // Arrange & Act
    var options = new CryptographyOptions
    {
      Secret = "mysecret-crypto"
    };

    // Assert
    Assert.Equal("mysecret-crypto", options.Secret);
  }
}
