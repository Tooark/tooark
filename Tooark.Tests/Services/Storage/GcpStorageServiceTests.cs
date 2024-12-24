using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using Moq;
using Tooark.Enums;
using Tooark.Factories;
using Tooark.Interfaces;
using Tooark.Options;

namespace Tooark.Tests.Services.Storage;

public class GcpStorageServiceTests
{
  private readonly Mock<IOptions<BucketOptions>> _bucketOptionsMock;
  private readonly Mock<StorageClient> _storageClientMock;
  private readonly IStorageService _storageService;

  public GcpStorageServiceTests()
  {
    _bucketOptionsMock = new Mock<IOptions<BucketOptions>>();
    _storageClientMock = new Mock<StorageClient>();

    var bucketOptions = new BucketOptions
    {
      CloudProvider = CloudProvider.GCP,
      BucketName = "test-bucket",
      FileSize = 1024,
      GCPPath = "path/to/credentials.json"
    };

    _bucketOptionsMock.Setup(o => o.Value).Returns(bucketOptions);

    _storageService = StorageServiceFactory.Create(_bucketOptionsMock.Object);
  }

  // Teste para verificar se o método Create está enviando o arquivo para o GCP
  [Fact]
  public async Task Create_ShouldUploadFileToGCP()
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

    var objectUpload = new Google.Apis.Storage.v1.Data.Object
    {
      Id = "1",
      Name = fileName,
      Bucket = bucketName,
      MediaLink = "http://example.com",
      SelfLink = "http://example.com"
    };

    _storageClientMock
      .Setup(s => s.UploadObjectAsync(bucketName, fileName, contentType, fileStream, null, default, null))
      .ReturnsAsync(objectUpload);

    // Act
    var result = await _storageService.Create(filePath, fileName, bucketName, contentType);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
    Assert.Equal(bucketName, result.BucketName);
  }

  // Teste para verificar se o método Delete está removendo o arquivo do GCP
  [Fact]
  public async Task Delete_ShouldRemoveFileFromGCP()
  {
    // Arrange
    var fileName = "test.txt";
    var bucketName = "test-bucket";

    _storageClientMock
      .Setup(s => s.DeleteObjectAsync(fileName, bucketName, null, default))
      .Returns(Task.CompletedTask);

    // Act
    await _storageService.Delete(fileName, bucketName);

    // Assert
    _storageClientMock.Verify(s => s.DeleteObjectAsync(fileName, bucketName, null, default), Times.Once);
  }

  // Teste para verificar se o método Download está recuperando o arquivo do GCP
  [Fact]
  public async Task Download_ShouldRetrieveFileFromGCP()
  {
    // Arrange
    var fileName = "test.txt";
    var bucketName = "test-bucket";

    var getObjectResponse = new MemoryStream();
    var storageObject = new Google.Apis.Storage.v1.Data.Object();

    _storageClientMock.Setup(s => s.DownloadObjectAsync(bucketName, fileName, getObjectResponse, null, default, null))
      .ReturnsAsync(storageObject)
      .Callback<string, string, Stream, DownloadObjectOptions, CancellationToken>((bucket, name, stream, options, token) =>
      {
        getObjectResponse.CopyTo(stream);
        stream.Position = 0;
      });

    // Act
    var result = await _storageService.Download(fileName, bucketName);

    // Assert
    Assert.NotNull(result);
  }
}
