using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class DeletedByTests
{
  // Teste criado para validar a criação de um DeletedBy válido.
  [Fact]
  public void Should_Delete_Valid_DeletedBy()
  {
    // Arrange
    var validGuid = Guid.NewGuid();

    // Act
    var deletedBy = new DeletedBy(validGuid);

    // Assert
    Assert.True(deletedBy.IsValid);
    Assert.Equal(validGuid, deletedBy.Value);
  }

  // Teste criado para validar a criação de um DeletedBy inválido.
  [Fact]
  public void Should_Not_Delete_DeletedBy_With_Empty_Guid()
  {
    // Arrange
    var emptyGuid = Guid.Empty;

    // Act
    var deletedBy = new DeletedBy(emptyGuid);

    // Assert
    Assert.False(deletedBy.IsValid);
    Assert.Equal("Field.Invalid;DeletedBy", deletedBy.Notifications.First().Message);
  }

  // Teste criado para validar a conversão implícita de um Guid para um DeletedBy.
  [Fact]
  public void Should_Implicitly_Convert_From_Guid_To_DeletedBy()
  {
    // Arrange
    var validGuid = Guid.NewGuid();

    // Act
    DeletedBy deletedBy = validGuid;

    // Assert
    Assert.True(deletedBy.IsValid);
    Assert.Equal(validGuid, deletedBy.Value);
  }

  // Teste criado para validar a conversão implícita de um DeletedBy para um Guid.
  [Fact]
  public void Should_Implicitly_Convert_From_DeletedBy_To_Guid()
  {
    // Arrange
    var validGuid = Guid.NewGuid();
    var deletedBy = new DeletedBy(validGuid);

    // Act
    Guid guid = deletedBy;

    // Assert
    Assert.True(deletedBy.IsValid);
    Assert.Equal(validGuid, guid);
  }
}
