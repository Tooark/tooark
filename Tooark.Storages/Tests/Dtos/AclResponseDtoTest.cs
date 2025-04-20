using Amazon.S3;
using Amazon.S3.Model;
using Google.Apis.Storage.v1.Data;
using Tooark.Storages.Dtos;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace Tooark.Tests.Storages.Dtos;

public class AclResponseDtoTest
{
  // Teste construtor com parâmetro GetACLResponse
  [Fact]
  public void Constructor_ShouldInitializeProperties_WithAmazonS3Response()
  {
    // Arrange
    var grants = new List<S3Grant>
    {
      new() { Grantee = new S3Grantee { DisplayName = "User1" }, Permission = S3Permission.FULL_CONTROL },
      new() { Grantee = new S3Grantee { DisplayName = "User2" }, Permission = S3Permission.READ }
    };
    var aclResponse = new GetACLResponse
    {
      AccessControlList = new S3AccessControlList
      {
        Grants = grants,
        Owner = new Owner { DisplayName = "Owner1" }
      }
    };

    // Act
    var dto = new AclResponseDto(aclResponse);

    // Assert
    Assert.Equal(2, dto.Acls.Length);
    Assert.Equal("Owner1", dto.Owner);
    Assert.Contains(dto.Acls, a => a.DisplayName == "User1" && a.Permission == "FULL_CONTROL");
    Assert.Contains(dto.Acls, a => a.DisplayName == "User2" && a.Permission == "READ");
  }

  // Teste construtor com parâmetro Google.Apis.Storage.v1.Data.Object
  [Fact]
  public void Constructor_ShouldInitializeProperties_WithGoogleCloudStorageResponse()
  {
    // Arrange
    var acl = new List<ObjectAccessControl>
    {
      new() { Entity = "User1", Role = "OWNER" },
      new() { Entity = "User2", Role = "READER" }
    };
    var aclResponse = new Object
    {
      Acl = acl,
      Owner = new Object.OwnerData { Entity = "Owner1" }
    };

    // Act
    var dto = new AclResponseDto(aclResponse);

    // Assert
    Assert.Equal(2, dto.Acls.Length);
    Assert.Equal("Owner1", dto.Owner);
    Assert.Contains(dto.Acls, a => a.DisplayName == "User1" && a.Permission == "OWNER");
    Assert.Contains(dto.Acls, a => a.DisplayName == "User2" && a.Permission == "READER");
  }
}
