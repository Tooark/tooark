using Amazon;
using Amazon.S3.Model;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace Tooark.Storages.Dtos;

/// <summary>
/// Classe de resposta do serviço de armazenamento.
/// </summary>
public class UploadResponseDto
{
  /// <summary>
  /// Id do arquivo.
  /// </summary>
  public string Id { get; private set; }

  /// <summary>
  /// Link para acesso ao arquivo.
  /// </summary>
  public string Link { get; private set; }

  /// <summary>
  /// Nome do bucket.
  /// </summary>
  public string BucketName { get; private set; }

  /// <summary>
  /// Nome do arquivo.
  /// </summary>
  public string FileName { get; private set; }

  /// <summary>
  /// Tamanho do arquivo em bytes.
  /// </summary>
  public ulong Size { get; private set; } = 0;


  /// <summary>
  /// Construtor padrão utilizando um retorno do Google Cloud Storage.
  /// </summary>
  /// <param name="response">Resposta da requisição de upload.</param>
  public UploadResponseDto(Object response)
  {
    // Define os valores.
    Id = response.Id;
    Link = response.MediaLink;
    BucketName = response.Bucket;
    FileName = response.Name;
    Size = response.Size ?? Size;
  }

  /// <summary>
  /// Construtor padrão utilizando um retorno do Amazon S3.
  /// </summary>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="region">Região da AWS.</param>
  /// <param name="response">Resposta da requisição de upload.</param>
  public UploadResponseDto(string bucketName, string fileName, RegionEndpoint region, PutObjectResponse response)
  {
    // Cria o link de acesso ao arquivo
    var link = $"https://{bucketName}.s3.{region.SystemName}.amazonaws.com/{fileName}";

    // Define os valores.
    Id = link;
    Link = link;
    BucketName = bucketName;
    FileName = fileName;
    Size = (ulong)response.Size;
  }

  /// <summary>
  /// Construtor padrão utilizando todos os parâmetros.
  /// </summary>
  /// <param name="id">Id do arquivo.</param>
  /// <param name="link">Link para acesso ao arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="size">Tamanho do arquivo em bytes.</param>
  public UploadResponseDto(string id, string link, string bucketName, string fileName, ulong size)
  {
    // Define os valores.
    Id = id;
    Link = link;
    BucketName = bucketName;
    FileName = fileName;
    Size = size;
  }
}
