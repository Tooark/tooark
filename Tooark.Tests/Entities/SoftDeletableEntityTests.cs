using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class SoftDeletableEntityTests
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
    Assert.Equal("Field.Invalid;UpdatedBy", entity.Notifications.First());
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
    Assert.Equal("Field.Invalid;UpdatedBy", entity.Notifications.First());
  }

  // Teste para verificar se a alteração não é permitida quando a entidade está excluída
  [Fact]
  public void ChangeNotAllowedIsDeleted_ShouldAddNotification_WhenEntityIsDeleted()
  {
    // Arrange
    var entity = new TestSoftDeletableEntity();
    var userId = Guid.NewGuid();
    entity.SetDeleted(userId);

    // Act
    entity.ChangeNotAllowedIsDeleted();

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal("Entity", entity.Notifications.First().Key);
  }

  // Teste para não marcar a entidade como excluída quando já está excluída
  [Fact]
  public void SetDeleted_ShouldNotChangeState_WhenAlreadyDeleted()
  {
    // Arrange
    var userId = Guid.NewGuid();
    var entity = new TestSoftDeletableEntity(userId);
    entity.SetDeleted(userId);

    // Act
    entity.SetDeleted(userId);

    // Assert
    Assert.True(entity.Deleted);
    Assert.Equal(userId, entity.UpdatedBy);
    Assert.Empty(entity.Notifications);
  }

  // Teste para não restaurar a entidade quando já está restaurada
  [Fact]
  public void SetRestored_ShouldNotChangeState_WhenAlreadyRestored()
  {
    // Arrange
    var userId = Guid.NewGuid();
    var entity = new TestSoftDeletableEntity(userId);
    entity.SetDeleted(userId);
    entity.SetRestored(userId);

    // Act
    entity.SetRestored(userId);

    // Assert
    Assert.False(entity.Deleted);
    Assert.Equal(userId, entity.UpdatedBy);
    Assert.Empty(entity.Notifications);
  }
}
