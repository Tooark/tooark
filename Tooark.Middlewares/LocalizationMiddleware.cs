using System.Globalization;
using static Tooark.Utils.Util;

namespace WS_Content.Middlewares;

public class LocalizationMiddleware(List<CultureInfo> supportedCultures) : IMiddleware
{
  private readonly List<CultureInfo> _supportedCultures = [.. supportedCultures];

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    // Avalia a chave de cultura com base no cabeçalho da solicitação
    var requestedCulture = ExtractCultureFromHeader(context.Request.Headers.AcceptLanguage);

    if (!string.IsNullOrEmpty(requestedCulture))
    {
      var culture = GetValidCulture(requestedCulture);

      Languages.SetCulture(culture.Name);
    }

    // Aguardar a próxima requisição
    await next(context);
  }


  private CultureInfo GetValidCulture(string cultureName)
  {
    return _supportedCultures
      .Find(culture =>
        string.Equals(
          culture.Name,
          cultureName,
          StringComparison.OrdinalIgnoreCase
        )
      ) ?? new CultureInfo(Languages.Default);
  }

  private static string? ExtractCultureFromHeader(string? headerValue)
  {
    return headerValue?
      .Split(',')
      .SelectMany(x => x.Split(';'))
      .FirstOrDefault(x =>
        x.Contains('-') &&
        x.Length == 5);
  }
}
