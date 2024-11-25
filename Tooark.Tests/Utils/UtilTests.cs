using System.Globalization;
using static Tooark.Utils.Util;

namespace Tooark.Tests.Utils;

public class LanguagesTests
{
  // Testa se o valor padrão da linguagem é "en-US"
  [Fact]
  public void DefaultLanguage_ShouldBeEnUS()
  {
    // Assert
    Assert.Equal("en-US", Languages.Default);
  }

  // Testa se a propriedade Current reflete corretamente a cultura atual
  [Fact]
  public void CurrentLanguage_ShouldBeCurrentCulture()
  {
    Assert.Equal(CultureInfo.CurrentCulture.Name, Languages.Current);
  }

  // Testa se a cultura atual é alterada corretamente
  [Theory]
  [InlineData("en-US")]
  [InlineData("es-ES")]
  [InlineData("jp-JP")]
  [InlineData("pt-BR")]
  [InlineData("pt-PT")]
  public void SetCulture_ShouldChangeCurrentCulture(string newCulture)
  {
    // Arrange & Act
    Languages.SetCulture(newCulture);

    // Assert
    Assert.Equal(newCulture, Languages.Current);
    Assert.Equal(newCulture, CultureInfo.CurrentCulture.Name);
    Assert.Equal(newCulture, CultureInfo.CurrentUICulture.Name);
    Assert.Equal(newCulture, CultureInfo.DefaultThreadCurrentCulture?.Name);
    Assert.Equal(newCulture, CultureInfo.DefaultThreadCurrentUICulture?.Name);
  }

  // Testa se a cultura atual é alterada corretamente com um objeto CultureInfo
  [Theory]
  [InlineData("en-US")]
  [InlineData("es-ES")]
  [InlineData("jp-JP")]
  [InlineData("pt-BR")]
  [InlineData("pt-PT")]
  public void SetCulture_WithCultureInfo_SetsCurrentCulture(string newCulture)
  {
    // Arrange
    var culture = new CultureInfo(newCulture);

    // Act
    Languages.SetCulture(culture);

    // Assert
    Assert.Equal(culture.Name, Languages.Current);
    Assert.Equal(culture, CultureInfo.CurrentCulture);
    Assert.Equal(culture, CultureInfo.CurrentUICulture);
    Assert.Equal(culture, CultureInfo.DefaultThreadCurrentCulture);
    Assert.Equal(culture, CultureInfo.DefaultThreadCurrentUICulture);
  }
}
