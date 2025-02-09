namespace Tooark.Tests.Moq.Model.Category;

public class ListSubCategory
{
  public int Id { get; set; }
  public string? Name { get; set; } = null!;
  public int Type { get; set; }
  public DateTime CreatedAt { get; set; }

  public DeepCategory? DeepCategory { get; set; }
}
