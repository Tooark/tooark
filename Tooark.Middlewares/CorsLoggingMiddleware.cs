namespace Tooark.Middlewares;

public class CorsLoggingMiddleware(RequestDelegate next, ILogger<CorsLoggingMiddleware> logger, IOptions<ApiOptions> apiOptions)
{
  private readonly RequestDelegate _next = next;
  private readonly ILogger<CorsLoggingMiddleware> _logger = logger;
  private readonly string[] _corsOrigins = apiOptions.Value.CorsOrigins.Split(',');
  private readonly string[] _corsOriginsCustom = apiOptions.Value.CorsOriginsCustom?.Split(',') ?? [];

  public async Task InvokeAsync(HttpContext context)
  {
    if (context.Request.Method == HttpMethod.Options.Method)
    {
      var originHeader = context.Request.Headers.Origin.ToString();

      if (!_corsOrigins.Contains(originHeader) && !_corsOriginsCustom.Contains(originHeader))
      {
        _logger.LogWarning("Middleware.CorsLogging.Invoke: Origin {OriginHeader} is not allowed by CORS.", originHeader);
      }
    }

    await _next(context);
  }
}
