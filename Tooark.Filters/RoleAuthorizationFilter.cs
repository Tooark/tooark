using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tooark.Filters;

public class RoleAuthorizationFilter : IAuthorizationFilter
{
  private readonly string _role;

  public RoleAuthorizationFilter(string role)
  {
    _role = role;
  }

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    if (!context.HttpContext.User.Identity.IsAuthenticated || 
        !context.HttpContext.User.IsInRole(_role))
    {
      context.Result = new ForbidResult();
    }
  }
}
