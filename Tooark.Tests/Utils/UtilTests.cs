using System.Globalization;
using Tooark.Tests.Models;
using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class UtilTests
{
  [Fact]
  public void GetName_ReturnsCurrentLanguage()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Name = "English Name" },
      new() { LanguageCode = "pt-BR", Name = "Nome em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetName(list);

    // Assert
    Assert.Equal("Nome em Português", result);
  }

  [Fact]
  public void GetName_ReturnsDefaultLanguageWhenCurrentNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Name = "English Name" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

    // Act
    var result = Util.GetName(list);

    // Assert
    Assert.Equal("English Name", result);
  }

  [Fact]
  public void GetName_ReturnsFirstLanguageWhenCurrentAndDefaultNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "es-ES", Name = "Nombre Español" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

    // Act
    var result = Util.GetName(list);

    // Assert
    Assert.Equal("Nombre Español", result);
  }

  [Fact]
  public void GetName_ReturnsLanguageCodeParameter()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Name = "English Name" },
      new() { LanguageCode = "pt-BR", Name = "Nome em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetName(list, "en-US");

    // Assert
    Assert.Equal("English Name", result);
  }

  [Fact]
  public void GetName_ReturnsDefaultLanguageWhenLanguageCodeParameterNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Name = "English Name" },
      new() { LanguageCode = "pt-BR", Name = "Nome em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetName(list, "fr-FR");

    // Assert
    Assert.Equal("English Name", result);
  }

  [Fact]
  public void GetName_ReturnsEmptyStringWhenNoMatchFound()
  {
    // Arrange
    var list = new List<Language>();

    // Act
    var result = Util.GetName(list);

    // Assert
    Assert.Equal(string.Empty, result);
  }

  [Fact]
  public void GetTitle_ReturnsCurrentLanguage()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Title = "English Title" },
      new() { LanguageCode = "pt-BR", Title = "Título em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetTitle(list);

    // Assert
    Assert.Equal("Título em Português", result);
  }

  [Fact]
  public void GetTitle_ReturnsDefaultLanguageWhenCurrentNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Title = "English Title" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

    // Act
    var result = Util.GetTitle(list);

    // Assert
    Assert.Equal("English Title", result);
  }

  [Fact]
  public void GetTitle_ReturnsFirstLanguageWhenCurrentAndDefaultNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "es-ES", Title = "Título Español" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

    // Act
    var result = Util.GetTitle(list);

    // Assert
    Assert.Equal("Título Español", result);
  }

  [Fact]
  public void GetTitle_ReturnsLanguageCodeParameter()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Title = "English Title" },
      new() { LanguageCode = "pt-BR", Title = "Título em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetTitle(list, "en-US");

    // Assert
    Assert.Equal("English Title", result);
  }

  [Fact]
  public void GetTitle_ReturnsDefaultLanguageWhenLanguageCodeParameterNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Title = "English Title" },
      new() { LanguageCode = "pt-BR", Title = "Título em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetTitle(list, "fr-FR");

    // Assert
    Assert.Equal("English Title", result);
  }

  [Fact]
  public void GetTitle_ReturnsEmptyStringWhenNoMatchFound()
  {
    // Arrange
    var list = new List<Language>();

    // Act
    var result = Util.GetTitle(list);

    // Assert
    Assert.Equal(string.Empty, result);
  }

  [Fact]
  public void GetDescription_ReturnsCurrentLanguage()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Description = "English Description" },
      new() { LanguageCode = "pt-BR", Description = "Descrição em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetDescription(list);

    // Assert
    Assert.Equal("Descrição em Português", result);
  }

  [Fact]
  public void GetDescription_ReturnsDefaultLanguageWhenCurrentNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Description = "English Description" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

    // Act
    var result = Util.GetDescription(list);

    // Assert
    Assert.Equal("English Description", result);
  }

  [Fact]
  public void GetDescription_ReturnsFirstLanguageWhenCurrentAndDefaultNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "es-ES", Description = "Descripción Español" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

    // Act
    var result = Util.GetDescription(list);

    // Assert
    Assert.Equal("Descripción Español", result);
  }

  [Fact]
  public void GetDescription_ReturnsLanguageCodeParameter()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Description = "English Description" },
      new() { LanguageCode = "pt-BR", Description = "Descrição em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetDescription(list, "en-US");

    // Assert
    Assert.Equal("English Description", result);
  }

  [Fact]
  public void GetDescription_ReturnsDefaultLanguageWhenLanguageCodeParameterNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Description = "English Description" },
      new() { LanguageCode = "pt-BR", Description = "Descrição em Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetDescription(list, "fr-FR");

    // Assert
    Assert.Equal("English Description", result);
  }

  [Fact]
  public void GetDescription_ReturnsEmptyStringWhenNoMatchFound()
  {
    // Arrange
    var list = new List<Language>();

    // Act
    var result = Util.GetDescription(list);

    // Assert
    Assert.Equal(string.Empty, result);
  }

  [Fact]
  public void GetLanguageCode_ReturnsCurrentLanguage()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Name = "English" },
      new() { LanguageCode = "pt-BR", Name = "Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetLanguageCode(list, "Name");

    // Assert
    Assert.Equal("Português", result);
  }

  [Fact]
  public void GetLanguageCode_ReturnsDefaultLanguageWhenCurrentNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Name = "English" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

    // Act
    var result = Util.GetLanguageCode(list, "Name");

    // Assert
    Assert.Equal("English", result);
  }

  [Fact]
  public void GetLanguageCode_ReturnsFirstLanguageWhenCurrentAndDefaultNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "es-ES", Name = "Nombre Español" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

    // Act
    var result = Util.GetLanguageCode(list, "Name");

    // Assert
    Assert.Equal("Nombre Español", result);
  }

  [Fact]
  public void GetLanguageCode_ReturnsLanguageCodeParameter()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Name = "English" },
      new() { LanguageCode = "pt-BR", Name = "Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetLanguageCode(list, "Name", "en-US");

    // Assert
    Assert.Equal("English", result);
  }

  [Fact]
  public void GetLanguageCode_ReturnsDefaultLanguageWhenLanguageCodeParameterNotAvailable()
  {
    // Arrange
    var list = new List<Language>
    {
      new() { LanguageCode = "en-US", Name = "English" },
      new() { LanguageCode = "pt-BR", Name = "Português" }
    };

    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

    // Act
    var result = Util.GetLanguageCode(list, "Name", "fr-FR");

    // Assert
    Assert.Equal("English", result);
  }

  [Fact]
  public void GetLanguageCode_ReturnsEmptyStringWhenNoMatchFound()
  {
    // Arrange
    var list = new List<Language>();

    // Act
    var result = Util.GetLanguageCode(list, "Name");

    // Assert
    Assert.Equal(string.Empty, result);
  }

  [Fact]
  public void GetLanguageCode_ThrowsExceptionWhenLanguageCodePropertyDoesNotExist()
  {
    // Arrange
    var list = new List<LanguageOnlyName>
    {
      new() { Name = "English" }
    };

    // Act
    var exception = Assert.Throws<InvalidOperationException>(() => Util.GetLanguageCode(list, "Name"));

    // Assert
    Assert.Equal("PropertyNotExist;LanguageCode", exception.Message);
  }

  [Fact]
  public void GetLanguageCode_ThrowsExceptionWhenNamePropertyDoesNotExist()
  {
    // Arrange
    var list = new List<LanguageOnlyLanguageCode>
    {
      new() { LanguageCode = "en-US" }
    };

    // Act
    var exception = Assert.Throws<InvalidOperationException>(() => Util.GetLanguageCode(list, "Name"));

    // Assert
    Assert.Equal("PropertyNotExist;Name", exception.Message);
  }
}
