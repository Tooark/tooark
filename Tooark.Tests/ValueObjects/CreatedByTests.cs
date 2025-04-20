using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class CreatedByTests
{
  // Teste criado para validar a criação de um CreatedBy válido.
  [Fact]
  public void Should_Create_Valid_CreatedBy()
  {
    // Arrange
    var validGuid = Guid.NewGuid();

    // Act
    var createdBy = new CreatedBy(validGuid);

    // Assert
    Assert.True(createdBy.IsValid);
    Assert.Equal(validGuid, createdBy.Value);
  }

  // Teste criado para validar a criação de um CreatedBy inválido.
  [Fact]
  public void Should_Not_Create_CreatedBy_With_Empty_Guid()
  {
    // Arrange
    var emptyGuid = Guid.Empty;

    // Act
    var createdBy = new CreatedBy(emptyGuid);

    // Assert
    Assert.False(createdBy.IsValid);
    Assert.Equal("Field.Invalid;CreatedBy", createdBy.Notifications.First().Message);
  }

  // Teste criado para validar a conversão implícita de um Guid para um CreatedBy.
  [Fact]
  public void Should_Implicitly_Convert_From_Guid_To_CreatedBy()
  {
    // Arrange
    var validGuid = Guid.NewGuid();

    // Act
    CreatedBy createdBy = validGuid;

    // Assert
    Assert.True(createdBy.IsValid);
    Assert.Equal(validGuid, createdBy.Value);
  }

  // Teste criado para validar a conversão implícita de um CreatedBy para um Guid.
  [Fact]
  public void Should_Implicitly_Convert_From_CreatedBy_To_Guid()
  {
    // Arrange
    var validGuid = Guid.NewGuid();
    var createdBy = new CreatedBy(validGuid);

    // Act
    Guid guid = createdBy;

    // Assert
    Assert.True(createdBy.IsValid);
    Assert.Equal(validGuid, guid);
  }
}
