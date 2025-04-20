using Microsoft.AspNetCore.Mvc.Filters;

namespace Tooark.Filters;

public class CustomResourceFilter : IResourceFilter
{
  public void OnResourceExecuting(ResourceExecutingContext context)
  {
    // Lógica antes da execução do recurso
  }

  public void OnResourceExecuted(ResourceExecutedContext context)
  {
    // Lógica após a execução do recurso
  }
}
