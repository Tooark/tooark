using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Moq;
using Tooark.Extensions;
using Tooark.Factories;
using Tooark.Tests.Moq.Util;
using static Tooark.Utils.Util;

namespace Tooark.Tests.Extensions;

public class JsonStringLocalizerExtensionTests
{
  private readonly Mock<IDistributedCache> _mockDistributedCache;
  private readonly JsonStringLocalizerExtension _localizer;
  private readonly JsonStringLocalizerExtension _additionalLocalizer;
  private readonly IStringLocalizer _stringLocalizer;
  private readonly IStringLocalizer _stringAdditionalLocalizer;
  private readonly string _culture;
  private readonly string _filePathDefault;
  private readonly string _filePath;

  public JsonStringLocalizerExtensionTests()
  {
    _mockDistributedCache = new Mock<IDistributedCache>();
    _culture = Languages.Current;
    _filePathDefault = $"Resources/{_culture}.default.json";
    _filePath = $"Resources/{_culture}.json";

    var defaultResourceContent = "{\"a\": \"Hello\", \"b\": \"Teste\", \"c\": \"Hello, {0}!\", \"d\": \"Hello, {0} {1} {2}!\"}";
    var additionalResourceContent = "{\"j\": \"World\", \"k\": \"World, {0}!\", \"l\": \"World, {0} {1} {2}!\"}";

    // Cria os arquivos de recursos
    if (!File.Exists(_filePathDefault))
    {
      File.WriteAllText(_filePathDefault, defaultResourceContent);
    }

    // Cria os arquivos de recursos
    if (!File.Exists(_filePath))
    {
      File.WriteAllText(_filePath, additionalResourceContent);
    }

    var mockFileProvider = new Mock<IFileProvider>();
    mockFileProvider.Setup(p => p.GetFileInfo(_filePathDefault)).Returns(new MockFileInfo(defaultResourceContent));
    mockFileProvider.Setup(p => p.GetFileInfo(_filePath)).Returns(new MockFileInfo(additionalResourceContent));

    var defaultResourceStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(defaultResourceContent));
    var additionalResourceStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(additionalResourceContent));

    _localizer = new JsonStringLocalizerExtension(_mockDistributedCache.Object, null, defaultResourceStream);
    _additionalLocalizer = new JsonStringLocalizerExtension(_mockDistributedCache.Object, new() { { _culture, _filePath } }, additionalResourceStream);

    var factory = new JsonStringLocalizerFactory(_mockDistributedCache.Object, [], defaultResourceStream);
    var factoryAdditional = new JsonStringLocalizerFactory(_mockDistributedCache.Object, new() { { _culture, _filePath } }, additionalResourceStream);

    _stringLocalizer = factory.Create(typeof(JsonStringLocalizerExtensionTests));
    _stringAdditionalLocalizer = factoryAdditional.Create(typeof(JsonStringLocalizerExtensionTests));
  }

  // Teste se this[string name] retorna uma string localizada utilizando apenas a key e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithKeyOnly_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "a";
    string localizedValue = "Hello";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
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
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithSingleParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "c;Tooark";
    string localizedValue = "Hello, Tooark!";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
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
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithMultiParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "d;Package;Nuget;Tooark";
    string localizedValue = "Hello, Package Nuget Tooark!";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
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
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumento e com o replace dos valores dos parâmetros
  [Fact]
  public void DefaultResource_Indexer_WithSingleParameterAndReplace_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "c;b";
    string localizedValue = "Hello, Teste!";

    // Act
    var result = _localizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumento e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithSingleArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "c";
    string argument = "Tooark";
    string localizedValue = "Hello, Tooark!";

    // Act
    var result = _localizer[key, argument];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumentos e os resources padrões
  [Fact]
  public void DefaultResource_Indexer_WithMultiArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "d";
    string[] arguments = ["Package", "Nuget", "Tooark"];
    string localizedValue = "Hello, Package Nuget Tooark!";

    // Act
    var result = _localizer[key, arguments];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna uma string localizada utilizando apenas a key e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithKeyOnly_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "j";
    string localizedValue = "World";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
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
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithSingleParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "k;Tooark";
    string localizedValue = "World, Tooark!";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
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
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithMultiParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "l;Package;Nuget;Tooark";
    string localizedValue = "World, Package Nuget Tooark!";

    // Act
    var result = _additionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
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
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumento e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithSingleArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "k";
    string argument = "Tooark";
    string localizedValue = "World, Tooark!";

    // Act
    var result = _additionalLocalizer[key, argument];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumentos e os resources adicionais
  [Fact]
  public void AdditionalResource_Indexer_WithMultiArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "l";
    string[] arguments = ["Package", "Nuget", "Tooark"];
    string localizedValue = "World, Package Nuget Tooark!";

    // Act
    var result = _additionalLocalizer[key, arguments];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se carregou todos arquivos e se as duas últimas strings são as esperadas
  [Fact]
  public void DefaultResource_GetAllStrings_ShouldReturnAllLocalizedStrings()
  {
    // Arrange & Act
    var result = _localizer.GetAllStrings(true);
    var lastTwoItems = result.TakeLast(2).ToList();

    // Assert
    Assert.True(result.Any());
    Assert.Collection(lastTwoItems,
      item => Assert.Equal("Hello, {0}!", item.Value),
      item => Assert.Equal("Hello, {0} {1} {2}!", item.Value));
  }

  // Teste para verificar se carregou todos arquivos e se as duas últimas strings são as esperadas
  [Fact]
  public void AdditionalResource_GetAllStrings_ShouldReturnAllLocalizedStrings()
  {
    // Arrange & Act
    var result = _additionalLocalizer.GetAllStrings(true);
    var lastTwoItems = result.TakeLast(2).ToList();

    // Assert
    Assert.True(result.Any());
    Assert.Collection(lastTwoItems,
      item => Assert.Equal("World, {0}!", item.Value),
      item => Assert.Equal("World, {0} {1} {2}!", item.Value));
  }

  // Teste se this[string name] retorna uma string localizada utilizando apenas a key e os resources padrões
  [Fact]
  public void StringDefaultResource_Indexer_WithKeyOnly_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "a";
    string localizedValue = "Hello";

    // Act
    var result = _stringLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna a chave buscada por não encontrar a key
  [Fact]
  public void StringDefaultResource_Indexer_WithKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom";

    // Act
    var result = _stringLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro e os resources padrões
  [Fact]
  public void StringDefaultResource_Indexer_WithSingleParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "c;Tooark";
    string localizedValue = "Hello, Tooark!";

    // Act
    var result = _stringLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro, cultura não existente e os resources padrões
  [Fact]
  public void StringDefaultResource_Indexer_WithSingleParameterAndKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Tooark";

    // Act
    var result = _stringLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros e os resources padrões
  [Fact]
  public void StringDefaultResource_Indexer_WithMultiParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "d;Package;Nuget;Tooark";
    string localizedValue = "Hello, Package Nuget Tooark!";

    // Act
    var result = _stringLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros, cultura não existente e os resources padrões
  [Fact]
  public void StringDefaultResource_Indexer_WithMultiParameterAndKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Package;Nuget;Tooark";

    // Act
    var result = _stringLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumento e os resources padrões
  [Fact]
  public void StringDefaultResource_Indexer_WithSingleArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "c";
    string argument = "Tooark";
    string localizedValue = "Hello, Tooark!";

    // Act
    var result = _stringLocalizer[key, argument];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumentos e os resources padrões
  [Fact]
  public void StringDefaultResource_Indexer_WithMultiArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "d";
    string[] arguments = ["Package", "Nuget", "Tooark"];
    string localizedValue = "Hello, Package Nuget Tooark!";

    // Act
    var result = _stringLocalizer[key, arguments];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna uma string localizada utilizando apenas a key e os resources adicionais
  [Fact]
  public void StringAdditionalResource_Indexer_WithKeyOnly_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "j";
    string localizedValue = "World";

    // Act
    var result = _stringAdditionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste se this[string name] retorna a chave buscada por não encontrar a adicionais
  [Fact]
  public void StringAdditionalResource_Indexer_WithKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom";

    // Act
    var result = _stringAdditionalLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro e os resources adicionais
  [Fact]
  public void StringAdditionalResource_Indexer_WithSingleParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "k;Tooark";
    string localizedValue = "World, Tooark!";

    // Act
    var result = _stringAdditionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro, cultura não existente e os resources adicionais
  [Fact]
  public void StringAdditionalResource_Indexer_WithSingleParameterAndKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Tooark";

    // Act
    var result = _stringAdditionalLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros e os resources adicionais
  [Fact]
  public void StringAdditionalResource_Indexer_WithMultiParameter_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "l;Package;Nuget;Tooark";
    string localizedValue = "World, Package Nuget Tooark!";

    // Act
    var result = _stringAdditionalLocalizer[key];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetros, cultura não existente e os resources adicionais
  [Fact]
  public void StringAdditionalResource_Indexer_WithMultiParameterAndKeyNotExist_ShouldReturnKey()
  {
    // Arrange
    string key = "custom;Package;Nuget;Tooark";

    // Act
    var result = _stringAdditionalLocalizer[key];

    // Assert
    Assert.Equal(key, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumento e os resources adicionais
  [Fact]
  public void StringAdditionalResource_Indexer_WithSingleArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "k";
    string argument = "Tooark";
    string localizedValue = "World, Tooark!";

    // Act
    var result = _stringAdditionalLocalizer[key, argument];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada com argumentos e os resources adicionais
  [Fact]
  public void StringAdditionalResource_Indexer_WithMultiArgument_ShouldReturnLocalizedString()
  {
    // Arrange
    string key = "l";
    string[] arguments = ["Package", "Nuget", "Tooark"];
    string localizedValue = "World, Package Nuget Tooark!";

    // Act
    var result = _stringAdditionalLocalizer[key, arguments];

    // Assert
    Assert.Equal(localizedValue, result.Value);
  }

  // Teste para verificar se carregou todos arquivos e se as duas últimas strings são as esperadas
  [Fact]
  public void StringDefaultResource_GetAllStrings_ShouldReturnAllLocalizedStrings()
  {
    // Arrange & Act
    var result = _stringLocalizer.GetAllStrings(true);
    var lastTwoItems = result.TakeLast(2).ToList();

    // Assert
    Assert.True(result.Any());
    Assert.Collection(lastTwoItems,
      item => Assert.Equal("Hello, {0}!", item.Value),
      item => Assert.Equal("Hello, {0} {1} {2}!", item.Value));
  }

  // Teste para verificar se carregou todos arquivos e se as duas últimas strings são as esperadas
  [Fact]
  public void StringAdditionalResource_GetAllStrings_ShouldReturnAllLocalizedStrings()
  {
    // Arrange & Act
    var result = _stringAdditionalLocalizer.GetAllStrings(true);
    var lastTwoItems = result.TakeLast(2).ToList();

    // Assert
    Assert.True(result.Any());
    Assert.Collection(lastTwoItems,
      item => Assert.Equal("World, {0}!", item.Value),
      item => Assert.Equal("World, {0} {1} {2}!", item.Value));
  }
}
