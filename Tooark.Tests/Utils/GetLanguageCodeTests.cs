using Tooark.Tests.Moq.Model.Language;
using static Tooark.Utils.Util;

namespace Tooark.Tests.Utils;

public class GetLanguageCodeTests
{
  // Lista de linguagens para testes
  private readonly List<MLanguage> _testItems =
  [
    new()
    {
      LanguageCode = "en-US",
      Name = "Name in English",
      Title = "Title in English",
      Description = "Description in English"
    },
    new()
    {
      LanguageCode = "es-ES",
      Name = "Nombre en Español",
      Title = "Título en Español",
      Description = "Descripción en Español"
    },
    new()
    {
      LanguageCode = "fr-FR",
      Name = "Nom en Français",
      Title = "Titre en Français",
      Description = "Description en Français"
    },
    new()
    {
      LanguageCode = "jp-JP",
      Name = "日本語での名前",
      Title = "日本語タイトル",
      Description = "日本語での説明"
    },
    new()
    {
      LanguageCode = "pt-BR",
      Name = "Nome em Português",
      Title = "Titulo em Português",
      Description = "Descrição em Português"
     }
  ];

  // Testa se o método GetName retorna o nome da linguagem correspondente à linguagem do parâmetro
  [Theory]
  [InlineData("en-US", "Name in English")]
  [InlineData("es-ES", "Nombre en Español")]
  [InlineData("fr-FR", "Nom en Français")]
  [InlineData("jp-JP", "日本語での名前")]
  [InlineData("pt-BR", "Nome em Português")]
  public void GetName_ShouldReturnName_BasedOnLanguageCode(string languageCode, string expectedName)
  {
    // Act
    var result = GetName(_testItems, languageCode);

    // Assert
    Assert.Equal(expectedName, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetName retorna o nome da linguagem correspondente à cultura definida
  [Theory]
  [InlineData("en-US", "Name in English")]
  [InlineData("es-ES", "Nombre en Español")]
  [InlineData("fr-FR", "Nom en Français")]
  [InlineData("jp-JP", "日本語での名前")]
  [InlineData("pt-BR", "Nome em Português")]
  public void GetName_ShouldReturnName_SetCurrentLanguageCode(string languageCode, string expectedName)
  {
    // Arrange
    Languages.SetCulture(languageCode);

    // Act
    var result = GetName(_testItems);

    // Assert
    Assert.Equal(expectedName, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetTitle retorna o titulo da linguagem correspondente à linguagem do parâmetro
  [Theory]
  [InlineData("en-US", "Title in English")]
  [InlineData("es-ES", "Título en Español")]
  [InlineData("fr-FR", "Titre en Français")]
  [InlineData("jp-JP", "日本語タイトル")]
  [InlineData("pt-BR", "Titulo em Português")]
  public void GetTitle_ShouldReturnTitle_BasedOnLanguageCode(string languageCode, string expectedName)
  {
    // Act
    var result = GetTitle(_testItems, languageCode);

    // Assert
    Assert.Equal(expectedName, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetTitle retorna o titulo da linguagem correspondente à cultura definida
  [Theory]
  [InlineData("en-US", "Title in English")]
  [InlineData("es-ES", "Título en Español")]
  [InlineData("fr-FR", "Titre en Français")]
  [InlineData("jp-JP", "日本語タイトル")]
  [InlineData("pt-BR", "Titulo em Português")]
  public void GetTitle_ShouldReturnTitle_SetCurrentLanguageCode(string languageCode, string expectedName)
  {
    // Arrange
    Languages.SetCulture(languageCode);

    // Act
    var result = GetTitle(_testItems);

    // Assert
    Assert.Equal(expectedName, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetDescription retorna o descrição da linguagem correspondente à linguagem do parâmetro
  [Theory]
  [InlineData("en-US", "Description in English")]
  [InlineData("es-ES", "Descripción en Español")]
  [InlineData("fr-FR", "Description en Français")]
  [InlineData("jp-JP", "日本語での説明")]
  [InlineData("pt-BR", "Descrição em Português")]
  public void GetDescription_ShouldReturnDescription_BasedOnLanguageCode(string languageCode, string expectedName)
  {
    // Act
    var result = GetDescription(_testItems, languageCode);

    // Assert
    Assert.Equal(expectedName, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetDescription retorna o descrição da linguagem correspondente à cultura definida
  [Theory]
  [InlineData("en-US", "Description in English")]
  [InlineData("es-ES", "Descripción en Español")]
  [InlineData("fr-FR", "Description en Français")]
  [InlineData("jp-JP", "日本語での説明")]
  [InlineData("pt-BR", "Descrição em Português")]
  public void GetDescription_ShouldReturnDescription_SetCurrentLanguageCode(string languageCode, string expectedName)
  {
    // Arrange
    Languages.SetCulture(languageCode);

    // Act
    var result = GetDescription(_testItems);

    // Assert
    Assert.Equal(expectedName, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetName retorna o nome da linguagem padrão (en-US) linguagem do parâmetro não está na lista
  [Fact]
  public void GetName_ShouldReturnNameDefault_WhenParameterNotInList()
  {
    // Arrange
    Languages.SetCulture("de-DE");

    // Act
    var result = GetName(_testItems);

    // Assert
    Assert.Equal("Name in English", result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetName retorna o nome da linguagem padrão (en-US) linguagem do parâmetro não está na lista
  [Fact]
  public void GetName_ShouldReturnNameDefault_WhenCultureInList()
  {
    // Act
    var result = GetName(_testItems, "de-DE");

    // Assert
    Assert.Equal("Name in English", result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetName retorna uma string vazia quando a lista de linguagens está vazia
  [Fact]
  public void GetName_ReturnsEmptyStringWhenListIsEmpty()
  {
    // Arrange
    var list = new List<MLanguage>();

    // Act
    var result = GetName(list);

    // Assert
    Assert.Equal(string.Empty, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetTitle retorna o nome da linguagem padrão (en-US) linguagem do parâmetro não está na lista
  [Fact]
  public void GetTitle_ShouldReturnTitleDefault_WhenParameterNotInList()
  {
    // Arrange
    Languages.SetCulture("de-DE");

    // Act
    var result = GetTitle(_testItems);

    // Assert
    Assert.Equal("Title in English", result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetTitle retorna o nome da linguagem padrão (en-US) linguagem do parâmetro não está na lista
  [Fact]
  public void GetTitle_ShouldReturnTitleDefault_WhenCultureInList()
  {
    // Arrange & Act
    var result = GetTitle(_testItems, "de-DE");

    // Assert
    Assert.Equal("Title in English", result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetTitle retorna uma string vazia quando a lista de linguagens está vazia
  [Fact]
  public void GetTitle_ReturnsEmptyStringWhenListIsEmpty()
  {
    // Arrange
    var list = new List<MLanguage>();

    // Act
    var result = GetTitle(list);

    // Assert
    Assert.Equal(string.Empty, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetDescription retorna o nome da linguagem padrão (en-US) linguagem do parâmetro não está na lista
  [Fact]
  public void GetDescription_ShouldReturnDescriptionDefault_WhenParameterNotInList()
  {
    // Arrange
    Languages.SetCulture("de-DE");

    // Act
    var result = GetDescription(_testItems);

    // Assert
    Assert.Equal("Description in English", result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetDescription retorna o nome da linguagem padrão (en-US) linguagem do parâmetro não está na lista
  [Fact]
  public void GetDescription_ShouldReturnDescriptionDefault_WhenCultureInList()
  {
    // Act
    var result = GetDescription(_testItems, "de-DE");

    // Assert
    Assert.Equal("Description in English", result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetDescription retorna uma string vazia quando a lista de linguagens está vazia
  [Fact]
  public void GetDescription_ReturnsEmptyStringWhenListIsEmpty()
  {
    // Arrange
    var list = new List<MLanguage>();

    // Act
    var result = GetDescription(list);

    // Assert
    Assert.Equal(string.Empty, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetLanguageCode retorna a propriedade correspondente à linguagem do parâmetro
  [Theory]
  [InlineData("en-US", "Name in English")]
  [InlineData("es-ES", "Nombre en Español")]
  [InlineData("fr-FR", "Nom en Français")]
  [InlineData("jp-JP", "日本語での名前")]
  [InlineData("pt-BR", "Nome em Português")]
  public void GetLanguageCode_ShouldReturnProperty_BasedOnLanguageCode(string languageCode, string expectedName)
  {
    // Act
    var languageCodeResult = GetLanguageCode(_testItems, "Name", languageCode);

    // Assert
    Assert.Equal(expectedName, languageCodeResult);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetLanguageCode retorna a propriedade correspondente à cultura definida
  [Theory]
  [InlineData("en-US", "Name in English")]
  [InlineData("es-ES", "Nombre en Español")]
  [InlineData("fr-FR", "Nom en Français")]
  [InlineData("jp-JP", "日本語での名前")]
  [InlineData("pt-BR", "Nome em Português")]
  public void GetLanguageCode_ShouldReturnProperty_SetCurrentLanguageCode(string languageCode, string expectedName)
  {
    // Arrange
    Languages.SetCulture(languageCode);

    // Act
    var languageCodeResult = GetLanguageCode(_testItems, "Name");

    // Assert
    Assert.Equal(expectedName, languageCodeResult);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }


  // Testa se o método GetLanguageCode retorna o nome da linguagem padrão (en-US) linguagem do parâmetro não está na lista
  [Fact]
  public void GetLanguageCode_ShouldReturnNameDefault_WhenParameterNotInList()
  {
    // Arrange
    Languages.SetCulture("de-DE");

    // Act
    var result = GetLanguageCode(_testItems, "Name");

    // Assert
    Assert.Equal("Name in English", result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetLanguageCode retorna o nome da linguagem padrão (en-US) linguagem do parâmetro não está na lista
  [Fact]
  public void GetLanguageCode_ShouldReturnNameDefault_WhenCultureInList()
  {
    // Act
    var result = GetLanguageCode(_testItems, "Name", "de-DE");

    // Assert
    Assert.Equal("Name in English", result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetLanguageCode retorna uma string vazia quando a lista de linguagens está vazia
  [Fact]
  public void GetLanguageCode_ReturnsEmptyStringWhenListIsEmpty()
  {
    // Arrange
    var list = new List<MLanguage>();

    // Act
    var result = GetLanguageCode(list, "Name");

    // Assert
    Assert.Equal(string.Empty, result);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetLanguageCode lança uma exceção quando a propriedade LanguageCode não existe na lista de linguagens
  [Fact]
  public void GetLanguageCode_ThrowsExceptionWhenLanguageCodePropertyDoesNotExist()
  {
    // Arrange
    List<MLanguageOnlyName> list =
    [
      new()
        {
          Name = "Name in English"
        }
    ];

    // Act
    var exception = Assert.Throws<InvalidOperationException>(() => GetLanguageCode(list, "Name"));

    // Assert
    Assert.Equal("PropertyNotExist;LanguageCode", exception.Message);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }

  // Testa se o método GetLanguageCode lança uma exceção quando a propriedade Name não existe na lista de linguagens
  [Fact]
  public void GetLanguageCode_ThrowsExceptionWhenNamePropertyDoesNotExist()
  {
    // Act
    var exception = Assert.Throws<InvalidOperationException>(() => GetLanguageCode(_testItems, "Person"));

    // Assert
    Assert.Equal("PropertyNotExist;Person", exception.Message);

    // Cleanup
    Languages.SetCulture(Languages.Default);
  }
}
