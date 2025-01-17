using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Google.Apis.Auth.OAuth2;
using Tooark.Enums;
using Tooark.Options;

namespace Tooark.Tests.Options;

public class BucketOptionsTests
{
  // Testes com propriedades AWS
  [Fact]
  public void AWSProperty_ShouldBeSetAndGetCorrectly()
  {
    // Arrange
    var bucketOptions = new BucketOptions();
    var awsCredentials = new BasicAWSCredentials("accessKey", "secretKey");

    // Act
    bucketOptions.AWS = awsCredentials;

    // Assert
    Assert.Equal(awsCredentials, bucketOptions.AWS);
  }

  // Testes com propriedades GCP
  [Fact]
  public void GCPProperty_ShouldBeSetAndGetCorrectly()
  {
    // Arrange
    var bucketOptions = new BucketOptions();
    var gcpCredentials = new JsonCredentialParameters();

    // Act
    bucketOptions.GCP = gcpCredentials;

    // Assert
    Assert.Equal(gcpCredentials, bucketOptions.GCP);
  }

  // Testes com propriedades nulas
  [Fact]
  public void Property_ShouldBeNullAndDefault()
  {
    // Arrange
    var bucketOptions = new BucketOptions();

    // Act & Assert
    Assert.Null(bucketOptions.AWS);
    Assert.Null(bucketOptions.GCP);
    Assert.Equal("", bucketOptions.BucketName);
    Assert.Equal(1024, bucketOptions.FileSize);
    Assert.Equal(CloudProvider.None, bucketOptions.CloudProvider);
    Assert.Equal(RegionEndpoint.USEast1, bucketOptions.AWSRegion);
    Assert.Equal(S3CannedACL.Private, bucketOptions.AWSAcl);
  }

  // Testes com propriedade BucketName
  [Fact]
  public void BucketName_ShouldBeSetAndGetCorrectly()
  {
    // Arrange
    var bucketOptions = new BucketOptions();
    var bucketName = "my-bucket";

    // Act
    bucketOptions.BucketName = bucketName;

    // Assert
    Assert.Equal(bucketName, bucketOptions.BucketName);
  }

  // Testes com propriedade FileSize
  [Fact]
  public void FileSize_ShouldBeSetAndGetCorrectly()
  {
    // Arrange
    var bucketOptions = new BucketOptions();
    var fileSize = 1024L;

    // Act
    bucketOptions.FileSize = fileSize;

    // Assert
    Assert.Equal(fileSize, bucketOptions.FileSize);
  }

  // Testes com propriedade CloudProvider
  [Theory]
  [InlineData(CloudProvider.AWS)]
  [InlineData(CloudProvider.GCP)]
  [InlineData(CloudProvider.None)]
  public void CloudProvider_ShouldBeSetAndGetCorrectly(CloudProvider cloudProvider)
  {
    // Arrange & Act
    var bucketOptions = new BucketOptions
    {
      CloudProvider = cloudProvider
    };

    // Assert
    Assert.Equal(cloudProvider, bucketOptions.CloudProvider);
  }

  // Testes com propriedade AWSRegion
  [Fact]
  public void AWSRegion_ShouldBeSetAndGetCorrectly()
  {
    // Arrange
    var bucketOptions = new BucketOptions();
    RegionEndpoint awsRegion = RegionEndpoint.USWest1;

    // Act
    bucketOptions.AWSRegion = awsRegion;

    // Assert
    Assert.Equal(awsRegion, bucketOptions.AWSRegion);
  }

  // Testes com propriedade AWSAcl
  [Fact]
  public void AWSAcl_ShouldBeSetAndGetCorrectly()
  {
    // Arrange
    var bucketOptions = new BucketOptions();
    var awsAcl = S3CannedACL.PublicRead;

    // Act
    bucketOptions.AWSAcl = awsAcl;

    // Assert
    Assert.Equal(awsAcl, bucketOptions.AWSAcl);
  }
}
