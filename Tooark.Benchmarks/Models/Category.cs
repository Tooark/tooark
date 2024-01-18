namespace Tooark.Benchmarks.Model;

public class Category
{
  public int Id { get; set; }
  public string Type { get; set; } = null!;
  public string? Description { get; set; }
  public DateTime CreatedAt { get; set; }

  public SubCategory? SubCategory { get; set; } = null!;
  public ICollection<ListSubCategory> ListSubCategory { get; set; } = null!;
}
