namespace Tooark.Observability.Options;

/// <summary>
/// Opções de configuração para tratamento de dados sensíveis em OpenTelemetry.
/// </summary>
public class DataSensitiveOptions
{
  /// <summary>
  /// Indica se os parâmetros de query devem ser removidos do atributo http.target.
  /// </summary>
  public bool HideQueryParameters { get; set; } = true;

  /// <summary>
  /// Indica se os headers sensíveis devem ser mascarados/removidos dos atributos de span.
  /// </summary>
  public bool HideHeaders { get; set; } = true;

  /// <summary>
  /// Lista de headers que devem ser tratados como sensíveis e mascarados quando a sanitização estiver ativa.
  /// </summary>
  /// <remarks>
  /// Valores são comparados de forma case-insensitive.
  /// </remarks>
  public string[] SensitiveRequestHeaders { get; set; } =
  [
    "authorization",
    "proxy-authorization",
    "cookie",
    "set-cookie",
    "x-api-key",
    "api-key",
    "apikey",
    "x-functions-key",
    "x-amz-security-token",
    "x-google-oauth-access-token",
    "x-azure-access-token"
  ];
}
