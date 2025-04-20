using Microsoft.AspNetCore.Mvc.Filters;

namespace Tooark.Filters;

public class CustomActionFilter : IActionFilter
{
  public void OnActionExecuting(ActionExecutingContext context)
  {
    // Lógica antes da execução da ação
  }

  public void OnActionExecuted(ActionExecutedContext context)
  {
    // Lógica após a execução da ação
  }
}
