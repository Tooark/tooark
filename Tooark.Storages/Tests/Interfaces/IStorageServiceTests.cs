using Moq;
using Tooark.Storages.Dtos;
using Tooark.Storages.Interfaces;

namespace Tooark.Tests.Storages.Interfaces;

public class IStorageServiceTests
{
  [Fact]
  public async Task Upload_ShouldReturnUploadResponseDto_WhenUploadIsSuccessful()
  {
    // Arrange
    var expectedResponse = new UploadResponseDto(new Google.Apis.Storage.v1.Data.Object());
    Mock<IStorageService> storageServiceMock = new();
    storageServiceMock
      .Setup(s => s.Upload(
        It.IsAny<MemoryStream>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<string>()))
      .ReturnsAsync(expectedResponse);

    // Act
    var result = await storageServiceMock.Object.Upload(new MemoryStream(), string.Empty, string.Empty, string.Empty);

    // Assert
    Assert.Equal(expectedResponse, result);
  }

  [Fact]
  public async Task Delete_ShouldReturnSuccessMessage_WhenDeleteIsSuccessful()
  {
    // Arrange
    var expectedResponse = "File deleted successfully.";
    Mock<IStorageService> storageServiceMock = new();
    storageServiceMock
      .Setup(s => s.Delete(
        It.IsAny<string>(),
        It.IsAny<string>()))
      .ReturnsAsync(expectedResponse);

    // Act
    var result = await storageServiceMock.Object.Delete(string.Empty, string.Empty);

    // Assert
    Assert.Equal(expectedResponse, result);
  }

  [Fact]
  public async Task Download_ShouldReturnFileStream_WhenDownloadIsSuccessful()
  {
    // Arrange
    var expectedResponse = new MemoryStream();
    Mock<IStorageService> storageServiceMock = new();
    storageServiceMock
      .Setup(s => s.Download(
        It.IsAny<string>(),
        It.IsAny<string>()))
      .ReturnsAsync(expectedResponse);

    // Act
    var result = await storageServiceMock.Object.Download(string.Empty, string.Empty);

    // Assert
    Assert.Equal(expectedResponse, result);
  }

  [Fact]
  public async Task Signer_ShouldReturnSignedUrl_WhenSignerIsSuccessful()
  {
    // Arrange
    var expectedResponse = "https://signed-url.com";
    Mock<IStorageService> storageServiceMock = new();
    storageServiceMock
      .Setup(s => s.Signer(
        It.IsAny<string>(),
        It.IsAny<int?>(),
        It.IsAny<string?>()))
      .ReturnsAsync(expectedResponse);

    // Act
    var result = await storageServiceMock.Object.Signer(string.Empty, 0, string.Empty);

    // Assert
    Assert.Equal(expectedResponse, result);
  }
}
