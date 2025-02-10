using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class DetailedEntityTests
{
  // Uma classe concreta para testar DetailedEntity
  private class TestDetailedEntity : DetailedEntity
  { }

  // Testa se o construtor atribui valores padrão
  [Fact]
  public void Constructor_ShouldAssignDefaultValues()
  {
    // Arrange & Act
    var entity = new TestDetailedEntity();

    // Assert
    Assert.Equal(Guid.Empty, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.UpdatedAt).TotalMinutes < 1);
  }

  // Testa se SetCreatedBy atribui um Guid válido
  [Fact]
  public void SetCreatedBy_WithValidGuid_ShouldAssignGuid()
  {
    // Arrange
    var entity = new TestDetailedEntity();
    var createdBy = Guid.NewGuid();

    // Act
    entity.SetCreatedBy(createdBy);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal(createdBy, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalMinutes < 1);
  }

  // Testa se SetUpdatedBy atualiza o atualizador e a data de atualização
  [Fact]
  public void SetUpdatedBy_WithValidGuid_ShouldUpdateUpdatedByAndUpdatedAt()
  {
    // Arrange
    var entity = new TestDetailedEntity();
    var updatedBy = Guid.NewGuid();

    // Act
    entity.SetUpdatedBy(updatedBy);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(updatedBy, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.UpdatedAt).TotalMinutes < 1);
  }

  // Testa se SetCreatedBy lança uma exceção ao tentar atribuir um Guid vazio
  [Fact]
  public void SetCreatedBy_WithGuidEmpty_ShouldThrowArgumentException()
  {
    // Arrange
    var entity = new TestDetailedEntity();

    // Act
    entity.SetCreatedBy(Guid.Empty);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(Guid.Empty, entity.CreatedBy);
    Assert.Equal(Guid.Empty, entity.UpdatedBy);
    Assert.Equal("IdentifierEmpty;CreatedBy", entity.Notifications.First());
  }

  // Testa se SetCreatedBy lança uma exceção ao tentar alterar o criador
  [Fact]
  public void SetCreatedBy_WithNonEmptyGuid_ShouldThrowInvalidOperationException()
  {
    // Arrange
    var entity = new TestDetailedEntity();
    var createdBy = Guid.NewGuid();
    entity.SetCreatedBy(createdBy);

    // Act
    entity.SetCreatedBy(Guid.NewGuid());

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal(createdBy, entity.UpdatedBy);
    Assert.Equal("ChangeNotAllowed;CreatedBy", entity.Notifications.First());
  }

  // Testa se SetUpdatedBy lança uma exceção ao tentar atribuir um Guid vazio
  [Fact]
  public void SetUpdatedBy_WithGuidEmpty_ShouldThrowArgumentException()
  {
    // Arrange
    var entity = new TestDetailedEntity();
    var createdBy = Guid.NewGuid();
    entity.SetCreatedBy(createdBy);

    // Act
    entity.SetUpdatedBy(Guid.Empty);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal(createdBy, entity.UpdatedBy);
    Assert.Equal("IdentifierEmpty;UpdatedBy", entity.Notifications.First());
  }
}
