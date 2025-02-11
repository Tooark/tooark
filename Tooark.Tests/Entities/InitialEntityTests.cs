using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class InitialEntityTests
{
  // Uma classe concreta para testar InitialEntity
  private class TestInitialEntity : InitialEntity
  {
    // Construtor padrão
    public TestInitialEntity() { }

    // Construtor com parâmetros
    public TestInitialEntity(Guid createdBy) : base(createdBy) { }
  }

  // Teste se o construtor atribui valores padrão
  [Fact]
  public void Constructor_ShouldAssignDefaultValues()
  {
    // Arrange
    var createdBy = Guid.Empty;

    // Act
    var entity = new TestInitialEntity();

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalMinutes < 1);
  }

  // Teste se o construtor atribui valores padrão com um criador
  [Fact]
  public void Constructor_ShouldAssignValues_WithCreatedBy()
  {
    // Arrange
    var createdBy = Guid.NewGuid();
    
    // Act
    var entity = new TestInitialEntity(createdBy);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalMinutes < 1);
  }

  // Teste se SetCreatedBy atribui um Guid válido
  [Fact]
  public void SetCreatedBy_WithValidGuid_ShouldAssignGuid()
  {
    // Arrange
    var entity = new TestInitialEntity();
    var createdBy = Guid.NewGuid();

    // Act
    entity.SetCreatedBy(createdBy);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalMinutes < 1);
  }

  // Teste se SetCreatedBy gera uma notificação ao tentar atribuir um Guid vazio
  [Fact]
  public void SetCreatedBy_WithGuidEmpty_ShouldGenerateNotification()
  {
    // Arrange
    var createdBy = Guid.Empty;
    var entity = new TestInitialEntity();

    // Act
    entity.SetCreatedBy(createdBy);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal("IdentifierEmpty;CreatedBy", entity.Notifications.First());
  }

  // Teste se SetCreatedBy lança uma exceção ao tentar alterar o criador
  [Fact]
  public void SetCreatedBy_WithNonEmptyGuid_ShouldThrowInvalidOperationException()
  {
    // Arrange
    var createdBy = Guid.NewGuid();
    var entity = new TestInitialEntity();
    entity.SetCreatedBy(createdBy);

    // Act
    entity.SetCreatedBy(Guid.NewGuid());

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal("ChangeNotAllowed;CreatedBy", entity.Notifications.First());
  }
}
