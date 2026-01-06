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

  // Teste se Equals(BaseEntity) retorna falso para null
  [Fact]
  public void Equals_BaseEntity_WithNull_ShouldReturnFalse()
  {
    // Arrange
    var entity = new TestEntity();
    BaseEntity? nullEntity = null;

    // Act
    var result = entity.Equals(nullEntity);

    // Assert
    Assert.False(result);
  }

  // Teste se Equals(BaseEntity) retorna verdadeiro para a mesma referência
  [Fact]
  public void Equals_BaseEntity_WithSameReference_ShouldReturnTrue()
  {
    // Arrange
    var entity = new TestEntity();

    // Act
    var result = entity.Equals(entity);

    // Assert
    Assert.True(result);
  }

  // Teste se Equals(BaseEntity) retorna verdadeiro para entidades com mesmo Id
  [Fact]
  public void Equals_BaseEntity_WithSameId_ShouldReturnTrue()
  {
    // Arrange
    var entity1 = new TestEntity();
    var entity2 = new TestEntity();
    entity2.SetId(entity1.Id);

    // Act
    var result = entity1.Equals(entity2);

    // Assert
    Assert.True(result);
  }

  // Teste se Equals(BaseEntity) retorna falso para entidades com Ids diferentes
  [Fact]
  public void Equals_BaseEntity_WithDifferentId_ShouldReturnFalse()
  {
    // Arrange
    var entity1 = new TestEntity();
    var entity2 = new TestEntity();

    // Act
    var result = entity1.Equals(entity2);

    // Assert
    Assert.False(result);
  }

  // Teste se operador == retorna verdadeiro para entidades iguais
  [Fact]
  public void OperatorEquals_WithEqualEntities_ShouldReturnTrue()
  {
    // Arrange
    var entity1 = new TestEntity();
    var entity2 = new TestEntity();
    entity2.SetId(entity1.Id);

    // Act
    var result = entity1 == entity2;

    // Assert
    Assert.True(result);
  }

  // Teste se operador == retorna falso para entidades diferentes
  [Fact]
  public void OperatorEquals_WithDifferentEntities_ShouldReturnFalse()
  {
    // Arrange
    var entity1 = new TestEntity();
    var entity2 = new TestEntity();

    // Act
    var result = entity1 == entity2;

    // Assert
    Assert.False(result);
  }

  // Teste se operador == retorna verdadeiro para ambos null
  [Fact]
  public void OperatorEquals_WithBothNull_ShouldReturnTrue()
  {
    // Arrange
    BaseEntity? entity1 = null;
    BaseEntity? entity2 = null;

    // Act
    var result = entity1 == entity2;

    // Assert
    Assert.True(result);
  }

  // Teste se operador != retorna verdadeiro para entidades diferentes
  [Fact]
  public void OperatorNotEquals_WithDifferentEntities_ShouldReturnTrue()
  {
    // Arrange
    var entity1 = new TestEntity();
    var entity2 = new TestEntity();

    // Act
    var result = entity1 != entity2;

    // Assert
    Assert.True(result);
  }

  // Teste se operador != retorna falso para entidades iguais
  [Fact]
  public void OperatorNotEquals_WithEqualEntities_ShouldReturnFalse()
  {
    // Arrange
    var entity1 = new TestEntity();
    var entity2 = new TestEntity();
    entity2.SetId(entity1.Id);

    // Act
    var result = entity1 != entity2;

    // Assert
    Assert.False(result);
  }

  // Teste se Equals(object) retorna verdadeiro para mesma entidade
  [Fact]
  public void Equals_Object_WithSameEntity_ShouldReturnTrue()
  {
    // Arrange
    var entity1 = new TestEntity();
    var entity2 = new TestEntity();
    entity2.SetId(entity1.Id);
    object obj = entity2;

    // Act
    var result = entity1.Equals(obj);

    // Assert
    Assert.True(result);
  }

  // Teste se Equals(object) retorna falso para objeto de tipo diferente
  [Fact]
  public void Equals_Object_WithDifferentType_ShouldReturnFalse()
  {
    // Arrange
    var entity = new TestEntity();
    object obj = "not an entity";

    // Act
    var result = entity.Equals(obj);

    // Assert
    Assert.False(result);
  }

  // Teste se Equals(object) retorna falso para null
  [Fact]
  public void Equals_Object_WithNull_ShouldReturnFalse()
  {
    // Arrange
    var entity = new TestEntity();
    object? obj = null;

    // Act
    var result = entity.Equals(obj);

    // Assert
    Assert.False(result);
  }
}
