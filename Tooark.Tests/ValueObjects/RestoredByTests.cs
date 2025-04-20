using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class RestoredByTests
{
  // Teste criado para validar a criação de um RestoredBy válido.
  [Fact]
  public void Should_Restore_Valid_RestoredBy()
  {
    // Arrange
    var validGuid = Guid.NewGuid();

    // Act
    var restoredBy = new RestoredBy(validGuid);

    // Assert
    Assert.True(restoredBy.IsValid);
    Assert.Equal(validGuid, restoredBy.Value);
  }

  // Teste criado para validar a criação de um RestoredBy inválido.
  [Fact]
  public void Should_Not_Restore_RestoredBy_With_Empty_Guid()
  {
    // Arrange
    var emptyGuid = Guid.Empty;

    // Act
    var restoredBy = new RestoredBy(emptyGuid);

    // Assert
    Assert.False(restoredBy.IsValid);
    Assert.Equal("Field.Invalid;RestoredBy", restoredBy.Notifications.First().Message);
  }

  // Teste criado para validar a conversão implícita de um Guid para um RestoredBy.
  [Fact]
  public void Should_Implicitly_Convert_From_Guid_To_RestoredBy()
  {
    // Arrange
    var validGuid = Guid.NewGuid();

    // Act
    RestoredBy restoredBy = validGuid;

    // Assert
    Assert.True(restoredBy.IsValid);
    Assert.Equal(validGuid, restoredBy.Value);
  }

  // Teste criado para validar a conversão implícita de um RestoredBy para um Guid.
  [Fact]
  public void Should_Implicitly_Convert_From_RestoredBy_To_Guid()
  {
    // Arrange
    var validGuid = Guid.NewGuid();
    var restoredBy = new RestoredBy(validGuid);

    // Act
    Guid guid = restoredBy;

    // Assert
    Assert.True(restoredBy.IsValid);
    Assert.Equal(validGuid, guid);
  }
}
