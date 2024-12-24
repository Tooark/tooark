using Microsoft.Extensions.Options;
using Moq;
using Tooark.Enums;
using Tooark.Interfaces;
using Tooark.Options;
using Amazon.S3;
using Tooark.Factories;
using Amazon.S3.Model;

namespace Tooark.Tests.Services.Storage;

public class AwsStorageServiceTests
{
  private readonly Mock<IOptions<BucketOptions>> _bucketOptionsMock;
  private readonly Mock<IAmazonS3> _s3ClientMock;
  private readonly IStorageService _storageService;

  public AwsStorageServiceTests()
  {
    _bucketOptionsMock = new Mock<IOptions<BucketOptions>>();
    _s3ClientMock = new Mock<IAmazonS3>();

    var bucketOptions = new BucketOptions
    {
      CloudProvider = CloudProvider.AWS,
      BucketName = "test-bucket",
      FileSize = 1024,
      AWS = new Amazon.Runtime.BasicAWSCredentials("accessKey", "secretKey"),
      AWSRegion = Amazon.RegionEndpoint.USEast1,
      AWSAcl = S3CannedACL.Private
    };

    _bucketOptionsMock.Setup(o => o.Value).Returns(bucketOptions);

    _storageService = StorageServiceFactory.Create(_bucketOptionsMock.Object);
  }

  [Fact]
  public async Task Create_ShouldUploadFileToS3()
  {
    // Arrange
    var filePath = "test.txt";
    var fileName = "test.txt";
    var bucketName = "test-bucket";
    var contentType = "text/plain";

    using var fileStream = new MemoryStream();
    using var writer = new StreamWriter(fileStream);
    writer.Write("test content");
    writer.Flush();
    fileStream.Position = 0;

    var putObjectResponse = new PutObjectResponse
    {
      HttpStatusCode = System.Net.HttpStatusCode.OK
    };

    _s3ClientMock
      .Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
      .ReturnsAsync(putObjectResponse);

    // Act
    var result = await _storageService.Create(fileStream, fileName, bucketName, contentType);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
    Assert.Equal(bucketName, result.BucketName);
  }

  [Fact]
  public async Task Delete_ShouldRemoveFileFromS3()
  {
    // Arrange
    var fileName = "test.txt";
    var bucketName = "test-bucket";

    var deleteObjectResponse = new DeleteObjectResponse();

    _s3ClientMock.Setup(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), default))
        .ReturnsAsync(deleteObjectResponse);

    // Act
    await _storageService.Delete(fileName, bucketName);

    // Assert
    _s3ClientMock.Verify(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), default), Times.Once);
  }

  [Fact]
  public async Task Download_ShouldRetrieveFileFromS3()
  {
    // Arrange
    var fileName = "test.txt";
    var bucketName = "test-bucket";

    var getObjectResponse = new GetObjectResponse
    {
      ResponseStream = new MemoryStream()
    };

    _s3ClientMock.Setup(s => s.GetObjectAsync(It.IsAny<GetObjectRequest>(), default))
        .ReturnsAsync(getObjectResponse);

    // Act
    var result = await _storageService.Download(fileName, bucketName);

    // Assert
    Assert.NotNull(result);
  }
}
