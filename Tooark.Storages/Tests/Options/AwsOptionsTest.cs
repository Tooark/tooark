using System.Text.Json;
using Amazon;
using Amazon.S3;
using Tooark.Storages.Options;

namespace Tooark.Tests.Storages.Options;

public class AwsOptionsTest
{
  // Teste para verificar se o construtor inicializa com valores padrão
  [Fact]
  public void Constructor_ShouldInitializeWithDefaultValues()
  {
    // Arrange & Act
    var options = new AwsOptions();

    // Assert
    Assert.Null(options.GetCredentials());
    Assert.Equal(RegionEndpoint.USEast1, options.Region);
    Assert.Null(options.Acl);
  }

  // Teste para verificar se o construtor inicializa com valores passados
  [Fact]
  public void Constructor_WithAccessKeyAndSecretKey_ShouldSetCredentials()
  {
    // Arrange
    var accessKey = "testAccessKey";
    var secretKey = "testSecretKey";

    // Act
    var options = new AwsOptions(accessKey, secretKey);

    // Assert
    Assert.Equal(accessKey, options.AccessKey);
    Assert.Equal(secretKey, options.SecretKey);
  }

  // Teste para verificar se o construtor inicializa com valores passados e região
  [Fact]
  public void Constructor_WithRegion_ShouldSetRegion()
  {
    // Arrange
    var accessKey = "testAccessKey";
    var secretKey = "testSecretKey";
    var region = RegionEndpoint.USWest2;

    // Act
    var options = new AwsOptions(accessKey, secretKey, region);

    // Assert
    Assert.Equal(region, options.Region);
    Assert.Equal(region.SystemName, options.RegionString);
  }

  // Teste para verificar se o construtor inicializa com valores passados, região e acl
  [Fact]
  public void Constructor_WithAcl_ShouldSetAcl()
  {
    // Arrange
    var accessKey = "testAccessKey";
    var secretKey = "testSecretKey";
    var region = RegionEndpoint.USWest2;
    var acl = S3CannedACL.PublicRead;

    // Act
    var options = new AwsOptions(accessKey, secretKey, region, acl);

    // Assert
    Assert.Equal(acl, options.Acl);
    Assert.Equal(acl.Value, options.AclString);
  }

  // Teste para verificar se o construtor inicializa com valores passados, região e acl
  [Fact]
  public void Constructor_WithJson_ShouldSetValues()
  {
    // Arrange
    string accessKey = "testAccessKey";
    string secretKey = "testSecretKey";
    string region = "us-west-2";
    string acl = "public-read";
    var json =$@"
    {{
      ""AccessKey"": ""{accessKey}"",
      ""SecretKey"": ""{secretKey}"",
      ""Region"": ""{region}"",
      ""Acl"": ""{acl}""
    }}";

    // Act
    var options = JsonSerializer.Deserialize<AwsOptions>(json)!;

    // Assert
    Assert.Equal(accessKey, options.AccessKey);
    Assert.Equal(secretKey, options.SecretKey);
    Assert.NotNull(options.GetCredentials());
    Assert.Equal(RegionEndpoint.GetBySystemName(region), options.Region);
    Assert.Equal(S3CannedACL.FindValue(acl), options.Acl);
  }
}
