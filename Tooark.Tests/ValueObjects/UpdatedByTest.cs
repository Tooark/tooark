using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class UpdatedByTest
{
  // Teste criado para validar a criação de um UpdatedBy válido.
  [Fact]
  public void Should_Update_Valid_UpdatedBy()
  {
    // Arrange
    var validGuid = Guid.NewGuid();

    // Act
    var updatedBy = new UpdatedBy(validGuid);

    // Assert
    Assert.True(updatedBy.IsValid);
    Assert.Equal(validGuid, updatedBy.Value);
  }

  // Teste criado para validar a criação de um UpdatedBy inválido.
  [Fact]
  public void Should_Not_Update_UpdatedBy_With_Empty_Guid()
  {
    // Arrange
    var emptyGuid = Guid.Empty;

    // Act
    var updatedBy = new UpdatedBy(emptyGuid);

    // Assert
    Assert.False(updatedBy.IsValid);
    Assert.Equal("Field.Invalid;UpdatedBy", updatedBy.Notifications.First().Message);
  }

  // Teste criado para validar a conversão implícita de um Guid para um UpdatedBy.
  [Fact]
  public void Should_Implicitly_Convert_From_Guid_To_UpdatedBy()
  {
    // Arrange
    var validGuid = Guid.NewGuid();

    // Act
    UpdatedBy updatedBy = validGuid;

    // Assert
    Assert.True(updatedBy.IsValid);
    Assert.Equal(validGuid, updatedBy.Value);
  }

  // Teste criado para validar a conversão implícita de um UpdatedBy para um Guid.
  [Fact]
  public void Should_Implicitly_Convert_From_UpdatedBy_To_Guid()
  {
    // Arrange
    var validGuid = Guid.NewGuid();
    var updatedBy = new UpdatedBy(validGuid);

    // Act
    Guid guid = updatedBy;

    // Assert
    Assert.True(updatedBy.IsValid);
    Assert.Equal(validGuid, guid);
  }
}
