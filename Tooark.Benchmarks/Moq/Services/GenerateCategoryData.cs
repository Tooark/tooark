using System.Text;
using Tooark.Benchmarks.Moq.Models.Category;

namespace Tooark.Benchmarks.Moq.Services;

public class GenerateCategoryData
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

  private static SubCategory CreateSubCategory(int sequence)
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

  private static ListSubCategory CreateListSubCategory(int sequence)
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
