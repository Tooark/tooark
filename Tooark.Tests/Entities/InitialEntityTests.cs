using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class InitialEntityTests
{
  // Uma classe concreta para testar InitialEntity
  private class TestInitialEntity : InitialEntity
  { }

  // Testa se o construtor atribui valores padrão
  [Fact]
  public void Constructor_ShouldAssignDefaultValues()
  {
    // Arrange & Act
    var entity = new TestInitialEntity();

    // Assert
    Assert.Equal(Guid.Empty, entity.CreatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalMinutes < 1);
  }

  // Testa se SetCreatedBy atribui um Guid válido
  [Fact]
  public void SetCreatedBy_WithValidGuid_ShouldAssignGuid()
  {
    // Arrange
    var entity = new TestInitialEntity();
    var createdBy = Guid.NewGuid();

    // Act
    entity.SetCreatedBy(createdBy);

    // Assert
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalMinutes < 1);
  }

  // Testa se SetCreatedBy lança uma exceção ao tentar atribuir um Guid vazio
  [Fact]
  public void SetCreatedBy_WithGuidEmpty_ShouldThrowArgumentException()
  {
    // Arrange
    var entity = new TestInitialEntity();

    // Act
    entity.SetCreatedBy(Guid.Empty);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(Guid.Empty, entity.CreatedBy);
    Assert.Equal("IdentifierEmpty;CreatedBy", entity.Notifications.First());
  }

  // Testa se SetCreatedBy lança uma exceção ao tentar alterar o criador
  [Fact]
  public void SetCreatedBy_WithNonEmptyGuid_ShouldThrowInvalidOperationException()
  {
    // Arrange
    var entity = new TestInitialEntity();
    var createdBy = Guid.NewGuid();
    entity.SetCreatedBy(createdBy);

    // Act
    entity.SetCreatedBy(Guid.NewGuid());

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal("ChangeNotAllowed;CreatedBy", entity.Notifications.First());
  }
}
