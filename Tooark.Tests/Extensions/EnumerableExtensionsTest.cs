using System.Text;
using Tooark.Extensions;
using Tooark.Tests.Moq.Model.Category;

namespace Tooark.Tests.Extensions;

public class EnumerableExtensionsTests
{
  // Criando uma lista de categorias para testar os métodos de extensão
  private readonly List<Category> _categories;

  // Construtor para inicializar a lista de categorias
  public EnumerableExtensionsTests()
  {
    _categories =
    [
      TestDataGenerator.CreateCategory(1),
      TestDataGenerator.CreateCategory(2),
      TestDataGenerator.CreateCategory(3),
      TestDataGenerator.CreateCategory(4),
      TestDataGenerator.CreateCategory(5)
    ];
  }

  // Classe para gerar dados de teste
  public static class TestDataGenerator
  {
    public static Category CreateCategory(int sequence)
    {
      bool includeOptional = sequence % 2 == 1;
      int valueChar = sequence * (includeOptional ? -1 : 1) + 70;
      string letter = new(Encoding.Default.GetChars([(byte)valueChar]));
      DateTime date = new(2025, 1, 1);

      var category = new Category
      {
        Id = sequence,
        Type = "Type " + letter,
        Description = includeOptional ? "Description " + letter : null,
        CreatedAt = date.AddDays(sequence),
        SubCategory = includeOptional ? CreateSubCategory(sequence) : null,
        ListSubCategory = includeOptional ? new List<ListSubCategory> { CreateListSubCategory(sequence) } : null!
      };

      return category;
    }

    public static SubCategory CreateSubCategory(int sequence)
    {
      bool includeOptional = sequence % 2 == 1;
      int valueChar = sequence * (includeOptional ? -1 : 1) + 72;
      string letter = new(Encoding.Default.GetChars([(byte)valueChar]));

      var subCategory = new SubCategory
      {
        Id = sequence * 100 + sequence,
        Name = "Name " + letter,
        Type = includeOptional ? sequence : null,
        DeepCategory = includeOptional ? CreateDeepCategory(sequence) : null,
        CreatedAt = DateTime.UtcNow.AddDays(sequence),
      };

      return subCategory;
    }

    public static DeepCategory CreateDeepCategory(int sequence)
    {
      bool includeOptional = sequence % 2 == 1;
      int valueChar = sequence * (includeOptional ? -1 : 1) + 74;
      string letter = new(Encoding.Default.GetChars([(byte)valueChar]));

      var deepCategory = new DeepCategory
      {
        Id = sequence * 10000 + sequence,
        Name = "Name " + letter,
        CreatedAt = DateTime.UtcNow.AddDays(sequence),
      };

      return deepCategory;
    }

    public static ListSubCategory CreateListSubCategory(int sequence)
    {
      bool includeOptional = sequence % 2 == 1;
      int valueChar = sequence * (includeOptional ? -1 : 1) + 76;
      string letter = new(Encoding.Default.GetChars([(byte)valueChar]));

      var listSubCategory = new ListSubCategory
      {
        Id = sequence * 1000 + sequence,
        Name = includeOptional ? "Name " + letter : null,
        Type = includeOptional ? 1 : 0,
        DeepCategory = includeOptional ? CreateDeepCategory(sequence) : null,
        CreatedAt = DateTime.UtcNow.AddDays(sequence)
      };

      return listSubCategory;
    }
  }

  // Teste para ordenar por inteiro
  [Fact]
  public void OrderByProperty_SortsByIntProperty()
  {
    // Arrange & Act
    var result = _categories
      .OrderByProperty("Id")
      .Select(x => x.Id)
      .ToList();

    // Assert
    Assert.Equal(1, result[0]);
    Assert.Equal(2, result[1]);
    Assert.Equal(3, result[2]);
    Assert.Equal(4, result[3]);
    Assert.Equal(5, result[4]);
  }

  // Teste para ordenar por string
  [Fact]
  public void OrderByProperty_SortsByStringProperty()
  {
    // Arrange & Act
    var result = _categories
      .OrderByProperty("Type")
      .Select(x => x.Type)
      .ToList();

    // Assert
    Assert.Equal("Type A", result[0]);
    Assert.Equal("Type C", result[1]);
    Assert.Equal("Type E", result[2]);
    Assert.Equal("Type H", result[3]);
    Assert.Equal("Type J", result[4]);
  }

