using Tooark.Entities;

namespace Tooark.Tests.Entities;

public class BaseEntityTests
{
  // Uma classe concreta para testar BaseEntity
  private class TestEntity : BaseEntity
  { }

  // Teste se o construtor atribui um novo Guid não vazio
  [Fact]
  public void Constructor_ShouldAssignNewGuid()
  {
    // Arrange & Act
    var entity = new TestEntity();

    // Assert
    Assert.True(entity.IsValid);
    Assert.NotEqual(Guid.Empty, entity.Id);
  }

   // Teste se SetId atribui um Guid válido
  [Fact]
  public void SetId_WithValidGuid_ShouldAssignGuid()
  {
    // Arrange
    var entity = new TestEntity();
    var newId = Guid.NewGuid();

    // Act
    entity.SetId(newId);

    // Assert
    Assert.True(entity.IsValid);
    Assert.Equal(newId, entity.Id);
  }

  // Teste se SetId gera uma notificação ao tentar atribuir um Guid vazio
  [Fact]
  public void SetId_WithEmptyGuid_ShouldGenerateNotification()
  {
    // Arrange
    var entity = new TestEntity();
    var originalId = entity.Id;

    // Act
    entity.SetId(Guid.Empty);

    // Assert
    Assert.False(entity.IsValid);
    Assert.Equal("IdentifierEmpty;Id", entity.Notifications.First());
    Assert.Equal(originalId, entity.Id);
  }

  // Teste se Equals retorna verdadeiro para o mesmo Guid
  [Fact]
  public void Equals_WithSameGuid_ShouldReturnTrue()
  {
    // Arrange
    var entity = new TestEntity();
    var sameId = entity.Id;

    // Act
    var result = entity.Equals(sameId);

    // Assert
    Assert.True(result);
  }

  // Teste se Equals retorna falso para um Guid diferente
  [Fact]
  public void Equals_WithDifferentGuid_ShouldReturnFalse()
  {
    // Arrange
    var entity = new TestEntity();
    var differentId = Guid.NewGuid();

    // Act
    var result = entity.Equals(differentId);

    // Assert
    Assert.False(result);
  }

  // Teste se GetHashCode retorna o hash correto do Id
  [Fact]
  public void GetHashCode_ShouldReturnIdHashCode()
  {
    // Arrange
    var entity = new TestEntity();
    var expectedHashCode = entity.Id.GetHashCode();

    // Act
    var result = entity.GetHashCode();

    // Assert
    Assert.Equal(expectedHashCode, result);
  }
}
