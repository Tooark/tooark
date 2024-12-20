using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Google.Apis.Auth.OAuth2;
using Tooark.Enums;

namespace Tooark.Options;

/// <summary>
/// Parâmetros de configuração do bucket.
/// </summary>
public class BucketOptions
{
  /// <summary>
  /// Seção do arquivo de configuração
  /// </summary>
  public readonly string Section = "Bucket";

  /// <summary>
  /// Nome do bucket
  /// </summary>
  public string BucketName { get; set; }  = "";

  /// <summary>
  /// Tamanho máximo do arquivo
  /// </summary>
  public long FileSize { get; set; } = 0;

  /// <summary>
  /// Tipo de cloud (AWS ou GCP).
  /// </summary>
  public CloudProvider CloudProvider { get; set; } = CloudProvider.None;

  /// <summary>
  /// Credenciais do AWS.
  /// </summary>
  public BasicAWSCredentials? AWS { get; set; }

  /// <summary>
  /// Credenciais do GCP.
  /// </summary>
  public JsonCredentialParameters? GCP { get; set; }

  /// <summary>
  /// Região da AWS.
  /// </summary>
  public RegionEndpoint? AWSRegion { get; set; }

  /// <summary>
  /// ACL dos arquivos na AWS.
  /// </summary>
  public S3CannedACL AWSAcl { get; set; } = S3CannedACL.Private;

  /// <summary>
  /// Credenciais do GCP em Arquivo.
  /// </summary>
  public string? GCPPath { get; set; }
}
