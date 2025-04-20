using Amazon.S3;
using Amazon.S3.Model;
using Google.Apis.Storage.v1.Data;
using Tooark.Storages.Dtos;

namespace Tooark.Tests.Storages.Dtos;

public class AclDtoTest
{
  // Teste de construtor com parâmetro S3Grant
  [Fact]
  public void Constructor_ShouldInitializeProperties_FromS3Grant()
  {
    // Arrange
    var grantee = new S3Grantee
    {
      DisplayName = "Test DisplayName",
      EmailAddress = "test@example.com",
      URI = "http://example.com"
    };
    var grant = new S3Grant
    {
      Grantee = grantee,
      Permission = S3Permission.FULL_CONTROL
    };

    // Act
    var aclDto = new AclDto(grant);

    // Assert
    Assert.Equal("Test DisplayName", aclDto.DisplayName);
    Assert.Equal("test@example.com", aclDto.EmailAddress);
    Assert.Equal("http://example.com", aclDto.Link);
    Assert.Equal("FULL_CONTROL", aclDto.Permission);
  }

  // Teste de construtor com parâmetro ObjectAccessControl
  [Fact]
  public void Constructor_ShouldInitializeProperties_FromObjectAccessControl()
  {
    // Arrange
    var objectAccessControl = new ObjectAccessControl
    {
      Entity = "Test Entity",
      Email = "test@example.com",
      SelfLink = "http://example.com",
      Kind = "storage#objectAccessControl",
      Role = "OWNER"
    };

    // Act
    var aclDto = new AclDto(objectAccessControl);

    // Assert
    Assert.Equal("Test Entity", aclDto.DisplayName);
    Assert.Equal("test@example.com", aclDto.EmailAddress);
    Assert.Equal("http://example.com", aclDto.Link);
    Assert.Equal("storage#objectAccessControl", aclDto.Type);
    Assert.Equal("OWNER", aclDto.Permission);
  }
}
