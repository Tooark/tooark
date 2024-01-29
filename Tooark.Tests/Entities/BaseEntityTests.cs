using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class BaseEntityTests
{
  // Uma classe concreta para testar BaseEntity
  private class FakeEntity : BaseEntity
  { }

  [Fact]
  public void Constructor_AssignsNewGuid()
  {
    // Arrange & Act
    var entity = new FakeEntity();

    // Assert
    Assert.NotEqual(Guid.Empty, entity.Id);
  }

  [Fact]
  public void SetId_ValidGuid_AssignsGuid()
  {
    // Arrange
    var entity = new FakeEntity();
    var newId = Guid.NewGuid();

    // Act
    entity.SetId(newId);

    // Assert
    Assert.Equal(newId, entity.Id);
  }

  [Fact]
  public void SetId_EmptyGuid_ThrowsArgumentException()
  {
    // Arrange
    var entity = new FakeEntity();

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => entity.SetId(Guid.Empty));
    Assert.Equal("IdentifierEmpty", exception.Message);
  }
}
