using Amazon.Util.Internal.PlatformServices;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tooark.Enums;
using Tooark.Factories;
using Tooark.Interfaces;
using Tooark.Options;

namespace Tooark.Tests.Services.Storage;

public class StorageServiceTests
{
  private readonly Mock<IOptions<BucketOptions>> _mockBucketOptions;
  private readonly Mock<ILogger<StorageService>> _mockLogger;
  private readonly IStorageService _storageService;

  public StorageServiceTests()
  {
    _mockBucketOptions = new Mock<IOptions<BucketOptions>>();
    _mockLogger = new Mock<ILogger<StorageService>>();

    var bucketOptions = new BucketOptions
    {
      BucketName = "test-bucket",
      FileSize = 1024,
      CloudProvider = CloudProvider.AWS
    };

    _mockBucketOptions.Setup(o => o.Value).Returns(bucketOptions);

    _storageService = StorageServiceFactory.Create(_mockBucketOptions.Object);
  }

  // private readonly Mock<IOptions<BucketOptions>> _mockBucketOptions;
  // private readonly Mock<ILogger<IStorageService>> _mockLogger;
  // private readonly IStorageService _storageService;

  // public StorageServiceTests()
  // {
  //   _mockBucketOptions = new Mock<IOptions<BucketOptions>>();
  //   _mockLogger = new Mock<ILogger<IStorageService>>();

  //   var jsonCredential = new JsonCredentialParameters()
  //   {
  //     Type = "service_account",
  //     ProjectId = "test-project",
  //     PrivateKeyId = "test-private",
  //     PrivateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCSS/kHa335lTjU\nomPL4bmZ+gsRhgIPwfAVKPcuPUfRDPNLMfj1P4qgKwNE+1CLDgfS/imoBDjAggwW\nf2WVv6tzazBaWt+ucR0jDp44dYvHu8dsY75GlvPtoLzWLQvedl7FVsFTWM1UtzsI\nNJ3lhAOihHpUyZRws1L5YJAJZBOLtCzsNjgMv+nIIeicjqdyt1CTCVLNr91wHJHx\nV1txUjVnWcRLEWHdd3pvS/UAWg/CIEtMZMnO49ce1rsICoZxwdQWfdgymHj7aKIf\nL+ceLUjlwQ1z9SWQUJzRvc6zaTDb0LOHiexHryqn40wRsg8A227PP9gkrUoAjNLi\no8mHsjPPAgMBAAECggEABvQbexHN5A4GrHynd6pb8wiifAu9j1gtfCP9U7JP9ozC\nB6Hxbl69zQ0eU6oKUwGwt4OjITxKIkRMJMmbX3nDwXGn07OcDCOOOftY4+Aaa7zu\nJqW7BonoQDQypY9wj/taV9De0YJYCB3JbYNqud0DezcGxX7rUkwDtpyzW+7oyuwM\nB41URX0woq/AcNHjy1IORs1QkoiKOvFfFbJsJV/AfhsnrGHMfROZV+DrcUOpIolc\nkkCHH6ysuENOT4AhBuhDpRxUCRu3qRaW2/XwoQwytOxo5nRkkMGUuYUF1x1xW4Ob\ntB90nbWAX4rjpkJNmQOfkPNOnY5MqClUpkQrsIS3yQKBgQDLBTe9z535xn4Hz3v/\nvmUz6GzqTYcA9RYpSPYwcmalYEwii4pU6Dkcr6oazKETKqlVN3q0CRe9x6dXS++9\ni/oJ1OCRRrAIBkYBPiUiIU6Hb063pKOYo7gOeHjxvnhsqhU56i5tndtYKzmxg6IY\nGqs2VvumpS4zOAPebomdu+NnXQKBgQC4eVUc5NSCy501+lQavkb33DNuBPVFD1Dd\nRQ+Sujk8ipMygqxfUMnL1aK64A1GDAqinZOZAJMdIEjmwBhwtlY//mQQzyoK0fC/\nZ+u871L7jlowGVdiQkz9iLWWrdQ1xlJtZDomYqW190IJkmwFa2OJiYkEJPASt+9e\n1NisxEuxGwKBgEotQN2IIvckz914sJyTaxSZIlpFM0NlSNCSyOiQk/JuicLBayx0\noJFbmXIrO9rt5mqtV5a6D8OWVAzwQMrnftbiwZ4yzpRP/nnrw9OBidiwEXnFnMRj\nZRdcawwghI1nY1QCvS3t1DuVp4G1T58w90dmZpHPnl62Y6t8haltAbw9AoGAG7v6\nJN7uaD+ughgBnweuacNMZCUQrvJPiQTCA6BFzKlU2go1WhsuS5vx3EClHjvfOXWR\nZDZm58Eb1L1ar09qhjJ73t8WhgvsMwqvsXcVtcZAHu0gayBmrNNp4Z5+whrv94xT\nBcRc/4+N+Rxvax0rGNl5pQrcoSPQNCSx8r+MVbMCgYEAk3zHnyBHmE+to1QJ+sXi\nwiLgQqeY+bQpmZk6bbx+QL+Tn3a258oU82O0I68Ko4QkagQAUGiwXleUtd+eQuVP\nJlDxRzYDQVZevSbQCBEljrMSBLWGYki5FnKajCOV1Ed8QCzoBsqTgvcyl62oo12p\nRBIznPaYGK0THwlGcbfwI+k=\n-----END PRIVATE KEY-----\n",
  //     ClientEmail = "test-email",
  //     ClientId = "test-id",
  //     TokenUri = "test-uri",
  //     ClientSecret = "test-secret",
  //     UniverseDomain = "test-domain"
  //   };
  //   var bucketOptions = new BucketOptions
  //   {
  //     BucketName = "test-bucket",
  //     FileSize = 1024,
  //     CloudProvider = CloudProvider.GCP,
  //     GCP = jsonCredential
  //   };

