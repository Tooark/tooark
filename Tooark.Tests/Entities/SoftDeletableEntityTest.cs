using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class SoftDeletableEntityTest
{
  // Uma classe concreta para testar SoftDeletableEntity
  private class TestSoftDeletableEntity : SoftDeletableEntity
  {
    // Construtor padrão
    public TestSoftDeletableEntity() { }

    // Construtor com parâmetros
    public TestSoftDeletableEntity(Guid createdBy) : base(createdBy) { }
  }

  // Testa se o construtor atribui valores padrão
  [Fact]
  public void Constructor_ShouldAssignDefaultValues()
  {
    // Arrange
    var createdBy = Guid.Empty;

    // Act
    var entity = new TestSoftDeletableEntity();

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.False(entity.Deleted);
  }

  // Teste se o construtor atribui valores padrão com um criador
  [Fact]
  public void Constructor_ShouldAssignValues_WithCreatedBy()
  {
    // Arrange
    var createdBy = Guid.NewGuid();

    // Act
    var entity = new TestSoftDeletableEntity(createdBy);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.False(entity.Deleted);
  }

  // Teste para marcar a entidade como excluída
  [Fact]
  public void SetDeleted_ShouldMarkEntityAsDeleted()
  {
    // Arrange
    var userId = Guid.NewGuid();
    var entity = new TestSoftDeletableEntity(userId);

    // Act
    entity.SetDeleted(userId);

    // Assert
    Assert.True(entity.IsValid);
    Assert.True(entity.Deleted);
    Assert.Equal(userId, entity.UpdatedBy);
  }

  // Teste para não marcar a entidade como excluída quando o usuário é vazio
  [Fact]
  public void SetDeleted_ShouldNotMarkEntityAsDeleted_WhenUserIdIsEmpty()
  {
    // Arrange
    var entity = new TestSoftDeletableEntity();
    var userId = Guid.Empty;

    // Act
    entity.SetDeleted(userId);

    // Assert
    Assert.False(entity.IsValid);
    Assert.False(entity.Deleted);
    Assert.Equal("IdentifierEmpty;ChangedBy", entity.Notifications.First());
  }

  // Teste para marcar a entidade como restaurada
  [Fact]
  public void SetRestored_ShouldMarkEntityAsNotDeleted()
  {
    // Arrange
    var entity = new TestSoftDeletableEntity();
    var userId = Guid.NewGuid();
    entity.SetDeleted(userId);

    // Act
    entity.SetRestored(userId);

    // Assert
    Assert.True(entity.IsValid);
    Assert.False(entity.Deleted);
  }

  // Teste para não marcar a entidade como restaurada quando o usuário é vazio
  [Fact]
  public void SetRestored_ShouldNotMarkEntityAsNotDeleted_WhenUserIdIsEmpty()
  {
    // Arrange
    var entity = new TestSoftDeletableEntity();
    var userId = Guid.NewGuid();
    entity.SetDeleted(userId);

    // Act
    entity.SetRestored(Guid.Empty);

    // Assert
    Assert.False(entity.IsValid);
    Assert.True(entity.Deleted);
    Assert.Equal("IdentifierEmpty;ChangedBy", entity.Notifications.First());
  }
}
