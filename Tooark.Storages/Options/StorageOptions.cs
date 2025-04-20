using Tooark.Enums;

namespace Tooark.Storages.Options;

/// <summary>
/// Parâmetros de configuração do storage.
/// </summary>
public class StorageOptions
{
  /// <summary>
  /// Variável privada com tamanho máximo do arquivo.
  /// </summary>
  private long _fileSize = 2048;

  /// <summary>
  /// Variável privada com tempo de expiração do link assinado.
  /// </summary>
  private int _signerMinutes = 5;

  /// <summary>
  /// Variável privada com credenciais da AWS.
  /// </summary>
  private AwsOptions? _awsRead;

  /// <summary>
  /// Variável privada com credenciais da GCP.
  /// </summary>
  private GcpOptions? _gcpRead;


  /// <summary>
  /// Seção do arquivo de configuração.
  /// </summary>
  public const string Section = "Storage";


  /// <summary>
  /// Nome do bucket.
  /// </summary>
  public string BucketName { get; set; } = "";

  /// <summary>
  /// Tamanho máximo do arquivo.
  /// </summary>
  /// <value>Valor padrão é 2048.</value>
  /// <remarks>Medida em kb (kilobytes).</remarks>
  public long FileSize
  {
    get => _fileSize;
    set => _fileSize = value > 0 ? value : 2048;
  }

  /// <summary>
  /// Tempo de expiração do link assinado.
  /// </summary>
  /// <value>Valor padrão é 5.</value>
  /// <remarks>Medida em minutos.</remarks>
  public int SignerMinutes
  {
    get => _signerMinutes;
    set => _signerMinutes = value > 0 ? value : 5;
  }

  /// <summary>
  /// Tipo de cloud (Amazon, Google e Microsoft).
  /// </summary>
  /// <value>Valor padrão é None.</value>
  public ECloudProvider CloudProvider { get; set; } = ECloudProvider.None;

  /// <summary>
  /// Credenciais do AWS.
  /// </summary>
  public AwsOptions? AWS { get; set; }

  /// <summary>
  /// Credenciais do AWS para leitura de arquivos.
  /// </summary>
  /// <remarks>
  /// Parâmetros de credenciais para leitura de arquivos.
  /// Se não for informado, será utilizado as credenciais AWS padrão.
  /// </remarks>
  public AwsOptions? AWSRead
  {
    get => _awsRead ?? AWS;
    set => _awsRead = value;
  }

  /// <summary>
  /// Credenciais do GCP.
  /// </summary>
  public GcpOptions? GCP { get; set; }

  /// <summary>
  /// Credenciais do GCP para leitura de arquivos.
  /// </summary>
  /// <remarks>
  /// Parâmetros de credenciais para leitura de arquivos.
  /// Se não for informado, será utilizado as credenciais GCP padrão.
  /// </remarks>
  public GcpOptions? GCPRead 
  {
    get => _gcpRead ?? GCP;
    set => _gcpRead = value;
  }
}
