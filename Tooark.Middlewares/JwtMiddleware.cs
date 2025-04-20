using System.Text.Json;
using WS_Content.Services;
using WS_Content.Types;

namespace WS_Content.Middlewares;

public class JwtMiddleware(
  RequestDelegate next,
  ILogger<JwtMiddleware> logger)
{
  private readonly RequestDelegate _next = next;
  private readonly ILogger<JwtMiddleware> _logger = logger;

  public async Task Invoke(HttpContext context, IAuthService _authService, IJwtService _jwtService)
  {
    try
    {
      string? token = ExtractToken(context);

      if (!string.IsNullOrEmpty(token))
      {
        var validationResult = _jwtService.ValidateToken(token);

        if (validationResult.IsValid)
        {
          var userContext = await _authService.GetUserAuth(validationResult.UserAuth);

          if (string.IsNullOrEmpty(userContext.ErrorAuth))
          {
            context.Items["User"] = userContext;
          }
          else
          {
            await SetErrorResponseAsync(context, userContext.ErrorAuth);

            return;
          }
        }
        else
        {
          await SetErrorResponseAsync(context, validationResult.ErrorToken);

          return;
        }
      }
    }
    catch (Exception ex)
    {
      _logger.LogError("Middleware.Jwt.Invoke: {Exception}", ex.ToString());

      await SetErrorResponseAsync(context, "InternalServerError");

      return;
    }

    await _next(context);
  }

  private static string? ExtractToken(HttpContext context)
  {
    var authorizationHeader = context.Request.Headers.Authorization.FirstOrDefault();

    if (authorizationHeader?.StartsWith("Bearer ") == true)
    {
      return authorizationHeader["Bearer ".Length..];
    }

    return null;
  }

  private static async Task SetErrorResponseAsync(HttpContext context, string error)
  {
    context.Response.StatusCode = GetStatusCodeForError(error);
    context.Response.ContentType = "application/json";

    await context.Response.WriteAsJsonAsync(new ResultDto<string>(error));
  }

  private static int GetStatusCodeForError(string error)
  {
    return error switch
    {
      "TokenInvalid" => StatusCodes.Status400BadRequest,
      "TokenExpired" => StatusCodes.Status401Unauthorized,
      "UserBlocked" => StatusCodes.Status403Forbidden,
      "UserDisabled" => StatusCodes.Status403Forbidden,
      "UserNotFound" => StatusCodes.Status404NotFound,
      _ => StatusCodes.Status500InternalServerError,
    };
  }
}
