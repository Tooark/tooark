using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class BaseEntityTests
{
  // Uma classe concreta para testar BaseEntity
  private class TestEntity : BaseEntity
  { }

  // Testa se o construtor atribui um novo Guid não vazio
  [Fact]
  public void Constructor_ShouldAssignNewGuid()
  {
    // Arrange & Act
    var entity = new TestEntity();

    // Assert
    Assert.NotEqual(Guid.Empty, entity.Id);
  }

   // Testa se SetId atribui um Guid válido
  [Fact]
  public void SetId_WithValidGuid_ShouldAssignGuid()
  {
    // Arrange
    var entity = new TestEntity();
    var newId = Guid.NewGuid();

    // Act
    entity.SetId(newId);

    // Assert
    Assert.Equal(newId, entity.Id);
  }

  // Testa se SetId lança uma exceção ao receber um Guid vazio
  [Fact]
  public void SetId_WithEmptyGuid_ShouldThrowArgumentException()
  {
    // Arrange
    var entity = new TestEntity();

    // Act & Assert
    var exception = Assert.Throws<ArgumentException>(() => entity.SetId(Guid.Empty));
    Assert.Equal("IdentifierEmpty", exception.Message);
  }
}
