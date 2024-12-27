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
  public const string Section = "Bucket";

  /// <summary>
  /// Nome do bucket
  /// </summary>
  public string BucketName { get; set; }  = "";

  /// <summary>
  /// Tamanho máximo do arquivo
  /// </summary>
  /// <value>1024</value>
  /// <remarks>Valor padrão é 1024. Medida em kb</remarks>
  public long FileSize { get; set; } = 1024;

  /// <summary>
  /// Tipo de cloud (AWS ou GCP).
  /// </summary>
  /// <value>CloudProvider.None</value>
  /// <remarks>Valor padrão é None</remarks>
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
  /// <value>RegionEndpoint.USEast1</value>
  /// <remarks>Valor padrão é USEast1</remarks>
  public RegionEndpoint? AWSRegion { get; set; } = RegionEndpoint.USEast1;

  /// <summary>
  /// ACL dos arquivos na AWS.
  /// </summary>
  /// <value>S3CannedACL.Private</value>
  /// <remarks>Valor padrão é Private</remarks>
  public S3CannedACL AWSAcl { get; set; } = S3CannedACL.Private;
}
