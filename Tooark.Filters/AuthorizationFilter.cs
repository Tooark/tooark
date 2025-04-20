using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Tooark.Filters;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
  private readonly ILogger<CustomAuthorizationFilter> _logger;

  public CustomAuthorizationFilter(ILogger<CustomAuthorizationFilter> logger)
  {
    _logger = logger;
  }

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    // Lógica de autorização personalizada
    if (!context.HttpContext.User.Identity.IsAuthenticated)
    {
      _logger.LogWarning("Acesso não autorizado: {Path}", context.HttpContext.Request.Path);
      context.Result = new JsonResult(new { message = "Usuário não autenticado" })
      {
        StatusCode = StatusCodes.Status401Unauthorized
      };
      return;
    }

    // Verificação de regras de negócio específicas
    if (!HasRequiredPermissions(context))
    {
      _logger.LogWarning("Acesso negado devido a permissões insuficientes: {Path}", context.HttpContext.Request.Path);
      context.Result = new JsonResult(new { message = "Permissões insuficientes" })
      {
        StatusCode = StatusCodes.Status403Forbidden
      };
      return;
    }
  }

  private bool HasRequiredPermissions(AuthorizationFilterContext context)
  {
    // Adicione a lógica para verificar as permissões necessárias
    // Exemplo: Verificar se o usuário tem uma permissão específica
    var userPermissions = context.HttpContext.User.Claims
        .Where(c => c.Type == "Permission")
        .Select(c => c.Value)
        .ToList();

    // Suponha que a rota requer uma permissão específica chamada "RequiredPermission"
    var requiredPermission = "RequiredPermission";
    return userPermissions.Contains(requiredPermission);
  }
}
