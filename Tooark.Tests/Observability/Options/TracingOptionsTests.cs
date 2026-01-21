using Tooark.Observability.Options;

namespace Tooark.Tests.Observability.Options;

public class TracingOptionsTests
{
  #region Default Values

  // Teste para verificar os valores padrão.
  [Fact]
  public void TracingOptions_ShouldHaveCorrectDefaultValues()
  {
    // Arrange & Act
    TracingOptions options = new();

    // Assert
    Assert.True(options.Enabled);
    Assert.Equal(1.0, options.SamplingRatio);
    Assert.Null(options.IgnorePathPrefix);
    Assert.NotNull(options.IgnorePaths);
    Assert.NotEmpty(options.IgnorePaths);
    Assert.Equal("Tooark", options.ActivitySourceName);
    Assert.NotNull(options.AdditionalSources);
    Assert.Empty(options.AdditionalSources);
    Assert.NotNull(options.DataSensitive);
  }

  // Teste para verificar os paths ignorados padrão.
  [Fact]
  public void TracingOptions_ShouldHaveCorrectDefaultIgnorePaths()
  {
    // Arrange & Act
    TracingOptions options = new();
    var expectedPaths = new[]
    {
      "/health",
      "/healthz",
      "/ready",
      "/traces",
      "/metrics",
      "/logs",
      "/favicon.ico"
    };

    // Assert
    Assert.Equal(expectedPaths.Length, options.IgnorePaths.Length);
    foreach (var path in expectedPaths)
    {
      Assert.Contains(path, options.IgnorePaths);
    }
  }

  #endregion

  #region Property Setters

  // Teste para definir Enabled.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void TracingOptions_ShouldSetEnabled(bool value)
  {
    // Arrange
    TracingOptions options = new();

    // Act
    options.Enabled = value;

    // Assert
    Assert.Equal(value, options.Enabled);
  }

  // Teste para definir SamplingRatio com valores válidos.
  [Theory]
  [InlineData(0.0)]
  [InlineData(0.25)]
  [InlineData(0.5)]
  [InlineData(0.75)]
  [InlineData(1.0)]
  public void TracingOptions_ShouldSetSamplingRatio(double value)
  {
    // Arrange
    TracingOptions options = new();

    // Act
    options.SamplingRatio = value;

    // Assert
    Assert.Equal(value, options.SamplingRatio);
  }

  // Teste para SamplingRatio ser limitado ao mínimo de 0.0.
  [Theory]
  [InlineData(-0.1, 0.0)]
  [InlineData(-1.0, 0.0)]
  [InlineData(-100.0, 0.0)]
  public void TracingOptions_ShouldClampSamplingRatioToMinimum(double value, double expected)
  {
    // Arrange
    TracingOptions options = new();

    // Act
    options.SamplingRatio = value;

    // Assert
    Assert.Equal(expected, options.SamplingRatio);
  }

  // Teste para SamplingRatio ser limitado ao máximo de 1.0.
  [Theory]
  [InlineData(1.1, 1.0)]
  [InlineData(2.0, 1.0)]
  [InlineData(100.0, 1.0)]
  public void TracingOptions_ShouldClampSamplingRatioToMaximum(double value, double expected)
  {
    // Arrange
    TracingOptions options = new();

    // Act
    options.SamplingRatio = value;

    // Assert
    Assert.Equal(expected, options.SamplingRatio);
  }

  // Teste para definir IgnorePathPrefix.
  [Theory]
  [InlineData("/api")]
  [InlineData("/internal")]
  [InlineData("/v1")]
  [InlineData(null)]
  public void TracingOptions_ShouldSetIgnorePathPrefix(string? prefix)
  {
    // Arrange
    TracingOptions options = new();

    // Act
    options.IgnorePathPrefix = prefix;

    // Assert
    Assert.Equal(prefix, options.IgnorePathPrefix);
  }

  // Teste para definir IgnorePaths.
  [Fact]
  public void TracingOptions_ShouldSetIgnorePaths()
  {
    // Arrange
    TracingOptions options = new();
    var paths = new[] { "/custom-health", "/custom-metrics", "/status" };

    // Act
    options.IgnorePaths = paths;

    // Assert
    Assert.Equal(paths, options.IgnorePaths);
    Assert.Equal(3, options.IgnorePaths.Length);
  }

