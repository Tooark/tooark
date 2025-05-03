using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class AuditableEntityTests
{
  // Uma classe concreta para testar AuditableEntity
  private class TestAuditableEntity : AuditableEntity
  {
    // Construtor padrão
    public TestAuditableEntity() { }

    // Construtor com parâmetros
    public TestAuditableEntity(Guid createdBy) : base(createdBy) { }
  }

  // Teste se o construtor atribui valores padrão
  [Fact]
  public void Constructor_ShouldAssignDefaultValues()
  {
    // Arrange
    var userId = Guid.Empty;

    // Act
    var entity = new TestAuditableEntity();

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(userId, entity.CreatedBy);
    Assert.Equal(userId, entity.UpdatedBy);
    Assert.Equal(1, entity.Version);
    Assert.False(entity.Deleted);
    Assert.Equal(userId, entity.DeletedBy);
    Assert.Null(entity.DeletedAt);
  }

  // Teste se o construtor atribui valores padrão com um criador
  [Fact]
  public void Constructor_ShouldAssignValues_WithCreatedBy()
  {
    // Arrange
    var userId = Guid.NewGuid();

    // Act
    var entity = new TestAuditableEntity(userId);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(userId, entity.CreatedBy);
    Assert.Equal(userId, entity.UpdatedBy);
    Assert.Equal(1, entity.Version);
    Assert.False(entity.Deleted);
    Assert.Equal(Guid.Empty, entity.DeletedBy);
    Assert.Null(entity.DeletedAt);
  }

  // Teste para marcar a entidade como excluída
  [Fact]
  public void SetDeleted_ShouldSetDeletedByAndDeletedAt()
  {
    // Arrange
    var entity = new TestAuditableEntity();
    var userId = Guid.NewGuid();

    // Act
    entity.SetDeleted(userId);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(userId, entity.UpdatedBy);
    Assert.Equal(2, entity.Version);
    Assert.True(entity.Deleted);
    Assert.Equal(userId, entity.DeletedBy);
    Assert.NotNull(entity.DeletedAt);
  }

  // Teste se SetDeleted não atribui DeletedBy quando o usuário é vazio
  [Fact]
  public void SetDeleted_ShouldNotSetDeletedBy_WhenUserIdIsEmpty()
  {
    // Arrange
    var entity = new TestAuditableEntity();
    var userId = Guid.Empty;

    // Act
    entity.SetDeleted(userId);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(userId, entity.UpdatedBy);
    Assert.Equal(1, entity.Version);
    Assert.False(entity.Deleted);
    Assert.Equal(userId, entity.DeletedBy);
    Assert.Null(entity.DeletedAt);
    Assert.Equal("Field.Invalid;DeletedBy", entity.Notifications.First());
  }

  // Teste se SetRestored atribui valores corretos
  [Fact]
  public void SetRestored_ShouldSetRestoredByAndRestoredAt()
  {
    // Arrange
    var entity = new TestAuditableEntity();
    var deletedBy = Guid.NewGuid();
    entity.SetDeleted(deletedBy);
    var userId = Guid.NewGuid();

    // Act
    entity.SetRestored(userId);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(userId, entity.UpdatedBy);
    Assert.Equal(3, entity.Version);
    Assert.False(entity.Deleted);
    Assert.Equal(userId, entity.RestoredBy);
    Assert.NotNull(entity.RestoredAt);
  }

  // Teste se SetRestored não atribui RestoredBy quando o usuário é vazio
  [Fact]
  public void SetRestored_ShouldNotSetRestoredBy_WhenUserIdIsEmpty()
  {
    // Arrange
    var entity = new TestAuditableEntity();
    var deletedBy = Guid.NewGuid();
    entity.SetDeleted(deletedBy);
    var userId = Guid.Empty;

    // Act
    entity.SetRestored(userId);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(deletedBy, entity.UpdatedBy);
    Assert.Equal(2, entity.Version);
    Assert.True(entity.Deleted);
    Assert.Equal(userId, entity.RestoredBy);
    Assert.Null(entity.RestoredAt);
    Assert.Equal("Field.Invalid;RestoredBy", entity.Notifications.First());
  }

  // Teste para verificar se ChangeNotAllowedIsDeleted adiciona notificação quando a entidade está excluída
  [Fact]
  public void ChangeNotAllowedIsDeleted_ShouldAddNotification_WhenEntityIsDeleted()
  {
    // Arrange
    var entity = new TestAuditableEntity();
    var userId = Guid.NewGuid();
    entity.SetDeleted(userId);

    // Act
    entity.ChangeNotAllowedIsDeleted();

    // Assert
    Assert.False(entity.IsValid);
    Assert.Contains(entity.Notifications, n => n.Key == "ChangeNotAllowedIsDeleted");
  }

  // Teste para verificar se ChangeNotAllowedIsDeleted não adiciona notificação quando a entidade não está excluída
  [Fact]
  public void ChangeNotAllowedIsDeleted_ShouldNotAddNotification_WhenEntityIsNotDeleted()
  {
    // Arrange
    var entity = new TestAuditableEntity();

    // Act
    entity.ChangeNotAllowedIsDeleted();

    // Assert
    Assert.True(entity.IsValid);
    Assert.Empty(entity.Notifications);
  }

  // Teste para SetUpdatedBy incrementando a versão e atualizando o UpdatedBy
  [Fact]
  public void SetUpdatedBy_ShouldIncrementVersionAndSetUpdatedBy()
  {
    // Arrange
    var entity = new TestAuditableEntity();
    var userId = Guid.NewGuid();

    // Act
    entity.SetUpdatedBy(userId);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(userId, entity.UpdatedBy);
    Assert.Equal(2, entity.Version);
  }

  // Teste para SetUpdatedBy não alterar a entidade quando o identificador é inválido
  [Fact]
  public void SetUpdatedBy_ShouldNotUpdate_WhenUserIdIsInvalid()
  {
    // Arrange
    var entity = new TestAuditableEntity();
    var userId = Guid.Empty;

    // Act
    entity.SetUpdatedBy(userId);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(Guid.Empty, entity.UpdatedBy);
    Assert.Equal(1, entity.Version);
    Assert.Equal("Field.Invalid;UpdatedBy", entity.Notifications.First());
  }
}
