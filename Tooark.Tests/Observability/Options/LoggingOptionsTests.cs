using Tooark.Observability.Options;

namespace Tooark.Tests.Observability.Options;

public class LoggingOptionsTests
{
  #region Default Values

  // Teste para verificar os valores padrão.
  [Fact]
  public void LoggingOptions_ShouldHaveCorrectDefaultValues()
  {
    // Arrange & Act
    var options = new LoggingOptions();

    // Assert
    Assert.True(options.Enabled);
    Assert.True(options.IncludeFormattedMessage);
    Assert.True(options.IncludeScopes);
    Assert.True(options.ParseStateValues);
  }

  #endregion

  #region Property Setters

  // Teste para definir Enabled.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void LoggingOptions_ShouldSetEnabled(bool value)
  {
    // Arrange
    LoggingOptions options = new();

    // Act
    options.Enabled = value;

    // Assert
    Assert.Equal(value, options.Enabled);
  }

  // Teste para definir IncludeFormattedMessage.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void LoggingOptions_ShouldSetIncludeFormattedMessage(bool value)
  {
    // Arrange
    LoggingOptions options = new();

    // Act
    options.IncludeFormattedMessage = value;

    // Assert
    Assert.Equal(value, options.IncludeFormattedMessage);
  }

  // Teste para definir IncludeScopes.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void LoggingOptions_ShouldSetIncludeScopes(bool value)
  {
    // Arrange
    LoggingOptions options = new();

    // Act
    options.IncludeScopes = value;

    // Assert
    Assert.Equal(value, options.IncludeScopes);
  }

  // Teste para definir ParseStateValues.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void LoggingOptions_ShouldSetParseStateValues(bool value)
  {
    // Arrange
    LoggingOptions options = new();

    // Act
    options.ParseStateValues = value;

    // Assert
    Assert.Equal(value, options.ParseStateValues);
  }

  #endregion

  #region Combined Configurations

  // Teste para configurar todas as opções desabilitadas.
  [Fact]
  public void LoggingOptions_ShouldAllowDisablingAllOptions()
  {
    // Arrange & Act
    LoggingOptions options = new()
    {
      Enabled = false,
      IncludeFormattedMessage = false,
      IncludeScopes = false,
      ParseStateValues = false
    };

    // Assert
    Assert.False(options.Enabled);
    Assert.False(options.IncludeFormattedMessage);
    Assert.False(options.IncludeScopes);
    Assert.False(options.ParseStateValues);
  }

  #endregion
}
