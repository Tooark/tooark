using Tooark.Interfaces;
using Tooark.Services;

namespace Tooark.Factories;

/// <summary>
/// Fornece métodos para criar instâncias de IHttpClientService.
/// </summary>
public static class HttpClientServiceFactory
{
  /// <summary>
  /// Cria uma nova instância de IHttpClientService.
  /// </summary>
  /// <param name="httpClient">O cliente HTTP a ser usado pelo serviço.</param>
  /// <returns>Uma instância de IHttpClientService.</returns>
  public static IHttpClientService Create(HttpClient httpClient)
  {
    return new HttpClientService(httpClient);
  }
}
