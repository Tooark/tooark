using Tooark.Enums;
using Tooark.Storages.Options;

namespace Tooark.Tests.Storages.Options;

public class StorageOptionsTest
{
  // Teste para verificar se os valores padrões são setados corretamente
  [Fact]
  public void DefaultValues_ShouldBeSetCorrectly()
  {
    // Arrange & Act
    var options = new StorageOptions();

    // Assert
    Assert.Equal("Storage", StorageOptions.Section);
    Assert.Equal("", options.BucketName);
    Assert.Equal(2048, options.FileSize);
    Assert.Equal(5, options.SignerMinutes);
    Assert.Equal(ECloudProvider.None, options.CloudProvider);
    Assert.Null(options.AWS);
    Assert.Null(options.AWSRead);
    Assert.Null(options.GCP);
    Assert.Null(options.GCPRead);
  }

  // Teste para verificar se o construtor define os valores dos parâmetros corretamente
  [Fact]
  public void Constructor_ShouldBeSetCorrectly_WhenSetParams()
  {
    // Arrange
    var bucketName = "bucket";
    var fileSize = 2048;
    var signerMinute = 5;
    var cloudProvider = ECloudProvider.Microsoft;

    // Act
    var options = new StorageOptions() 
    {
      BucketName = bucketName,
      FileSize = fileSize,
      SignerMinutes = signerMinute,
      CloudProvider = cloudProvider
    };

    // Assert
    Assert.Equal(bucketName, options.BucketName);
    Assert.Equal(fileSize, options.FileSize);
    Assert.Equal(signerMinute, options.SignerMinutes);
    Assert.Equal(cloudProvider, options.CloudProvider);
  }

  // Teste para verificar se o construtor inicializa as propriedades corretamente da AWS
  [Fact]
  public void AWS_ShouldReturnAWS_WhenAWSReadIsNotSet()
  {
    // Arrange
    var awsOptions = new AwsOptions();

    // Act
    var options = new StorageOptions { AWS = awsOptions };

    // Assert
    Assert.Equal(awsOptions, options.AWS);
    Assert.Equal(awsOptions, options.AWSRead);
  }

  // Teste para verificar se o construtor inicializa as propriedades corretamente da AWS e AWSRead
  [Fact]
  public void AWSRead_ShouldReturnAWSRead_WhenAWSReadIsSet()
  {
    // Arrange
    var awsOptions = new AwsOptions();
    var awsReadOptions = new AwsOptions();

    // Act
    var options = new StorageOptions { AWS = awsOptions, AWSRead = awsReadOptions };

    // Assert
    Assert.Equal(awsOptions, options.AWS);
    Assert.Equal(awsReadOptions, options.AWSRead);
  }

  // Teste para verificar se o construtor inicializa as propriedades corretamente da GCP
  [Fact]
  public void GCP_ShouldReturnGCP_WhenGCPReadIsNotSet()
  {
    // Arrange
    var gcpOptions = new GcpOptions();

    // Act
    var options = new StorageOptions { GCP = gcpOptions };

    // Assert
    Assert.Equal(gcpOptions, options.GCP);
    Assert.Equal(gcpOptions, options.GCPRead);
  }

  // Teste para verificar se o construtor inicializa as propriedades corretamente da GCP e GCPRead
  [Fact]
  public void GCPRead_ShouldReturnGCPRead_WhenGCPReadIsSet()
  {
    // Arrange
    var gcpOptions = new GcpOptions();
    var gcpReadOptions = new GcpOptions();

    // Act
    var options = new StorageOptions { GCP = gcpOptions, GCPRead = gcpReadOptions };

    // Assert
    Assert.Equal(gcpOptions, options.GCP);
    Assert.Equal(gcpReadOptions, options.GCPRead);
  }  
}
