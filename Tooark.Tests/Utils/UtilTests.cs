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
    // Act
    Languages.SetCulture(newCulture);

    // Assert
    Assert.Equal(newCulture, CultureInfo.CurrentCulture.Name);
    Assert.Equal(newCulture, Languages.Current);
  }
}
