using Tooark.Dtos;

namespace Tooark.Tests.Dtos;

public class SearchDtoTests
{
  // Cria uma classe que estende SearchDto para testes
  private class TestSearchDto : SearchDto { }
  

  // Teste de valores padrão
  [Fact]
  public void DefaultValues_ShouldBeCorrect()
  {
    // Arrange & Act
    var dto = new TestSearchDto();

    // Assert
    Assert.Equal(1, dto.PageIndex);
    Assert.Equal(0, dto.PageIndexLogical);
    Assert.Equal(50, dto.PageSize);
    Assert.Null(dto.Search);
  }

  // Teste de PageIndex com valores negativos e positivos para valores válidos
  [Theory]
  [InlineData(-1, 0, 0)]
  [InlineData(0, 0, 0)]
  [InlineData(5, 5, 4)]
  public void PageIndex_ShouldBeSetCorrectly(int input, int expected, int expectedLogical)
  {
    // Arrange & Act
    var dto = new TestSearchDto
    {
      PageIndex = input
    };

    // Assert
    Assert.Equal(expected, dto.PageIndex);
    Assert.Equal(expectedLogical, dto.PageIndexLogical);
  }

  // Teste de PageSize com valores negativos e positivos para valores válidos
  [Theory]
  [InlineData(-1, 0)]
  [InlineData(0, 0)]
  [InlineData(100, 100)]
  public void PageSize_ShouldBeSetCorrectly(int input, int expected)
  {
    // Arrange & Act
    var dto = new TestSearchDto
    {
      PageSize = input
    };

    // Assert
    Assert.Equal(expected, dto.PageSize);
  }

  // Teste de Search
  [Fact]
  public void Search_ShouldBeSetCorrectly()
  {
    // Arrange & Act
    var dto = new TestSearchDto
    {
      Search = "test"
    };

    // Assert
    Assert.Equal("test", dto.Search);
  }
}
