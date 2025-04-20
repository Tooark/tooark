using Amazon.S3;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tooark.Storages;
using Tooark.Storages.Factories;
using Tooark.Storages.Interfaces;
using Tooark.Storages.Options;

namespace Tooark.Tests.Storages.Factories;

public class StorageServiceFactoryTests
{
  // Teste com todos os parâmetros válidos
  [Fact]
  public void Create_WithValidParameters_ReturnsStorageServiceInstance()
  {
    // Arrange
    var loggerMock = new Mock<ILogger<StorageService>>();
    var storageOptionsMock = new Mock<IOptions<StorageOptions>>();
    var awsClientMock = new Mock<IAmazonS3>();
    var gcpClientMock = new Mock<StorageClient>();
    var gcpClientSignerMock = new Mock<IUrlSigner>();
    storageOptionsMock
      .Setup(o => o.Value)
      .Returns(
        new StorageOptions
        {
          BucketName = "test-bucket",
          CloudProvider = "AWS",
          AWS = new AwsOptions()
        }
      );

    // Act
    var result = StorageServiceFactory.Create(
      loggerMock.Object,
      storageOptionsMock.Object,
      awsClientMock.Object,
      gcpClientMock.Object,
      gcpClientSignerMock.Object
    );

    // Assert
    Assert.NotNull(result);
    Assert.IsType<StorageService>(result);
  }

  // Teste com parâmetros opcionais nulos
  [Fact]
  public void Create_WithNullOptionalParameters_ReturnsStorageServiceInstance()
  {
    // Arrange
    var loggerMock = new Mock<ILogger<StorageService>>();
    var storageOptionsMock = new Mock<IOptions<StorageOptions>>();
    storageOptionsMock
      .Setup(o => o.Value)
      .Returns(
        new StorageOptions
        {
          BucketName = "test-bucket",
          CloudProvider = "AWS",
          AWS = new AwsOptions()
        }
      );

    // Act
    var result = StorageServiceFactory.Create(
      loggerMock.Object,
      storageOptionsMock.Object
    );

    // Assert
    Assert.NotNull(result);
    Assert.IsType<StorageService>(result);
  }
}
