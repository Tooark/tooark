using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class VersionedEntityTest
{
  // Uma classe concreta para testar VersionedEntity
  private class TestVersionedEntity : VersionedEntity
  { }

  // Testa se a versão é inicializada com 1
  [Fact]
  public void Version_ShouldBeInitializedToOne()
  {
    // Arrange
    var entity = new TestVersionedEntity();

    // Act
    var version = entity.Version;

    // Assert
    Assert.Equal(1, version);
  }

  // Testa se a versão é incrementada ao chamar SetUpdatedBy
  [Fact]
  public void SetUpdatedBy_ShouldIncrementVersion_WhenCalled()
  {
    // Arrange
    var entity = new TestVersionedEntity();
    var initialVersion = entity.Version;
    var updatedBy = Guid.NewGuid();

    // Act
    entity.SetUpdatedBy(updatedBy);
    var updatedVersion = entity.Version;

    // Assert
    Assert.Equal(initialVersion + 1, updatedVersion);
  }

  // Testa se a versão não é incrementada ao chamar SetUpdatedBy quando a entidade é inválida
  [Fact]
  public void SetUpdatedBy_ShouldNotIncrementVersion_WhenEntityIsInvalid()
  {
    // Arrange
    var entity = new TestVersionedEntity();
    var updatedBy = Guid.NewGuid();
    entity.SetUpdatedBy(updatedBy);
    var version = entity.Version;

    // Act
    entity.SetUpdatedBy(Guid.Empty);
    var updatedVersion = entity.Version;

    // Assert
    Assert.Equal(version, updatedVersion);
    Assert.False(entity.IsValid);
    Assert.Equal("IdentifierEmpty;UpdatedBy", entity.Notifications.First());
  }
}
