using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class DetailedEntityTests
{
  // Uma classe concreta para testar DetailedEntity
  private class FakeDetailedEntity : DetailedEntity
  { }

  [Fact]
  public void Constructor_AssignsDefaultValues()
  {
    // Arrange & Act
    var entity = new FakeDetailedEntity();

    // Assert
    Assert.Equal(Guid.Empty, entity.CreatedBy);
    Assert.Equal(Guid.Empty, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalSeconds < 1);
    Assert.True((DateTime.UtcNow - entity.UpdatedAt).TotalSeconds < 1);
  }

  [Fact]
  public void SetCreatedBy_ValidGuid_AssignsGuid()
  {
    // Arrange
    var entity = new FakeDetailedEntity();
    var createdBy = Guid.NewGuid();

    // Act
    entity.SetCreatedBy(createdBy);

    // Assert
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal(createdBy, entity.UpdatedBy);
  }

  [Fact]
  public void SetCreatedBy_NonEmptyGuid_ThrowsInvalidOperationException()
  {
    // Arrange
    var entity = new FakeDetailedEntity();
    var createdBy = Guid.NewGuid();
    entity.SetCreatedBy(createdBy);

    // Act & Assert
    var exception = Assert.Throws<InvalidOperationException>(() => entity.SetCreatedBy(Guid.NewGuid()));
    Assert.Equal("ChangeNotAllowed;CreatedBy", exception.Message);
  }

  [Fact]
  public void UpdateEntity_ValidGuid_UpdatesUpdatedByAndUpdatedAt()
  {
    // Arrange
    var entity = new FakeDetailedEntity();
    var updatedBy = Guid.NewGuid();

    // Act
    entity.UpdateEntity(updatedBy);

    // Assert
    Assert.Equal(updatedBy, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.UpdatedAt).TotalSeconds < 1);
  }
}
