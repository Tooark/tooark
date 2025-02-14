using Tooark.Dtos;

namespace Tooark.Tests.Dtos;

public class DtoTests
{
  // Classe de objeto de valor de teste
  private class TestDto : Dto
  { }

  // Testa se a classe de objeto de valor herda de DTO
  [Fact]
  public void Dto_ShouldInheritFromNotification()
  {
    // Arrange & Act
    var dto = new TestDto();

    // Assert
    Assert.IsAssignableFrom<Dto>(dto);
  }
}