  // Teste para definir IgnorePaths como array vazio.
  [Fact]
  public void TracingOptions_ShouldSetEmptyIgnorePaths()
  {
    // Arrange
    TracingOptions options = new();

    // Act
    options.IgnorePaths = [];

    // Assert
    Assert.Empty(options.IgnorePaths);
  }

  // Teste para definir ActivitySourceName.
  [Theory]
  [InlineData("MyApp")]
  [InlineData("MyCompany.MyProduct")]
  [InlineData("orders-api")]
  public void TracingOptions_ShouldSetActivitySourceName(string sourceName)
  {
    // Arrange
    TracingOptions options = new();

    // Act
    options.ActivitySourceName = sourceName;

    // Assert
    Assert.Equal(sourceName, options.ActivitySourceName);
  }

  // Teste para definir AdditionalSources.
  [Fact]
  public void TracingOptions_ShouldSetAdditionalSources()
  {
    // Arrange
    TracingOptions options = new();
    var sources = new[]
    {
      "Tooark.MyComponent",
      "orders-api",
      "orders-api.billing"
    };

    // Act
    options.AdditionalSources = sources;

    // Assert
    Assert.Equal(sources, options.AdditionalSources);
    Assert.Equal(3, options.AdditionalSources.Length);
  }

  // Teste para definir AdditionalSources como array vazio.
  [Fact]
  public void TracingOptions_ShouldSetEmptyAdditionalSources()
  {
    // Arrange
    TracingOptions options = new();

    // Act
    options.AdditionalSources = [];

    // Assert
    Assert.Empty(options.AdditionalSources);
  }

  // Teste para definir DataSensitive.
  [Fact]
  public void TracingOptions_ShouldSetDataSensitive()
  {
    // Arrange
    TracingOptions options = new();
    var dataSensitiveOptions = new DataSensitiveOptions
    {
      HideQueryParameters = false,
      HideHeaders = false,
      SensitiveRequestHeaders = ["custom-header"]
    };

    // Act
    options.DataSensitive = dataSensitiveOptions;

    // Assert
    Assert.False(options.DataSensitive.HideQueryParameters);
    Assert.False(options.DataSensitive.HideHeaders);
    Assert.Single(options.DataSensitive.SensitiveRequestHeaders);
    Assert.Contains("custom-header", options.DataSensitive.SensitiveRequestHeaders);
  }

  #endregion

  #region Combined Configurations

  // Teste para configuração completa.
  [Fact]
  public void TracingOptions_ShouldAllowFullConfiguration()
  {
    // Arrange & Act
    var options = new TracingOptions
    {
      Enabled = true,
      SamplingRatio = 0.5,
      IgnorePathPrefix = "/api",
      IgnorePaths = ["/health", "/metrics"],
      ActivitySourceName = "MyApp",
      AdditionalSources = ["MyApp.Component1", "MyApp.Component2"],
      DataSensitive = new DataSensitiveOptions
      {
        HideQueryParameters = true,
        HideHeaders = true
      }
    };

    // Assert
    Assert.True(options.Enabled);
    Assert.Equal(0.5, options.SamplingRatio);
    Assert.Equal("/api", options.IgnorePathPrefix);
    Assert.Equal(2, options.IgnorePaths.Length);
    Assert.Equal("MyApp", options.ActivitySourceName);
    Assert.Equal(2, options.AdditionalSources.Length);
    Assert.True(options.DataSensitive.HideQueryParameters);
    Assert.True(options.DataSensitive.HideHeaders);
  }

  // Teste para configuração desabilitada.
  [Fact]
  public void TracingOptions_ShouldAllowDisabling()
  {
    // Arrange & Act
    var options = new TracingOptions
    {
      Enabled = false
    };

    // Assert
    Assert.False(options.Enabled);
  }

  // Teste para configuração de amostragem mínima.
  [Fact]
  public void TracingOptions_ShouldAllowMinimalSamplingConfiguration()
  {
    // Arrange & Act
    var options = new TracingOptions
    {
      Enabled = true,
      SamplingRatio = 0.01 // 1% sampling
    };

    // Assert
    Assert.True(options.Enabled);
    Assert.Equal(0.01, options.SamplingRatio);
  }

  #endregion
}
