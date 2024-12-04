using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Moq;
using Tooark.Factories;
using static Tooark.Utils.Util;

namespace Tooark.Tests.Extensions;

public class IStringLocalizerTest
{
  private readonly Mock<IDistributedCache> _distributedCacheMock;
  private readonly IStringLocalizer _localizer;
  private readonly string _culture;
  private readonly string _filePath;

  public IStringLocalizerTest()
  {
    _culture = "en-US";
    _filePath = $"Resources/{_culture}.json";
    File.WriteAllText(_filePath, "{\"hello\": \"Hello\", \"world\": \"World\", \"param\": \"Hello, {0}!\", \"multiParam\": \"Hello, {0} {1} {2}!\"}");
    Languages.SetCulture(_culture);

    _distributedCacheMock = new Mock<IDistributedCache>();

    var factory = new JsonStringLocalizerFactory(_distributedCacheMock.Object, new() { { _culture, _filePath } });

    _localizer = factory.Create(typeof(IStringLocalizerTest));
  }

  // Teste para verificar se o método this[string name] retorna uma string localizada
  [Fact]
  public void Indexer_ShouldReturnLocalizedString()
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

  // Teste para verificar se o método this[string name] retorna uma string localizada com parâmetro
  [Fact]
  public void Indexer_WithSingleParameter_ShouldReturnLocalizedString()
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

  // Teste para verificar se o método this[string name] retorna uma string localizada com múltiplos parâmetros
  [Fact]
  public void Indexer_WithMultiParameter_ShouldReturnLocalizedString()
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

  // Teste para verificar se o método this[string name, string arguments] retorna uma string localizada formatada
  [Fact]
  public void IndexerWithSingleArguments_ShouldReturnFormattedLocalizedString()
  {
    // Arrange
    string key = "multiParam";
    string[] arguments = ["Package", "Nuget", "Tooark"];
    string formattedValue = "Hello, Package Nuget Tooark!";

    // Act
    var result = _localizer[key, arguments];

    // Assert
    Assert.Equal(formattedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método this[string name, params object[] arguments] retorna uma string localizada formatada
  [Fact]
  public void IndexerWithMultiArguments_ShouldReturnFormattedLocalizedString()
  {
    // Arrange
    string key = "param";
    string arguments = "Tooark";
    string formattedValue = "Hello, Tooark!";

    // Act
    var result = _localizer[key, arguments];

    // Assert
    Assert.Equal(formattedValue, result.Value);

    // Cleanup
    File.Delete(_filePath);
  }

  // Teste para verificar se o método GetAllStrings retorna todas as strings localizadas
  [Fact]
  public void GetAllStrings_ShouldReturnAllLocalizedStrings()
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
}
