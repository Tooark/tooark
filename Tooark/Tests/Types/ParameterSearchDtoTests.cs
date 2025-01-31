using Tooark.Types;

namespace Tooark.Tests.Types;

public class ParameterSearchDtoTests
{
  // Testa os valores padrão de ParameterSearchDto
  [Fact]
  public void ParameterSearchDto_DefaultValues()
  {
    // Arrange
    var searchDto = new TestParameterSearchDto();

    // Assert
    Assert.Null(searchDto.Search);
    Assert.Equal(0, searchDto.PageIndex);
    Assert.Equal(50, searchDto.PageSize);
  }

  // Testa os valores padrão de ParameterSearchOrderDto
  [Fact]
  public void ParameterSearchOrderDto_DefaultValues()
  {
    // Arrange
    var searchOrderDto = new TestParameterSearchOrderDto();

    // Assert
    Assert.Null(searchOrderDto.Order);
  }

  // Testa os valores padrão da classe Order
  [Fact]
  public void Order_DefaultValues()
  {
    // Arrange
    var order = new Order();

    // Assert
    Assert.Null(order.Field);
    Assert.True(order.Asc);
  }

  // Testa a atribuição de valores na classe Order
  [Fact]
  public void Order_SetValues()
  {
    // Arrange
    var order = new Order
    {
      Field = "Name",
      Asc = false
    };

    // Assert
    Assert.Equal("Name", order.Field);
    Assert.False(order.Asc);
  }
}

// Classes tests para permitir a instanciação e teste de classes abstratas
public class TestParameterSearchDto : ParameterSearchDto { }
public class TestParameterSearchOrderDto : ParameterSearchOrderDto { }