  // Teste para ordenar por data
  [Fact]
  public void OrderByProperty_SortsByDateTimeProperty()
  {
    // Arrange
    DateTime date = new(2025, 1, 1);
    
    // Act
    var result = _categories
      .OrderByProperty("CreatedAt")
      .Select(x => x.CreatedAt)
      .ToList();

    // Assert
    Assert.Equal(date.AddDays(1), result[0]);
    Assert.Equal(date.AddDays(2), result[1]);
    Assert.Equal(date.AddDays(3), result[2]);
    Assert.Equal(date.AddDays(4), result[3]);
    Assert.Equal(date.AddDays(5), result[4]);
  }

  // Teste para ordenar por uma propriedade de uma classe
  [Fact]
  public void OrderByProperty_SortsByClassProperty()
  {
    // Arrange & Act
    var result = _categories
      .OrderByProperty("SubCategory.Name")
      .Select(x => x.SubCategory?.Name)
      .ToList();

    // Assert
    Assert.Null(result[0]);
    Assert.Null(result[1]);
    Assert.Equal("Name C", result[2]);
    Assert.Equal("Name E", result[3]);
    Assert.Equal("Name G", result[4]);
  }

  // Teste para ordenar por uma propriedade de uma classe dentro de outra classe
  [Fact]
  public void OrderByProperty_SortsByClassDeepProperty()
  {
    // Arrange & Act
    var result = _categories
      .OrderByProperty("SubCategory.DeepCategory.Name")
      .Select(x => x.SubCategory?.DeepCategory?.Name)
      .ToList();

    // Assert
    Assert.Null(result[0]);
    Assert.Null(result[1]);
    Assert.Equal("Name E", result[2]);
    Assert.Equal("Name G", result[3]);
    Assert.Equal("Name I", result[4]);
  }

  // Teste para ordenar por uma propriedade dentro de uma lista
  [Fact]
  public void OrderByProperty_SortsByListProperty()
  {
    // Arrange & Act
    var result = _categories
      .OrderByProperty("ListSubCategory.Name")
      .Select(x => x.ListSubCategory?.FirstOrDefault()?.Name)
      .ToList();

    // Assert
    Assert.Null(result[0]);
    Assert.Null(result[1]);
    Assert.Equal("Name G", result[2]);
    Assert.Equal("Name I", result[3]);
    Assert.Equal("Name K", result[4]);
  }

  // Teste para ordenar por uma propriedade de uma classe dentro de uma lista
  [Fact]
  public void OrderByProperty_SortsByListDeepProperty()
  {
    // Arrange & Act
    var result = _categories
      .OrderByProperty("ListSubCategory.DeepCategory.Name")
      .Select(x => x.ListSubCategory?.FirstOrDefault()?.DeepCategory?.Name)
      .ToList();

    // Assert
    Assert.Null(result[0]);
    Assert.Null(result[1]);
    Assert.Equal("Name E", result[2]);
    Assert.Equal("Name G", result[3]);
    Assert.Equal("Name I", result[4]);
  }

  // Teste para ordenar por uma propriedade passando null
  [Fact]
  public void OrderByProperty_SortsByNullProperty()
  {
    // Arrange
    var categories = new List<Category> { new() { Id = 1 } };

    // Act
    var result = _categories
      .OrderByProperty(null!)
      .Select(x => x.Id)
      .ToList();

    // Assert
    Assert.Equal(categories.First().Id, result[0]);
  }

  // Teste para ordenar por uma propriedade passando vazio
  [Fact]
  public void OrderByProperty_SortsByEmptyProperty()
  {
    // Arrange
    var categories = new List<Category> { new() { Id = 1 } };

    // Act
    var result = _categories
      .OrderByProperty(string.Empty)
      .Select(x => x.Id)
      .ToList();

    // Assert
    Assert.Equal(categories.First().Id, result[0]);
  }

  // Teste para ordenar por uma propriedade passando propriedade inexistente
  [Fact]
  public void OrderByProperty_SortsByUnknownProperty()
  {
    // Arrange
    var categories = new List<Category> { new() { Id = 1 } };

    // Act
    var result = _categories
      .OrderByProperty("Unknown")
      .Select(x => x.Id)
      .ToList();

    // Assert
    Assert.Equal(categories.First().Id, result[0]);
  }
}