  //   _mockBucketOptions.Setup(o => o.Value).Returns(bucketOptions);

  //   _storageService = StorageServiceFactory.Create(_mockBucketOptions.Object);
  // }

  public static IFormFile CreateMockFormFile(string fileName, string content)
  {
    var fileMock = new Mock<IFormFile>();
    var ms = new MemoryStream();
    var writer = new StreamWriter(ms);
    writer.Write(content);
    writer.Flush();
    ms.Position = 0;

    fileMock.Setup(_ => _.FileName).Returns(fileName);
    fileMock.Setup(_ => _.Length).Returns(ms.Length);
    fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
    fileMock.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
      .Returns((Stream target, CancellationToken token) => ms.CopyToAsync(target, 81920, token));

    return fileMock.Object;
  }

  [Fact]
  public async Task Create_WithMockFormFile_ShouldReturnStorageResponseDto()
  {
    // Arrange
    var fileName = "test.txt";
    var content = "This is a test file content.";
    var mockFormFile = CreateMockFormFile(fileName, content);
    var bucketName = "test-bucket";
    var contentType = "text/plain";

    // Act
    var result = await _storageService.Create(mockFormFile, fileName, bucketName, contentType);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
    Assert.Equal(bucketName, result.BucketName);
  }

  [Fact]
  public async Task Create_WithNullFilePath_ShouldThrowArgumentException()
  {
    // Arrange
    string filePath = null!;
    var fileName = "test.txt";

    // Act & Assert
    await Assert.ThrowsAsync<ArgumentException>(() => _storageService.Create(filePath, fileName));
  }

  [Fact]
  public async Task Delete_WithValidFileName_ShouldNotThrowException()
  {
    // Arrange
    var fileName = "test.txt";
    var bucketName = "test-bucket";

    // Act
    var exception = await Record.ExceptionAsync(() => _storageService.Delete(fileName, bucketName));

    // Assert
    Assert.Null(exception);
  }

  [Fact]
  public async Task Download_WithValidFileName_ShouldReturnStream()
  {
    // Arrange
    var fileName = "test.txt";
    var bucketName = "test-bucket";

    // Act
    var result = await _storageService.Download(fileName, bucketName);

    // Assert
    Assert.NotNull(result);
    Assert.IsType<MemoryStream>(result);
  }
}
