using Tooark.Interfaces;
using Tooark.Services;

namespace Tooark.Factories;

/// <summary>
/// Classe de fábrica para criar instâncias de <see cref="IHttpClientService"/>.
/// </summary>
public static class HttpClientServiceFactory
{
  /// <summary>
  /// Cria uma instância de <see cref="IHttpClientService"/>.
  /// </summary>
  /// <param name="httpClient">O cliente HTTP a ser usado pelo serviço.</param>
  /// <returns>Uma instância de <see cref="IHttpClientService"/>.</returns>
  public static IHttpClientService Create(HttpClient httpClient)
  {
    return new HttpClientService(httpClient);
  }
}
