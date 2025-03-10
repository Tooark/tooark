namespace Tooark.Tests.Moq.Entities.Category;

public class SubCategory
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public int? Type { get; set; }
  public DateTime CreatedAt { get; set; }

  public DeepCategory? DeepCategory { get; set; }
}
