using System.Globalization;
using Tooark.Tests.Moq.Model.Language;
using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class GetLanguageCodeTests
{
  // Testa se o método GetName retorna o nome da linguagem correspondente à cultura atual do sistema
  [Fact]
  public void GetName_ReturnsNameMatchingCurrentCulture()
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

  // Testa se o método GetName retorna o nome da linguagem padrão (en-US) quando a cultura atual do sistema não está na lista
  [Fact]
  public void GetName_ReturnsDefaultNameWhenCurrentCultureNotInList()
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

  // Testa se o método GetName retorna o nome da primeira linguagem da lista quando a cultura atual do sistema e a linguagem padrão não estão na lista
  [Fact]
  public void GetName_ReturnsFirstNameWhenCurrentCultureAndDefaultNotInList()
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

  // Testa se o método GetName retorna o nome da linguagem correspondente ao código passado como parâmetro
  [Fact]
  public void GetName_ReturnsNameMatchingCodeParameter()
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

  // Testa se o método GetName retorna o nome da linguagem padrão (en-US) quando o código passado como parâmetro não está na lista
  [Fact]
  public void GetName_ReturnsDefaultNameWhenCodeParameterNotInList()
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

  // Testa se o método GetName retorna uma string vazia quando a lista de linguagens está vazia
  [Fact]
  public void GetName_ReturnsEmptyStringWhenListIsEmpty()
  {
    // Arrange
    var list = new List<Language>();

    // Act
    var result = Util.GetName(list);

    // Assert
    Assert.Equal(string.Empty, result);
  }

  // Testa se o método GetTitle retorna o titulo da linguagem correspondente à cultura atual do sistema
  [Fact]
  public void GetTitle_ReturnsTitleMatchingCurrentCulture()
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

  // Testa se o método GetTitle retorna o titulo da linguagem padrão (en-US) quando a cultura atual do sistema não está na lista
  [Fact]
  public void GetTitle_ReturnsDefaultTitleWhenCurrentCultureNotInList()
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

  // Testa se o método GetTitle retorna o titulo da primeira linguagem da lista quando a cultura atual do sistema e a linguagem padrão não estão na lista
  [Fact]
  public void GetTitle_ReturnsFirstTitleWhenCurrentCultureAndDefaultNotInList()
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

  // Testa se o método GetTitle retorna o titulo da linguagem correspondente ao código passado como parâmetro
  [Fact]
  public void GetTitle_ReturnsTitleMatchingCodeParameter()
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

  // Testa se o método GetTitle retorna o titulo da linguagem padrão (en-US) quando o código passado como parâmetro não está na lista
  [Fact]
  public void GetTitle_ReturnsDefaultTitleWhenCodeParameterNotInList()
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

  // Testa se o método GetTitle retorna uma string vazia quando a lista de linguagens está vazia
  [Fact]
  public void GetTitle_ReturnsEmptyStringWhenListIsEmpty()
  {
    // Arrange
    var list = new List<Language>();

    // Act
    var result = Util.GetTitle(list);

    // Assert
    Assert.Equal(string.Empty, result);
  }

  // Testa se o método GetDescription retorna a descrição da linguagem correspondente à cultura atual do sistema
  [Fact]
  public void GetDescription_ReturnsDescriptionMatchingCurrentCulture()
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

  // Testa se o método GetDescription retorna a descrição da linguagem padrão (en-US) quando a cultura atual do sistema não está na lista
  [Fact]
  public void GetDescription_ReturnsDefaultDescriptionWhenCurrentCultureNotInList()
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

  // Testa se o método GetDescription retorna a descrição da primeira linguagem da lista quando a cultura atual do sistema e a linguagem padrão não estão na lista
  [Fact]
  public void GetDescription_ReturnsFirstDescriptionWhenCurrentCultureAndDefaultNotInList()
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

  // Testa se o método GetDescription retorna a descrição da linguagem correspondente ao código passado como parâmetro
  [Fact]
  public void GetDescription_ReturnsDescriptionMatchingCodeParameter()
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

  // Testa se o método GetDescription retorna a descrição da linguagem padrão (en-US) quando o código passado como parâmetro não está na lista
  [Fact]
  public void GetDescription_ReturnsDefaultDescriptionWhenCodeParameterNotInList()
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

  // Testa se o método GetDescription retorna uma string vazia quando a lista de linguagens está vazia
  [Fact]
  public void GetDescription_ReturnsEmptyStringWhenListIsEmpty()
  {
    // Arrange
    var list = new List<Language>();

    // Act
    var result = Util.GetDescription(list);

    // Assert
    Assert.Equal(string.Empty, result);
  }

  // Testa se o método GetLanguageCode retorna o código da linguagem correspondente à cultura atual do sistema
  [Fact]
  public void GetLanguageCode_ReturnsCodeMatchingCurrentCulture()
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

  // Testa se o método GetLanguageCode retorna o código da linguagem padrão (en-US) quando a cultura atual do sistema não está na lista
  [Fact]
  public void GetLanguageCode_ReturnsDefaultCodeWhenCurrentCultureNotInList()
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

  // Testa se o método GetLanguageCode retorna o código da primeira linguagem da lista quando a cultura atual do sistema e a linguagem padrão não estão na lista
  [Fact]
  public void GetLanguageCode_ReturnsFirstCodeWhenCurrentCultureAndDefaultNotInList()
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

  // Testa se o método GetLanguageCode retorna o código da linguagem correspondente ao código passado como parâmetro
  [Fact]
  public void GetLanguageCode_ReturnsCodeMatchingCodeParameter()
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

  // Testa se o método GetLanguageCode retorna o código da linguagem padrão (en-US) quando o código passado como parâmetro não está na lista
  [Fact]
  public void GetLanguageCode_ReturnsDefaultCodeWhenCodeParameterNotInList()
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

  // Testa se o método GetLanguageCode retorna uma string vazia quando a lista de linguagens está vazia
  [Fact]
  public void GetLanguageCode_ReturnsEmptyStringWhenListIsEmpty()
  {
    // Arrange
    var list = new List<Language>();

    // Act
    var result = Util.GetLanguageCode(list, "Name");

    // Assert
    Assert.Equal(string.Empty, result);
  }

  // Testa se o método GetLanguageCode lança uma exceção quando a propriedade LanguageCode não existe na lista de linguagens
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

  // Testa se o método GetLanguageCode lança uma exceção quando a propriedade Name não existe na lista de linguagens
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
