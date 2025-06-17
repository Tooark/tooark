using Tooark.Dtos;

namespace Tooark.Tests.Dtos;

public class SearchDtoTests
{
  // Teste de valores padrão
  [Fact]
  public void DefaultValues_ShouldBeCorrect()
  {
    // Arrange & Act
    var dto = new SearchDto();

    // Assert
    Assert.Null(dto.Search);
    Assert.Null(dto.SearchNormalized);
    Assert.Equal(1, dto.PageIndex);
    Assert.Equal(0, dto.PageIndexLogical);
    Assert.Equal(50, dto.PageSize);
  }

  // Teste de Paginação com valores negativos e positivos para valores válidos
  [Theory]
  [InlineData(-1, 10, 0, 0)]
  [InlineData(0, 20, 0, 0)]
  [InlineData(5, 30, 5, 4)]
  public void Pagination_ShouldBeSetCorrectly(int input, int size, int expected, int expectedLogical)
  {
    // Arrange & Act
    var dto = new SearchDto(input, size);

    // Assert
    Assert.Null(dto.Search);
    Assert.Null(dto.SearchNormalized);
    Assert.Equal(expected, dto.PageIndex);
    Assert.Equal(expectedLogical, dto.PageIndexLogical);
    Assert.Equal(size, dto.PageSize);
  }

  // Teste de Search
  [Theory]
  [InlineData("test", "test", "TEST")]
  [InlineData(null, null, null)]
  [InlineData("", "", "")]
  public void Search_ShouldBeSetCorrectly(string? searchInput, string? expectedSearch, string? expectedNormalized = null)
  {
    // Arrange & Act
    var dto = new SearchDto(searchInput);

    // Assert
    Assert.Equal(expectedSearch, dto.Search);
    Assert.Equal(expectedNormalized, dto.SearchNormalized);
    Assert.Equal(1, dto.PageIndex);
    Assert.Equal(0, dto.PageIndexLogical);
    Assert.Equal(50, dto.PageSize);
  }

  // Teste de Paginação e Search
  [Fact]
  public void Complete_ShouldBeSetCorrectly()
  {
    // Arrange & Act
    var dto = new SearchDto("test", 2, 100);

    // Assert
    Assert.Equal("test", dto.Search);
    Assert.Equal("TEST", dto.SearchNormalized);
    Assert.Equal(2, dto.PageIndex);
    Assert.Equal(1, dto.PageIndexLogical);
    Assert.Equal(100, dto.PageSize);
  }
}
