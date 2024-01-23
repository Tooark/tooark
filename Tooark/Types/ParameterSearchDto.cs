namespace Tooark.Types;

public class ParameterSearchDto
{
  public string? Search { get; set; }
  public int PageIndex { get; set; } = 0;
  public int PageSize { get; set; } = 50;
}

public class ParameterSearchOrderDto : ParameterSearchDto
{
  public Order? Order { get; set; }
}

public class Order
{
  public string Field { get; set; } = null!;
  public bool Asc { get; set; } = true;
}
