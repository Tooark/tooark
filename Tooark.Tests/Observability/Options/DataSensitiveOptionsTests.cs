using Tooark.Observability.Options;

namespace Tooark.Tests.Observability.Options;

public class DataSensitiveOptionsTests
{
  #region Default Values

  // Teste para verificar os valores padrão.
  [Fact]
  public void DataSensitiveOptions_ShouldHaveCorrectDefaultValues()
  {
    // Arrange & Act
    var options = new DataSensitiveOptions();

    // Assert
    Assert.True(options.HideQueryParameters);
    Assert.True(options.HideHeaders);
    Assert.NotNull(options.SensitiveRequestHeaders);
    Assert.NotEmpty(options.SensitiveRequestHeaders);
  }

  // Teste para verificar os headers sensíveis padrão.
  [Fact]
  public void DataSensitiveOptions_ShouldHaveCorrectDefaultSensitiveHeaders()
  {
    // Arrange & Act
    var options = new DataSensitiveOptions();
    var expectedHeaders = new[]
    {
      "authorization",
      "proxy-authorization",
      "cookie",
      "set-cookie",
      "x-api-key",
      "api-key",
      "apikey",
      "x-functions-key",
      "x-amz-security-token",
      "x-google-oauth-access-token",
      "x-azure-access-token"
    };

    // Assert
    Assert.Equal(expectedHeaders.Length, options.SensitiveRequestHeaders.Length);
    foreach (var header in expectedHeaders)
    {
      Assert.Contains(header, options.SensitiveRequestHeaders);
    }
  }

  #endregion

  #region Property Setters

  // Teste para definir HideQueryParameters.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void DataSensitiveOptions_ShouldSetHideQueryParameters(bool value)
  {
    // Arrange
    DataSensitiveOptions options = new();

    // Act
    options.HideQueryParameters = value;

    // Assert
    Assert.Equal(value, options.HideQueryParameters);
  }

  // Teste para definir HideHeaders.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void DataSensitiveOptions_ShouldSetHideHeaders(bool value)
  {
    // Arrange
    DataSensitiveOptions options = new();

    // Act
    options.HideHeaders = value;

    // Assert
    Assert.Equal(value, options.HideHeaders);
  }

  // Teste para definir SensitiveRequestHeaders.
  [Fact]
  public void DataSensitiveOptions_ShouldSetSensitiveRequestHeaders()
  {
    // Arrange
    var options = new DataSensitiveOptions();
    var customHeaders = new[] { "custom-auth", "x-custom-token" };

    // Act
    options.SensitiveRequestHeaders = customHeaders;

    // Assert
    Assert.Equal(customHeaders, options.SensitiveRequestHeaders);
    Assert.Equal(2, options.SensitiveRequestHeaders.Length);
  }

  // Teste para definir SensitiveRequestHeaders como array vazio.
  [Fact]
  public void DataSensitiveOptions_ShouldSetEmptySensitiveRequestHeaders()
  {
    // Arrange
    DataSensitiveOptions options = new();

    // Act
    options.SensitiveRequestHeaders = [];

    // Assert
    Assert.Empty(options.SensitiveRequestHeaders);
  }

  #endregion
}
