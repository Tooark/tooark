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
    Assert.Equal(Guid.Empty, entity.CreatedBy);
    Assert.Equal(Guid.Empty, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalSeconds < 1);
    Assert.True((DateTime.UtcNow - entity.UpdatedAt).TotalSeconds < 1);
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
    Assert.Equal(createdBy, entity.CreatedBy);
    Assert.Equal(createdBy, entity.UpdatedBy);
  }

  // Testa se SetCreatedBy lança uma exceção ao tentar alterar o criador
  [Fact]
  public void SetCreatedBy_WithNonEmptyGuid_ShouldThrowInvalidOperationException()
  {
    // Arrange
    var entity = new TestDetailedEntity();
    var createdBy = Guid.NewGuid();
    entity.SetCreatedBy(createdBy);

    // Act & Assert
    var exception = Assert.Throws<InvalidOperationException>(() => entity.SetCreatedBy(Guid.NewGuid()));
    Assert.Equal("ChangeNotAllowed;CreatedBy", exception.Message);
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
    Assert.Equal(updatedBy, entity.UpdatedBy);
    Assert.True((DateTime.UtcNow - entity.UpdatedAt).TotalSeconds < 1);
  }
}
