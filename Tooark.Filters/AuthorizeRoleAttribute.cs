using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tooark.Filters;

public class AuthorizeRoleAttribute : TypeFilterAttribute
{
  public AuthorizeRoleAttribute(string role) : base(typeof(RoleAuthorizationFilter))
  {
    Arguments = new object[] { role };
  }
}
