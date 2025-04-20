using Amazon;
using Amazon.S3.Model;
using Object = Google.Apis.Storage.v1.Data.Object;
using Tooark.Storages.Dtos;

namespace Tooark.Tests.Storages.Dtos;

public class UploadResponseDtoTest
{
  // Teste de unidade para o construtor que utiliza um retorno do Google Cloud Storage.
  [Fact]
  public void Constructor_GoogleStorageResponse_ShouldSetProperties()
  {
    // Arrange
    var googleResponse = new Object
    {
      Id = "test-id",
      MediaLink = "http://example.com/test-file",
      Bucket = "test-bucket",
      Name = "test-file",
      Size = 12345
    };

    // Act
    var dto = new UploadResponseDto(googleResponse);

    // Assert
    Assert.Equal("test-id", dto.Id);
    Assert.Equal("http://example.com/test-file", dto.Link);
    Assert.Equal("test-bucket", dto.BucketName);
    Assert.Equal("test-file", dto.FileName);
    Assert.Equal((ulong)12345, dto.Size);
  }

  // Teste de unidade para o construtor que utiliza um retorno do Amazon S3.
  [Fact]
  public void Constructor_AmazonS3Response_ShouldSetProperties()
  {
    // Arrange
    var bucketName = "test-bucket";
    var fileName = "test-file";
    var region = RegionEndpoint.USEast1;
    var s3Response = new PutObjectResponse
    {
      Size = 12345
    };

    // Act
    var dto = new UploadResponseDto(bucketName, fileName, region, s3Response);

    // Assert
    var expectedLink = $"https://{bucketName}.s3.{region.SystemName}.amazonaws.com/{fileName}";
    Assert.Equal(expectedLink, dto.Id);
    Assert.Equal(expectedLink, dto.Link);
    Assert.Equal(bucketName, dto.BucketName);
    Assert.Equal(fileName, dto.FileName);
    Assert.Equal((ulong)12345, dto.Size);
  }

  // Teste de unidade para o construtor que recebe todos os parâmetros.
  [Fact]
  public void Constructor_AllParameters_ShouldSetProperties()
  {
    // Arrange
    var id = "test-id";
    var link = "http://example.com/test-file";
    var bucketName = "test-bucket";
    var fileName = "test-file";
    var size = (ulong)12345;

    // Act
    var dto = new UploadResponseDto(id, link, bucketName, fileName, size);

    // Assert
    Assert.Equal(id, dto.Id);
    Assert.Equal(link, dto.Link);
    Assert.Equal(bucketName, dto.BucketName);
    Assert.Equal(fileName, dto.FileName);
    Assert.Equal(size, dto.Size);
  }
}
