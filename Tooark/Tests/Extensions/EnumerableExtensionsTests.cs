using System.Text;
using Tooark.Extensions;
using Tooark.Tests.Moq.Model.Category;

namespace Tooark.Tests.Extensions;

public class EnumerableExtensionsTests
{
  private readonly List<Category> _categories;

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

  public static class TestDataGenerator
  {
    public static Category CreateCategory(int sequence)
    {
      bool includeOptional = sequence % 2 == 0;
      int valueChar = sequence * (includeOptional ? -1 : 1) + 77;

      var category = new Category
      {
        Id = sequence,
        Type = "Type " + new string(Encoding.Default.GetChars([(byte)valueChar])),
        Description = includeOptional ? "Description " + valueChar : null,
        CreatedAt = DateTime.UtcNow.AddDays(valueChar),
        SubCategory = includeOptional ? CreateSubCategory(sequence) : null,
        ListSubCategory = includeOptional ? new List<ListSubCategory> { CreateListSubCategory(sequence) } : null!
      };

      return category;
    }

    public static SubCategory CreateSubCategory(int sequence)
    {
      bool includeOptional = sequence % 2 == 0;

      var subCategory = new SubCategory
      {
        Id = sequence * 100 + sequence,
        Name = "Name " + sequence,
        Type = includeOptional ? sequence : null,
        CreatedAt = DateTime.UtcNow.AddDays(sequence),
      };

      return subCategory;
    }

    public static ListSubCategory CreateListSubCategory(int sequence)
    {
      bool includeOptional = sequence % 2 == 0;

      var listSubCategory = new ListSubCategory
      {
        Id = sequence * 1000 + sequence,
        Name = includeOptional ? "Name " + (sequence + 10 ^ sequence) : null,
        Type = includeOptional ? 1 : 0,
        CreatedAt = DateTime.UtcNow.AddDays(sequence)
      };

      return listSubCategory;
    }
  }

  // Teste para Ordenação Ascendente Simples
  [Fact]
  public void OrderByProperty_WithSimpleType_OrdersAscendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("Type").ToList();

