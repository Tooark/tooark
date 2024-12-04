using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Tooark.Extensions;
using static Tooark.Utils.Util;

namespace Tooark.Tests.Extensions;

public class JsonStringLocalizerExtensionTest
{
  private readonly Mock<IDistributedCache> _mockDistributedCache;
  private readonly JsonStringLocalizerExtension _localizer;
  private readonly JsonStringLocalizerExtension _additionalLocalizer;
  private readonly string _culture;
  private readonly string _filePathDefault;
  private readonly string _filePath;

  public JsonStringLocalizerExtensionTest()
  {
    _mockDistributedCache = new Mock<IDistributedCache>();
    _culture = "en-US";
    _filePathDefault = $"Resources/{_culture}.default.json";
    _filePath = $"Resources/{_culture}.json";

    File.WriteAllText(_filePathDefault, "{\"hello\": \"Hello\", \"param\": \"Hello, {0}!\", \"multiParam\": \"Hello, {0} {1} {2}!\"}");
    File.WriteAllText(_filePath, "{\"world\": \"World\", \"param\": \"World, {0}!\", \"multiParam\": \"World, {0} {1} {2}!\"}");
    Languages.SetCulture(_culture);

    _localizer = new JsonStringLocalizerExtension(_mockDistributedCache.Object, []);
    _additionalLocalizer = new JsonStringLocalizerExtension(_mockDistributedCache.Object, new() { { _culture, _filePath } });
  }

  // Teste se this[string name] retorna uma string localizada utilizando apenas a key e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithKeyOnly_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "hello";
    string localizedValue = "Hello";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste se this[string name] retorna uma string localizada na cultura padrão utilizando apenas a key, cultura não existente e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithKeyOnly_CultureNotExist_ShouldReturnLocalizedStringInCultureDefault()
  {
    // Arrange
    string key = "hello";
    string localizedValue = "Hello";
    Languages.SetCulture("pt");

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste se this[string name] retorna a chave buscada por não encontrar a key
  [Fact]
  public void DefaultResource_Indexer_WithKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(key, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithSingleParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "param;Tooark";
    string localizedValue = "Hello, Tooark!";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro, cultura não existente e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithSingleParameter_CultureNotExist_ShouldReturnLocalizedStringInCultureDefault()
  {
    // Arrange
    string key = "param;Tooark";
    string localizedValue = "Hello, Tooark!";
    Languages.SetCulture("pt");

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro, cultura não existente e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithSingleParameterAndKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Tooark";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(key, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithMultiParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "multiParam;Package;Nuget;Tooark";
    string localizedValue = "Hello, Package Nuget Tooark!";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros, cultura não existente e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithMultiParameter_CultureNotExist_ShouldReturnLocalizedStringInCultureDefault()
  {
    // Arrange
    string key = "multiParam;Package;Nuget;Tooark";
    string localizedValue = "Hello, Package Nuget Tooark!";
    Languages.SetCulture("pt");

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros, cultura não existente e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithMultiParameterAndKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Package;Nuget;Tooark";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(key, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumento e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithSingleArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "param";
    string argument = "Tooark";
    string localizedValue = "Hello, Tooark!";

    // Act
    var result = _localizer[key, argument];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumentos e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithMultiArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "multiParam";
    string[] arguments = ["Package", "Nuget", "Tooark"];
    string localizedValue = "Hello, Package Nuget Tooark!";

    // Act
    var result = _localizer[key, arguments];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste se this[string name] retorna uma string localizada utilizando apenas a key e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithKeyOnly_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "world";
    string localizedValue = "World";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste se this[string name] retorna uma string localizada na cultura padrão utilizando apenas a key, cultura não existente e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithKeyOnly_CultureNotExist_ShouldReturnLocalizedStringInCultureDefault()
  {
    // Arrange
    string key = "world";
    string localizedValue = "World";
    Languages.SetCulture("pt");

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste se this[string name] retorna a chave buscada por não encontrar a adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithSingleParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "param;Tooark";
    string localizedValue = "World, Tooark!";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro, cultura não existente e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithSingleParameter_CultureNotExist_ShouldReturnLocalizedStringInCultureDefault()
  {
    // Arrange
    string key = "param;Tooark";
    string localizedValue = "World, Tooark!";
    Languages.SetCulture("pt");

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro, cultura não existente e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithSingleParameterAndKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Tooark";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithMultiParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "multiParam;Package;Nuget;Tooark";
    string localizedValue = "World, Package Nuget Tooark!";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros, cultura não existente e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithMultiParameter_CultureNotExist_ShouldReturnLocalizedStringInCultureDefault()
  {
    // Arrange
    string key = "multiParam;Package;Nuget;Tooark";
    string localizedValue = "World, Package Nuget Tooark!";
    Languages.SetCulture("pt");

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros, cultura não existente e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithMultiParameterAndKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Package;Nuget;Tooark";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumento e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithSingleArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "param";
    string argument = "Tooark";
    string localizedValue = "World, Tooark!";

    // Act
    var result = _additionalLocalizer[key, argument];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumentos e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithMultiArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "multiParam";
    string[] arguments = ["Package", "Nuget", "Tooark"];
    string localizedValue = "World, Package Nuget Tooark!";

    // Act
    var result = _additionalLocalizer[key, arguments];

    // Assert
    Assert.Equal(localizedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se carregou todos arquivos e se as duas últimas strings são as esperadas
  [Fact]
  public void DefaultResource_GetAllStrings_ShouldReturnAllLocalizedStrings()
  {
    // Arrange & Act
    var result = _localizer.GetAllStrings(false);
    var lastTwoItems = result.TakeLast(2).ToList();

    // Assert
    Assert.True(result.Any());
    Assert.Collection(lastTwoItems,
      item => Assert.Equal("Hello, {0}!", item.Value),
      item => Assert.Equal("Hello, {0} {1} {2}!", item.Value));

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se carregou todos arquivos e se as duas últimas strings são as esperadas
  [Fact]
  public void AdditionalResource_GetAllStrings_ShouldReturnAllLocalizedStrings()
  {
    // Arrange & Act
    var result = _additionalLocalizer.GetAllStrings(false);
    var lastTwoItems = result.TakeLast(2).ToList();

    // Assert
    Assert.True(result.Any());
    Assert.Collection(lastTwoItems,
      item => Assert.Equal("World, {0} {1} {2}!", item.Value),
      item => Assert.Equal("World", item.Value));

    // Cleanup
    File.Delete(_filePath);
  }
}
