using Tooark.Observability.Options;

namespace Tooark.Tests.Observability.Options;

public class MetricsOptionsTests
{
  #region Default Values

  // Teste para verificar os valores padrão.
  [Fact]
  public void MetricsOptions_ShouldHaveCorrectDefaultValues()
  {
    // Arrange & Act
    MetricsOptions options = new();

    // Assert
    Assert.True(options.Enabled);
    Assert.True(options.RuntimeMetricsEnabled);
    Assert.Equal("Tooark", options.MeterName);
    Assert.NotNull(options.AdditionalMeters);
    Assert.Empty(options.AdditionalMeters);
  }

  #endregion

  #region Property Setters

  // Teste para definir Enabled.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void MetricsOptions_ShouldSetEnabled(bool value)
  {
    // Arrange
    MetricsOptions options = new();

    // Act
    options.Enabled = value;

    // Assert
    Assert.Equal(value, options.Enabled);
  }

  // Teste para definir RuntimeMetricsEnabled.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void MetricsOptions_ShouldSetRuntimeMetricsEnabled(bool value)
  {
    // Arrange
    MetricsOptions options = new();

    // Act
    options.RuntimeMetricsEnabled = value;

    // Assert
    Assert.Equal(value, options.RuntimeMetricsEnabled);
  }

  // Teste para definir MeterName.
  [Theory]
  [InlineData("MyApp")]
  [InlineData("MyCompany.MyProduct")]
  [InlineData("Custom.Meter.Name")]
  public void MetricsOptions_ShouldSetMeterName(string meterName)
  {
    // Arrange
    MetricsOptions options = new();

    // Act
    options.MeterName = meterName;

    // Assert
    Assert.Equal(meterName, options.MeterName);
  }

  // Teste para definir AdditionalMeters.
  [Fact]
  public void MetricsOptions_ShouldSetAdditionalMeters()
  {
    // Arrange
    MetricsOptions options = new();
    var additionalMeters = new[]
    {
      "MyCompany.MyProduct",
      "MyCompany.MyProduct.Component1",
      "MyCompany.MyProduct.Component2"
    };

    // Act
    options.AdditionalMeters = additionalMeters;

    // Assert
    Assert.Equal(additionalMeters, options.AdditionalMeters);
    Assert.Equal(3, options.AdditionalMeters.Length);
  }

  // Teste para definir AdditionalMeters como array vazio.
  [Fact]
  public void MetricsOptions_ShouldSetEmptyAdditionalMeters()
  {
    // Arrange
    MetricsOptions options = new();

    // Act
    options.AdditionalMeters = [];

    // Assert
    Assert.Empty(options.AdditionalMeters);
  }

  #endregion

  #region Combined Configurations

  // Teste para configurar todas as opções.
  [Fact]
  public void MetricsOptions_ShouldAllowFullConfiguration()
  {
    // Arrange & Act
    MetricsOptions options = new()
    {
      Enabled = true,
      RuntimeMetricsEnabled = false,
      MeterName = "CustomMeter",
      AdditionalMeters = ["Meter1", "Meter2"]
    };

    // Assert
    Assert.True(options.Enabled);
    Assert.False(options.RuntimeMetricsEnabled);
    Assert.Equal("CustomMeter", options.MeterName);
    Assert.Equal(2, options.AdditionalMeters.Length);
    Assert.Contains("Meter1", options.AdditionalMeters);
    Assert.Contains("Meter2", options.AdditionalMeters);
  }

  // Teste para configuração desabilitada.
  [Fact]
  public void MetricsOptions_ShouldAllowDisabling()
  {
    // Arrange
    MetricsOptions options = new()
    {
      Enabled = false,
      RuntimeMetricsEnabled = false
    };

    // Assert
    Assert.False(options.Enabled);
    Assert.False(options.RuntimeMetricsEnabled);
  }

  #endregion
}
