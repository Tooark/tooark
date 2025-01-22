using System.Globalization;
using Moq;
using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class LanguageTests
{
  // Testa se o valor padrão da linguagem é "en-US"
  [Fact]
  public void DefaultLanguage_ShouldBeEnUS()
  {
    // Arrange & Act
    var defaultLanguage = Language.Default;

    // Assert
    Assert.Equal("en-US", defaultLanguage);
  }

  // Testa se a propriedade Current reflete corretamente a cultura atual
  [Fact]
  public void CurrentLanguage_ShouldBeCurrentCulture()
  {
    // Arrange
    var mockCultureInfo = new Mock<CultureInfo>("en-US");
    mockCultureInfo.Setup(c => c.Name).Returns("en-US");
    Language.SetCulture(mockCultureInfo.Object);

    // Act
    var currentLanguage = Language.Current;

    // Assert
    Assert.Equal(mockCultureInfo.Object.Name, currentLanguage);
  }

  // Testa se a cultura atual é alterada corretamente
  [Theory]
  [InlineData("en-US")]
  [InlineData("es-ES")]
  [InlineData("jp-JP")]
  [InlineData("pt-BR")]
  [InlineData("pt-PT")]
  public void SetCulture_ShouldChangeCurrentCulture(string culture)
  {
    // Arrange
    var mockCultureInfo = new Mock<CultureInfo>(culture);
    mockCultureInfo.Setup(c => c.Name).Returns(culture);

    // Act
    Language.SetCulture(mockCultureInfo.Object);

    // Assert
    Assert.Equal(mockCultureInfo.Object.Name, Language.Current);
    Assert.Equal(mockCultureInfo.Object.Name, Language.CurrentCulture.Name);
    Assert.Equal(mockCultureInfo.Object.Name, CultureInfo.CurrentCulture.Name);
    Assert.Equal(mockCultureInfo.Object.Name, CultureInfo.CurrentUICulture.Name);
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
    var mockCultureInfo = new Mock<CultureInfo>(newCulture);
    mockCultureInfo.Setup(c => c.Name).Returns(newCulture);

    // Act
    Language.SetCulture(mockCultureInfo.Object);

    // Assert
    Assert.Equal(mockCultureInfo.Object.Name, Language.Current);
    Assert.Equal(mockCultureInfo.Object, Language.CurrentCulture);
    Assert.Equal(mockCultureInfo.Object, CultureInfo.CurrentCulture);
    Assert.Equal(mockCultureInfo.Object, CultureInfo.CurrentUICulture);
  }

  // Testa se a cultura atual é alterada corretamente
  [Theory]
  [InlineData("en")]
  [InlineData("es")]
  [InlineData("jp")]
  [InlineData("pt")]
  [InlineData("ptBR")]
  public void SetCulture_ShouldDefaultCulture_WhenCultureInvalid(string culture)
  {
    // Arrange & Act
    Language.SetCulture(culture);

    // Assert
    Assert.Equal(Language.Default, Language.Current);
    Assert.Equal(Language.Default, Language.CurrentCulture.Name);
    Assert.Equal(Language.Default, CultureInfo.CurrentCulture.Name);
    Assert.Equal(Language.Default, CultureInfo.CurrentUICulture.Name);
  }
}
