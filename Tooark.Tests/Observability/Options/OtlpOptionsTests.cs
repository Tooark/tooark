using OpenTelemetry;
using OpenTelemetry.Exporter;
using Tooark.Observability.Enums;
using Tooark.Observability.Options;

namespace Tooark.Tests.Observability.Options;

public class OtlpOptionsTests
{
  #region Default Values

  // Teste para verificar os valores padrão.
  [Fact]
  public void OtlpOptions_ShouldHaveCorrectDefaultValues()
  {
    // Arrange & Act
    OtlpOptions options = new();

    // Assert
    Assert.False(options.Enabled);
    Assert.Equal(OtlpExportProtocol.Grpc, options.Protocol.ToProtocol());
    Assert.Null(options.Endpoint);
    Assert.Equal(ExportProcessorType.Batch, options.ExportProcessorType.ToType());
    Assert.NotNull(options.Batch);
    Assert.False(options.ServerlessOptimized);
    Assert.Null(options.Headers);
  }

  // Teste para verificar valores padrão do Batch aninhado.
  [Fact]
  public void OtlpOptions_ShouldHaveCorrectDefaultBatchOptions()
  {
    // Arrange & Act
    OtlpOptions options = new();

    // Assert
    Assert.NotNull(options.Batch);
    Assert.Equal(2048, options.Batch.MaxQueueSize);
    Assert.Equal(5000, options.Batch.ScheduledDelayMilliseconds);
    Assert.Equal(30000, options.Batch.ExporterTimeoutMilliseconds);
    Assert.Equal(512, options.Batch.MaxExportBatchSize);
  }

  #endregion

  #region Property Setters

  // Teste para definir Enabled.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void OtlpOptions_ShouldSetEnabled(bool value)
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.Enabled = value;

