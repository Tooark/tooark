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
    new()
    {
      TestDataGenerator.CreateCategory(1),
      TestDataGenerator.CreateCategory(2),
      TestDataGenerator.CreateCategory(3),
      TestDataGenerator.CreateCategory(4),
      TestDataGenerator.CreateCategory(5)
    };
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
        Type = "Type " + new string(Encoding.Default.GetChars(new byte[] { (byte)valueChar })),
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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("Type").ToList();


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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("Type", false).ToList();

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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("Description").ToList();

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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("Description", false).ToList();

    Assert.Equal("Description 75", orderedResult[0].Description);
    Assert.Equal("Description 73", orderedResult[1].Description);
    Assert.Null(orderedResult[2].Description);
    Assert.Null(orderedResult[3].Description);
    Assert.Null(orderedResult[4].Description);
  }

  // Teste para Ordenação Ascendente de Propriedades Complexas
  [Fact]
  public void OrderByProperty_WithComplexType_OrdersAscendingCorrectly()
  {
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("SubCategory.Name", true).ToList();

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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("SubCategory.Name", false).ToList();

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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("ListSubCategory.Name", true, "Type", 1).ToList();

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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("ListSubCategory.Name", false, "Type", 1).ToList();

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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("Unknown").ToList();

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
    var queryable = _categories.AsEnumerable();
    var orderedResult = queryable.OrderByProperty("Unknown", false).ToList();

    Assert.Equal(5, orderedResult[0].Id);
    Assert.Equal(4, orderedResult[1].Id);
    Assert.Equal(3, orderedResult[2].Id);
    Assert.Equal(2, orderedResult[3].Id);
    Assert.Equal(1, orderedResult[4].Id);
  }
}
