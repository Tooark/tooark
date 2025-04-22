using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class DetailedEntityTests
{
  // Uma classe concreta para testar DetailedEntity
  private class TestDetailedEntity : DetailedEntity
  {
    // Construtor padrão
    public TestDetailedEntity() { }

    // Construtor com parâmetros
    public TestDetailedEntity(Guid createdBy) : base(createdBy) { }
  }

  // Teste se o construtor atribui valores padrão
  [Fact]
  public void Constructor_ShouldAssignDefaultValues()
  {
    // Arrange
    var createdBy = Guid.Empty;

    // Act
    var entity = new TestDetailedEntity();

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal(createdBy, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.UpdatedAt).TotalMinutes < 1);
  }

  // Teste se o construtor atribui valores padrão com um criador
  [Fact]
  public void Constructor_ShouldAssignValues_WithCreatedBy()
  {
    // Arrange
    var createdBy = Guid.NewGuid();

    // Act
    var entity = new TestDetailedEntity(createdBy);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal(createdBy, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalMinutes < 1);
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

  // Testa se SetCreatedBy gera uma notificação ao tentar atribuir um Guid vazio
  [Fact]
  public void SetCreatedBy_WithGuidEmpty_ShouldGenerateNotification()
  {
    // Arrange
    var entity = new TestDetailedEntity();

    // Act
    entity.SetCreatedBy(Guid.Empty);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(Guid.Empty, entity.CreatedBy);
    Assert.Equal(Guid.Empty, entity.UpdatedBy);
    Assert.Equal("Field.Invalid;CreatedBy", entity.Notifications.First());
  }

  // Testa se SetCreatedBy gera uma notificação ao tentar alterar o criador
  [Fact]
  public void SetCreatedBy_WithNonEmptyGuid_ShouldGenerateNotification()
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

  // Testa se SetUpdatedBy gera uma notificação ao tentar atribuir um Guid vazio
  [Fact]
  public void SetUpdatedBy_WithGuidEmpty_ShouldGenerateNotification()
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
    Assert.Equal("Field.Invalid;UpdatedBy", entity.Notifications.First());
  }
}