    // Assert
    Assert.Equal(value, options.Enabled);
  }

  // Teste para definir Protocol como Http.
  [Fact]
  public void OtlpOptions_ShouldSetProtocolHttp()
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.Protocol = EProtocolOtlp.Http;

    // Assert
    Assert.Equal(EProtocolOtlp.Http.ToInt(), options.Protocol.ToInt());
    Assert.Equal(OtlpExportProtocol.HttpProtobuf, options.Protocol.ToProtocol());
  }

  // Teste para definir Protocol como Grpc.
  [Fact]
  public void OtlpOptions_ShouldSetProtocolGrpc()
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.Protocol = EProtocolOtlp.Grpc;

    // Assert
    Assert.Equal(EProtocolOtlp.Grpc.ToInt(), options.Protocol.ToInt());
    Assert.Equal(OtlpExportProtocol.Grpc, options.Protocol.ToProtocol());
  }

  // Teste para definir Protocol via string.
  [Theory]
  [InlineData("http", OtlpExportProtocol.HttpProtobuf)]
  [InlineData("grpc", OtlpExportProtocol.Grpc)]
  public void OtlpOptions_ShouldSetProtocolViaString(string protocolString, OtlpExportProtocol expected)
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.Protocol = protocolString;

    // Assert
    Assert.Equal(expected, options.Protocol.ToProtocol());
  }

  // Teste para definir Endpoint.
  [Fact]
  public void OtlpOptions_ShouldSetEndpoint()
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.Endpoint = "http://localhost:4317";

    // Assert
    Assert.NotNull(options.Endpoint);
    Assert.Equal("http://localhost:4317", options.Endpoint);
  }

  // Teste para definir ExportProcessorType como Simple.
  [Fact]
  public void OtlpOptions_ShouldSetExportProcessorTypeSimple()
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.ExportProcessorType = EProcessorType.Simple;

    // Assert
    Assert.Equal(EProcessorType.Simple.ToInt(), options.ExportProcessorType.ToInt());
    Assert.Equal(ExportProcessorType.Simple, options.ExportProcessorType.ToType());
  }

  // Teste para definir ExportProcessorType como Batch.
  [Fact]
  public void OtlpOptions_ShouldSetExportProcessorTypeBatch()
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.ExportProcessorType = EProcessorType.Batch;

    // Assert
    Assert.Equal(EProcessorType.Batch.ToInt(), options.ExportProcessorType.ToInt());
    Assert.Equal(ExportProcessorType.Batch, options.ExportProcessorType.ToType());
  }

  // Teste para definir ExportProcessorType via string.
  [Theory]
  [InlineData("simple", ExportProcessorType.Simple)]
  [InlineData("batch", ExportProcessorType.Batch)]
  public void OtlpOptions_ShouldSetExportProcessorTypeViaString(string typeString, ExportProcessorType expected)
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.ExportProcessorType = typeString;

    // Assert
    Assert.Equal(expected, options.ExportProcessorType.ToType());
  }

  // Teste para definir Batch.
  [Fact]
  public void OtlpOptions_ShouldSetBatch()
  {
    // Arrange
    OtlpOptions options = new();
    var batchOptions = new OtlpBatchOptions
    {
      MaxQueueSize = 4096,
      ScheduledDelayMilliseconds = 3000,
      ExporterTimeoutMilliseconds = 15000,
      MaxExportBatchSize = 256
    };

    // Act
    options.Batch = batchOptions;

    // Assert
    Assert.Equal(4096, options.Batch.MaxQueueSize);
    Assert.Equal(3000, options.Batch.ScheduledDelayMilliseconds);
    Assert.Equal(15000, options.Batch.ExporterTimeoutMilliseconds);
    Assert.Equal(256, options.Batch.MaxExportBatchSize);
  }

  // Teste para definir ServerlessOptimized.
  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void OtlpOptions_ShouldSetServerlessOptimized(bool value)
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.ServerlessOptimized = value;

    // Assert
    Assert.Equal(value, options.ServerlessOptimized);
  }

  // Teste para definir Headers.
  [Theory]
  [InlineData("authorization=Bearer token123")]
  [InlineData("authorization=Bearer token123,tenant.id=tenant-123")]
  [InlineData(null)]
  public void OtlpOptions_ShouldSetHeaders(string? headers)
  {
    // Arrange
    OtlpOptions options = new();

    // Act
    options.Headers = headers;

    // Assert
    Assert.Equal(headers, options.Headers);
  }

  #endregion

  #region Combined Configurations

  // Teste para configuração completa de gRPC.
  [Fact]
  public void OtlpOptions_ShouldAllowGrpcConfiguration()
  {
    // Arrange & Act
    var options = new OtlpOptions
    {
      Enabled = true,
      Protocol = "grpc",
      Endpoint = "http://localhost:4317",
      ExportProcessorType = "batch",
      ServerlessOptimized = true,
      Headers = "authorization=Bearer token"
    };

    // Assert
    Assert.True(options.Enabled);
    Assert.Equal(OtlpExportProtocol.Grpc, options.Protocol.ToProtocol());
    Assert.NotNull(options.Endpoint);
    Assert.Equal("http://localhost:4317", options.Endpoint);
    Assert.Equal(ExportProcessorType.Batch, options.ExportProcessorType.ToType());
    Assert.True(options.ServerlessOptimized);
    Assert.Equal("authorization=Bearer token", options.Headers);
  }

  // Teste para configuração completa de HTTP.
  [Fact]
  public void OtlpOptions_ShouldAllowHttpConfiguration()
  {
    // Arrange & Act
    var options = new OtlpOptions
    {
      Enabled = true,
      Protocol = "http",
      Endpoint = "http://localhost:4318",
      ExportProcessorType = "simple",
      ServerlessOptimized = false,
      Headers = "x-api-key=mykey"
    };

    // Assert
    Assert.True(options.Enabled);
    Assert.Equal(OtlpExportProtocol.HttpProtobuf, options.Protocol.ToProtocol());
    Assert.NotNull(options.Endpoint);
    Assert.Equal("http://localhost:4318", options.Endpoint);
    Assert.Equal(ExportProcessorType.Simple, options.ExportProcessorType.ToType());
    Assert.False(options.ServerlessOptimized);
    Assert.Equal("x-api-key=mykey", options.Headers);
  }

  // Teste para configuração desabilitada.
  [Fact]
  public void OtlpOptions_ShouldAllowDisabledConfiguration()
  {
    // Arrange & Act
    var options = new OtlpOptions
    {
      Enabled = false
    };

    // Assert
    Assert.False(options.Enabled);
    Assert.Null(options.Endpoint);
  }

  #endregion
}
