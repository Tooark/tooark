using System.Reflection;
using Tooark.Dtos;

namespace Tooark.Tests.Dtos;

public class SearchOrderDtoTest
{
  // Cria uma classe que estende SearchOrderDto para testes
  private class TestSearchOrderDto : SearchOrderDto { }


  // Teste de valores padrão
  [Fact]
  public void SearchOrder_ShouldDefaultValue()
  {
    // Arrange & Act
    TestSearchOrderDto dto = new();

    // Assert
    Assert.Null(dto.OrderBy);
    Assert.True(dto.OrderAsc);
    Assert.Null(dto.Search);
    Assert.Equal(0, dto.PageIndex);
    Assert.Equal(50, dto.PageSize);
  }

  // Teste para definir valor do OrderBy
  [Fact]
  public void OrderBy_ShouldBeSettable()
  {
    // Arrange & Act
    TestSearchOrderDto dto = new();
    var expectedOrderBy = "TestField";

    // Act
    dto.OrderBy = expectedOrderBy;

    // Assert
    Assert.Equal(expectedOrderBy, dto.OrderBy);
    Assert.True(dto.OrderAsc);
    Assert.Null(dto.Search);
    Assert.Equal(0, dto.PageIndex);
    Assert.Equal(50, dto.PageSize);
  }

  // Teste para definir valor do OrderAsc
  [Fact]
  public void OrderAsc_ShouldBeSettable()
  {
    // Arrange & Act
    TestSearchOrderDto dto = new();
    var expectedOrderAsc = false;

    // Act
    dto.OrderAsc = expectedOrderAsc;

    // Assert
    Assert.Null(dto.OrderBy);
    Assert.False(dto.OrderAsc);
    Assert.Null(dto.Search);
    Assert.Equal(0, dto.PageIndex);
    Assert.Equal(50, dto.PageSize);
  }
}
