using Microsoft.AspNetCore.Mvc.Filters;

namespace Tooark.Filters;

public class CustomResultFilter : IResultFilter
{
  public void OnResultExecuting(ResultExecutingContext context)
  {
    // Lógica antes da execução do resultado
  }

  public void OnResultExecuted(ResultExecutedContext context)
  {
    // Lógica após a execução do resultado
  }
}
