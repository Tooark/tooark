using Tooark.Dtos;

namespace Tooark.Tests.Dtos;

public class BucketResponseDtoTest
{
  // Testa se as propriedades da classe BucketResponseDto estão sendo setadas corretamente
  [Fact]
  public void BucketResponseDto_Should_Set_Properties_Correctly()
  {
    // Arrange
    var id = "123";
    var fileName = "file.txt";
    var bucketName = "my-bucket";
    var downloadLink = "http://example.com/download/file.txt";
    var bucketLink = "http://example.com/bucket/file.txt";

    // Act
    var dto = new BucketResponseDto
    {
      Id = id,
      FileName = fileName,
      BucketName = bucketName,
      DownloadLink = downloadLink,
      BucketLink = bucketLink
    };

    // Assert
    Assert.Equal(id, dto.Id);
    Assert.Equal(fileName, dto.FileName);
    Assert.Equal(bucketName, dto.BucketName);
    Assert.Equal(downloadLink, dto.DownloadLink);
    Assert.Equal(bucketLink, dto.BucketLink);
  }
}
