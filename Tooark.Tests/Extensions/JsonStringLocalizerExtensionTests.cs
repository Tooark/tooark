using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Tooark.Extensions;
using Tooark.Utils;

namespace Tooark.Tests.Extensions;

public class JsonStringLocalizerExtensionTests
{
  private readonly Mock<IDistributedCache> _mockDistributedCache;
  private readonly JsonStringLocalizerExtension _localizer;

  // Construtor para inicializar as variáveis de teste
  public JsonStringLocalizerExtensionTests()
  {
    // Define a instância da linguagem como a implementação padrão
    Language.Instance = new Language.LanguageImplementation();

    // Define a cultura atual como "pt-BR"
    Language.SetCulture("pt-BR");
    
    // Define o mock do cache distribuído
    _mockDistributedCache = new Mock<IDistributedCache>();

    // Define o localizador de string JSON
    _localizer = new JsonStringLocalizerExtension(_mockDistributedCache.Object);
  }


  // Teste se this[string name] retorna uma string localizada utilizando apenas a key
  [Fact]
  public void LocalizedString_CurrentCulture_WithStringKeyOnly_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "a";
    string localizedValue = "Olá";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna uma string localizada utilizando key e parâmetro
  [Fact]
  public void LocalizedString_CurrentCulture_WithStringKeyAndParam_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "b;Tooark";
    string localizedValue = "Olá, Tooark";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna uma string localizada utilizando e múltiplos parâmetros
  [Fact]
  public void LocalizedString_CurrentCulture_WithStringKeyAndMultiParam_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "c;Package;Nuget;Tooark";
    string localizedValue = "Olá, Package Nuget Tooark";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna a chave buscada por não encontrar a key utilizando apenas a key
  [Fact]
  public void LocalizedString_CurrentCulture_WithStringKeyNotFound_ShouldReturnKey()
  {
    // Arrange
    string key = "custom";
    string expectedValue = key.Split(';')[0];

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(expectedValue, result.Value);
  }

  // Teste se this[string name] retorna a chave buscada por não encontrar a key utilizando key e parâmetro
  [Fact]
  public void LocalizedString_CurrentCulture_WithStringKeyNotFoundAndParam_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Tooark";
    string expectedValue = key.Split(';')[0];

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(expectedValue, result.Value);
  }

  // Teste se this[string name] retorna uma string localizada utilizando key e lista de parâmetro
  [Fact]
  public void LocalizedString_CurrentCulture_WithStringKeyAndListParam_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "b";
    string[] parameters = ["Tooark"];
    string localizedValue = "Olá, Tooark";

    // Act
    var result = _localizer[key, parameters];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna uma string localizada utilizando key e lista de múltiplos parâmetros
  [Fact]
  public void LocalizedString_CurrentCulture_WithStringKeyAndListMultiParam_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "c";
    string[] parameters = ["Package", "Nuget", "Tooark"];
    string localizedValue = "Olá, Package Nuget Tooark";

    // Act
    var result = _localizer[key, parameters];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para retornar todas as strings do arquivo da cultura atual de recursos
  [Fact]
  public void GetAllStrings_CurrentCulture_ShouldReturnAllStrings()
  {
    // Arrange
    var expectedName = "a";
    var expectedValue = "Olá";

    // Act
    var result = _localizer.GetAllStrings(false).ToList();

    // Assert
    Assert.Contains(result, r =>
      r.Name == expectedName &&
      r.Value == expectedValue);
  }

  // Teste para retornar todas as strings do arquivo de cultura padrão de recursos
  [Fact]
  public void GetAllStrings_DefaultCulture_ShouldReturnAllStrings()
  {
    // Arrange
    var expectedName = "z";
    var expectedValue = "Default";

    // Act
    var result = _localizer.GetAllStrings(true).ToList();

    // Assert
    Assert.Contains(result, r =>
      r.Name == expectedName &&
      r.Value == expectedValue);
  }

  // Teste se this[string name] retorna uma string localizada utilizando apenas a key da cultura padrão
  [Fact]
  public void LocalizedString_DefaultCulture_WithStringKeyOnly_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "z";
    string localizedValue = "Default";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna uma string localizada utilizando key e lista de parâmetro com quantidade de parâmetros diferente do esperado
  [Fact]
  public void LocalizedString_CurrentCulture_WithStringKeyAndWrongCountParam_ShouldReturnKey()
  {
    // Arrange
    string key = "c";
    string[] parameters = ["Tooark"];
    string expectedValue = "Olá, {0} {1} {2};Tooark";

    // Act
    var result = _localizer[key, parameters];

    // Assert
    Assert.Equal(expectedValue, result.Value);
  }

  // Teste se this[string name] com chave vazia ou nula
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  public void LocalizedString_CurrentCulture_WithKeyEmpty_ShouldReturnKey(string? key)
  {
    // Arrange & Act
    var result = _localizer[key!];

    // Assert
    Assert.Equal(string.Empty, result.Value);
  }

  // Teste se this[string name] com chave vazia ou nula
  [Fact]
  public void LocalizedString_AdditionalNotFound_ShouldReturnKey()
  {
    // Arrange
    Language.Instance = new Language.LanguageImplementation();
    Language.SetCulture("es-ES");
    var mockDistributedCache = new Mock<IDistributedCache>();
    var localizer = new JsonStringLocalizerExtension(mockDistributedCache.Object);
    string key = "custom";

    // Act
    var result = localizer[key];

    // Assert
    Assert.Equal(key, result.Value);
  }
}
