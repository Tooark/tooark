using Google.Cloud.Storage.V1;
using Tooark.Storages.Interfaces;

namespace Tooark.Storages.Adapters;

/// <summary>
/// Adaptador para a classe <see cref="UrlSigner"/>.
/// </summary>
/// <remarks>
/// Implementa a interface <see cref="IUrlSigner"/>.
/// </remarks>
/// <param name="urlSigner">Instância de <see cref="UrlSigner"/>.</param>
public class UrlSignerAdapter(UrlSigner urlSigner) : IUrlSigner
{
  /// <summary>
  /// Instância privada de <see cref="UrlSigner"/>.
  /// </summary>
  private readonly UrlSigner _urlSigner = urlSigner;

  /// <summary>
  /// Assina uma URL para um objeto no Google Cloud Storage.
  /// </summary>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="objectName">Nome do objeto.</param>
  /// <param name="expiration">Tempo de expiração da URL.</param>
  /// <param name="cancellationToken">Token de cancelamento. Parâmetro opcional.</param>
  /// <returns>URL assinada.</returns>
  public async Task<string> SignAsync(
    string bucketName,
    string objectName,
    TimeSpan expiration,
    CancellationToken cancellationToken = default
  )
  {
    // Chama o método de assinatura da URL. Definindo o método HTTP como GET e sem versão de assinatura.
    return await _urlSigner.SignAsync(bucketName, objectName, expiration, HttpMethod.Get, null, cancellationToken);
  }

  /// <summary>
  /// Assina uma URL para um objeto no Google Cloud Storage.
  /// </summary>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="objectName">Nome do objeto.</param>
  /// <param name="expiration">Tempo de expiração da URL.</param>
  /// <param name="httpMethod">Método HTTP.</param>
  /// <param name="cancellationToken">Token de cancelamento. Parâmetro opcional.</param>
  /// <returns>URL assinada.</returns>
  public async Task<string> SignAsync(
    string bucketName,
    string objectName,
    TimeSpan expiration,
    HttpMethod httpMethod,
    CancellationToken cancellationToken = default
  )
  {
    // Chama o método de assinatura da URL. Definindo sem versão de assinatura.
    return await _urlSigner.SignAsync(bucketName, objectName, expiration, httpMethod, null, cancellationToken);
  }

  /// <summary>
  /// Assina uma URL para um objeto no Google Cloud Storage.
  /// </summary>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="objectName">Nome do objeto.</param>
  /// <param name="expiration">Tempo de expiração da URL.</param>
  /// <param name="httpMethod">Método HTTP.</param>
  /// <param name="signingVersion">Versão de assinatura.</param>
  /// <param name="cancellationToken">Token de cancelamento. Parâmetro opcional.</param>
  /// <returns>URL assinada.</returns>
  public async Task<string> SignAsync(
    string bucketName,
    string objectName,
    TimeSpan expiration,
    HttpMethod httpMethod,
    SigningVersion? signingVersion,
    CancellationToken cancellationToken = default
  )
  {
    // Chama o método de assinatura da URL.
    return await _urlSigner.SignAsync(bucketName, objectName, expiration, httpMethod, signingVersion, cancellationToken);
  }
}
