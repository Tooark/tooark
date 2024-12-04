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
  private readonly string _filePath;

  public JsonStringLocalizerExtensionTest()
  {
    _mockDistributedCache = new Mock<IDistributedCache>();
    _localizer = new JsonStringLocalizerExtension(_mockDistributedCache.Object, []);
    
    _culture = "en-US";
    _filePath = $"Resources/{_culture}.json";
    File.WriteAllText(_filePath, "{\"hello\": \"Hello\", \"world\": \"World\"}");
    Languages.SetCulture(_culture);

    _additionalLocalizer = new JsonStringLocalizerExtension(_mockDistributedCache.Object, new(){{_culture, _filePath}});
  }

  // Teste para verificar quando não existe o arquivo retorna a key
  [Fact]
  public void GetLocalizedString_ShouldReturnKey_WhenFileDoesNotExist()
  {
    // Arrange
    string key = "hello";
    string culture = "en";
    Languages.SetCulture(culture);

    // Act
    string result = _localizer.GetLocalizedString(key, culture);

    // Assert
    Assert.Equal(key, result);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar quando existe o arquivo retorna a string localizada
  [Fact]
  public void GetLocalizedString_ShouldReturnLocalizedString_WhenFileExists()
  {
    // Arrange
    string key = "hello";
    string localizedValue = "Hello";

    // Act
    string result = _additionalLocalizer.GetLocalizedString(key, _culture);

    // Assert
    Assert.Equal(localizedValue, result);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se carregou todos arquivos e se as duas últimas strings são as esperadas
  [Fact]
  public void GetAllStrings_ShouldReturnAllLocalizedStrings()
  {
    // Arrange & Act
    var result = _additionalLocalizer.GetAllStrings(false);
    var lastTwoItems = result.TakeLast(2).ToList();

    // Assert
    Assert.True(result.Any());
    Assert.Collection(lastTwoItems,
      item => Assert.Equal("Hello", item.Value),
      item => Assert.Equal("World", item.Value));

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para localizar a string com a cultura padrão quando a cultura selecionada é nula
  [Fact]
  public void GetLocalizedString_ShouldReturnDefaultCultureString_WhenCultureSelectIsNull()
  {
    // Arrange
    string key = "hello";
    string defaultCulture = "en";
    string defaultFilePath = $"Resources/{defaultCulture}.default.json";
    string localizedValue = "Hello";
    Languages.SetCulture(defaultCulture);
    File.WriteAllText(defaultFilePath, "{\"hello\": \"Hello\"}");

    // Act
    string result = _localizer.GetLocalizedString(key, null);

    // Assert
    Assert.Equal(localizedValue, result);

    // Cleanup
    File.Delete(defaultFilePath);
  }

  // Teste para localizar a string com a cultura padrão quando a cultura selecionada é vazia
  [Fact]
  public void GetLocalizedString_ShouldReturnDefaultCultureString_WhenCultureSelectIsEmpty()
  {
    // Arrange
    string key = "hello";
    string defaultCulture = "en";
    string defaultFilePath = $"Resources/{defaultCulture}.default.json";
    string localizedValue = "Hello";
    Languages.SetCulture(defaultCulture);
    File.WriteAllText(defaultFilePath, "{\"hello\": \"Hello\"}");

    // Act
    string result = _localizer.GetLocalizedString(key, string.Empty);

    // Assert
    Assert.Equal(localizedValue, result);

    // Cleanup
    File.Delete(defaultFilePath);
  }
}
