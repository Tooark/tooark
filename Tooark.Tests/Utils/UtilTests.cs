using System.Globalization;
using static Tooark.Utils.Util;

namespace Tooark.Tests.Utils;

public class LanguagesTests
{
  // Testa se o valor padrão da linguagem é "en-US"
  [Fact]
  public void Default_ShouldBe_enUS()
  { 
    // Assert
    Assert.Equal("en-US", Languages.Default);
  }

  // Testa se a propriedade Current reflete corretamente a cultura atual
  [Fact]
  public void Current_ShouldMatch_CurrentCulture()
  {
    // Act
    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
    
    // Assert
    Assert.Equal("pt-BR", Languages.Current);
  }
}
