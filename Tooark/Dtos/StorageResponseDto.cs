namespace Tooark.Dtos;

/// <summary>
/// Classe de resposta do serviço de armazenamento.
/// </summary>
public class StorageResponseDto
{
  /// <summary>
  /// Id do arquivo.
  /// </summary>
  public required string Id { get; set; }

  /// <summary>
  /// Nome do arquivo.
  /// </summary>
  public required string FileName { get; set; }

  /// <summary>
  /// Nome do bucket.
  /// </summary>
  public required string BucketName { get; set; }

  /// <summary>
  /// Link do arquivo para download.
  /// </summary>
  public required string DownloadLink { get; set; }

  /// <summary>
  /// Link do arquivo para acesso dentro do bucket.
  /// </summary>
  public required string BucketLink { get; set; }
}
