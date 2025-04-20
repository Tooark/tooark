using Google.Cloud.Storage.V1;

namespace Tooark.Storages.Interfaces;

/// <summary>
/// Interface IUrlSigner que contém os métodos para assinatura de URLs.
/// </summary>
public interface IUrlSigner
{
  /// <summary>
  /// Assina uma URL para um objeto no Google Cloud Storage.
  /// </summary>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="objectName">Nome do objeto.</param>
  /// <param name="expiration">Tempo de expiração da URL.</param>
  /// <param name="cancellationToken">Token de cancelamento. Parâmetro opcional.</param>
  /// <returns>URL assinada.</returns>
  Task<string> SignAsync(
    string bucketName,
    string objectName,
    TimeSpan expiration,
    CancellationToken cancellationToken = default
  );

  /// <summary>
  /// Assina uma URL para um objeto no Google Cloud Storage.
  /// </summary>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="objectName">Nome do objeto.</param>
  /// <param name="expiration">Tempo de expiração da URL.</param>
  /// <param name="httpMethod">Método HTTP.</param>
  /// <param name="cancellationToken">Token de cancelamento. Parâmetro opcional.</param>
  /// <returns>URL assinada.</returns>
  Task<string> SignAsync(
    string bucketName,
    string objectName,
    TimeSpan expiration,
    HttpMethod httpMethod,
    CancellationToken cancellationToken = default
  );

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
  Task<string> SignAsync(
    string bucketName,
    string objectName,
    TimeSpan expiration,
    HttpMethod httpMethod,
    SigningVersion? signingVersion,
    CancellationToken cancellationToken = default
  );
}
