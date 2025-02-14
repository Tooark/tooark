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
    var dto = new TestSearchDto();

    Assert.Equal(0, dto.PageIndex);
    Assert.Equal(50, dto.PageSize);
    Assert.Null(dto.Search);
  }

  // Teste de PageIndex com valores negativos e positivos para valores válidos
  [Theory]
  [InlineData(-1, 0)]
  [InlineData(0, 0)]
  [InlineData(5, 5)]
  public void PageIndex_ShouldBeSetCorrectly(int input, int expected)
  {
    var dto = new TestSearchDto
    {
      PageIndex = input
    };

    Assert.Equal(expected, dto.PageIndex);
  }

  // Teste de PageSize com valores negativos e positivos para valores válidos
  [Theory]
  [InlineData(-1, 0)]
  [InlineData(0, 0)]
  [InlineData(100, 100)]
  public void PageSize_ShouldBeSetCorrectly(int input, int expected)
  {
    var dto = new TestSearchDto
    {
      PageSize = input
    };

    Assert.Equal(expected, dto.PageSize);
  }

  // Teste de Search
  [Fact]
  public void Search_ShouldBeSetCorrectly()
  {
    var dto = new TestSearchDto
    {
      Search = "test"
    };

    Assert.Equal("test", dto.Search);
  }
}