    // Assert
    Assert.Equal("Type I", orderedResult[0].Type);
    Assert.Equal("Type K", orderedResult[1].Type);
    Assert.Equal("Type N", orderedResult[2].Type);
    Assert.Equal("Type P", orderedResult[3].Type);
    Assert.Equal("Type R", orderedResult[4].Type);
  }

  // Teste para Ordenação Descendente Simples
  [Fact]
  public void OrderByProperty_WithSimpleType_OrdersDescendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("Type", false).ToList();

    // Assert
    Assert.Equal("Type R", orderedResult[0].Type);
    Assert.Equal("Type P", orderedResult[1].Type);
    Assert.Equal("Type N", orderedResult[2].Type);
    Assert.Equal("Type K", orderedResult[3].Type);
    Assert.Equal("Type I", orderedResult[4].Type);
  }

  // Teste para Ordenação Ascendente com Campos Nulos
  [Fact]
  public void OrderByProperty_WithNullValues_OrdersAscendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("Description").ToList();

    // Assert
    Assert.Null(orderedResult[0].Description);
    Assert.Null(orderedResult[1].Description);
    Assert.Null(orderedResult[2].Description);
    Assert.Equal("Description 73", orderedResult[3].Description);
    Assert.Equal("Description 75", orderedResult[4].Description);
  }

  // Teste para Ordenação Descendente com Campos Nulos
  [Fact]
  public void OrderByProperty_WithNullValues_OrdersDescendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("Description", false).ToList();

    // Assert
    Assert.Equal("Description 75", orderedResult[0].Description);
    Assert.Equal("Description 73", orderedResult[1].Description);
    Assert.Null(orderedResult[2].Description);
    Assert.Null(orderedResult[3].Description);
    Assert.Null(orderedResult[4].Description);
  }

  // Teste para Ordenação Ascendente com Condição
  [Fact]
  public void OrderByProperty_WithCondition_OrdersAscendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("Type", propertyEquals: "Id", valueEquals: 1).ToList();

    // Assert
    Assert.Equal("Type I", orderedResult[0].Type);
  }

  // Teste para Ordenação Descendente com Condição
  [Fact]
  public void OrderByProperty_WithCondition_OrdersDescendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("Type", false, propertyEquals: "Id", valueEquals: 1).ToList();

    // Assert
    Assert.Equal("Type R", orderedResult[0].Type);
  }

  // Teste para Ordenação Ascendente de Propriedades Complexas
  [Fact]
  public void OrderByProperty_WithComplexType_OrdersAscendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("SubCategory.Name", true).ToList();

    // Assert
    Assert.Null(orderedResult[0].SubCategory?.Name);
    Assert.Null(orderedResult[1].SubCategory?.Name);
    Assert.Null(orderedResult[2].SubCategory?.Name);
    Assert.Equal("Name 2", orderedResult[3].SubCategory?.Name);
    Assert.Equal("Name 4", orderedResult[4].SubCategory?.Name);
  }

  // Teste para Ordenação Descendente de Propriedades Complexas
  [Fact]
  public void OrderByProperty_WithComplexType_OrdersDescendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("SubCategory.Name", false).ToList();

    // Assert
    Assert.Equal("Name 4", orderedResult[0].SubCategory?.Name);
    Assert.Equal("Name 2", orderedResult[1].SubCategory?.Name);
    Assert.Null(orderedResult[2].SubCategory?.Name);
    Assert.Null(orderedResult[3].SubCategory?.Name);
    Assert.Null(orderedResult[3].SubCategory?.Name);
  }

  // Teste para Ordenação Ascendente de Coleções Complexas
  [Fact]
  public void OrderByProperty_WithComplexCollection_OrdersAscendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("ListSubCategory.Name", true, "Type", 1).ToList();

    // Assert
    Assert.Null(orderedResult[0].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
    Assert.Null(orderedResult[1].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
    Assert.Null(orderedResult[2].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
    Assert.Equal("Name 10", orderedResult[3].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
    Assert.Equal("Name 14", orderedResult[4].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
  }

  // Teste para Ordenação Descendente de Coleções Complexas
  [Fact]
  public void OrderByProperty_WithComplexCollection_OrdersDescendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("ListSubCategory.Name", false, "Type", 1).ToList();

    // Assert
    Assert.Equal("Name 14", orderedResult[0].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
    Assert.Equal("Name 10", orderedResult[1].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
    Assert.Null(orderedResult[2].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
    Assert.Null(orderedResult[3].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
    Assert.Null(orderedResult[4].ListSubCategory?.FirstOrDefault(x => x.Type == 1)?.Name);
  }

  // Teste para Ordenação Ascendente com Campo Desconhecido
  [Fact]
  public void OrderByProperty_WithUnknownProperty_OrdersAscendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("Unknown").ToList();

    // Assert
    Assert.Equal(1, orderedResult[0].Id);
    Assert.Equal(2, orderedResult[1].Id);
    Assert.Equal(3, orderedResult[2].Id);
    Assert.Equal(4, orderedResult[3].Id);
    Assert.Equal(5, orderedResult[4].Id);
  }

  // Teste para Ordenação Descendente com Campo Desconhecido
  [Fact]
  public void OrderByProperty_WithUnknownProperty_OrdersDescendingCorrectly()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty("Unknown", false).ToList();

    // Assert
    Assert.Equal(5, orderedResult[0].Id);
    Assert.Equal(4, orderedResult[1].Id);
    Assert.Equal(3, orderedResult[2].Id);
    Assert.Equal(2, orderedResult[3].Id);
    Assert.Equal(1, orderedResult[4].Id);
  }

  // Teste para Ordenação com Campo Null
  [Fact]
  public void OrderByProperty_WithNullProperty_ListSize()
  {
    // Arrange & Act
    var orderedResult = _categories.OrderByProperty(null).ToList();

    // Assert
    Assert.Equal(5, orderedResult.Count);
  }

  // Teste para Ordenação lista Vazia
  [Fact]
  public void OrderByProperty_WithEmptyList_ListEmpty()
  {
    var _listNullCategory = new List<Category>();
    var orderedResult = _listNullCategory.OrderByProperty("Type").ToList();

    Assert.Empty(orderedResult);
  }

  // // Teste para Ordenação Descendente com Campo Null
  // [Fact]
  // public void OrderByProperty_WithNullProperty_OrdersDescendingCorrectly()
  // {
    

  //     Assert.Throws<ArgumentException>(() => _categories.OrderByProperty("InvalidProperty").ToList());
  // }
}
