using System.Net;
using System.Text.Json;
using WS_Content.Helpers;

namespace WS_Content.Middlewares;

public class ErrorHandlerMiddleware
{
  private readonly RequestDelegate _next;

  public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

  public async Task Invoke(HttpContext context, ILogger<ErrorHandlerMiddleware> logger)
  {
    try
    {
      await _next(context);
    }
    catch (Exception error)
    {
      var code = "app_error";
      var resp = context.Response;
      resp.ContentType = "application/json";

      switch (error)
      {
        case AppException e:
          resp.StatusCode = (int)e.HttpStatusCode;
          break;

        case KeyNotFoundException:
          resp.StatusCode = (int)HttpStatusCode.NotFound;
          break;

        default:
          code = "server_error";
          resp.StatusCode = (int)HttpStatusCode.InternalServerError;
          break;
      };

      if (resp.StatusCode == (int)HttpStatusCode.InternalServerError)
      {
        logger.LogError("Server error: {Message}", error.Message);

        if (error.InnerException is Exception iex)
        {
          logger.LogError("Inner exception: {Message}", iex.Message);
        }
      }

      await resp.WriteAsync(JsonSerializer.Serialize(new
      {
        message = error?.Message,
        code
      }));
    }
  }
}
