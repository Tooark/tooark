using System.Text.Json.Serialization;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;

namespace Tooark.Storages.Options;

/// <summary>
/// Parâmetros de configuração do AWS.
/// </summary>
public class AwsOptions : AWSCredentials
{
  /// <summary>
  /// Variável privada para Região da AWS em formato de string.
  /// </summary>
  private string _regionString = RegionEndpoint.USEast1.SystemName;

  /// <summary>
  /// Variável privada para ACL (Lista de Controle de Acesso) dos arquivos na AWS em formato de string.
  /// </summary>
  private string? _aclString = null;


  /// <summary>
  /// Construtor padrão.
  /// </summary>
  public AwsOptions()
  { }

  /// <summary>
  /// Construtor com as credenciais de acesso.
  /// </summary>
  /// <param name="accessKey">Chave de acesso.</param>
  /// <param name="secretKey">Chave secreta.</param>
  public AwsOptions(string accessKey, string secretKey)
  {
    // Define as credenciais de acesso.
    SetCredentials(accessKey, secretKey);
  }

  /// <summary>
  /// Construtor com as credenciais de acesso e a região.
  /// </summary>
  /// <param name="accessKey">Chave de acesso.</param>
  /// <param name="secretKey">Chave secreta.</param>
  /// <param name="region">Região da AWS.</param>
  public AwsOptions(string accessKey, string secretKey, RegionEndpoint region)
  {
    // Define as credenciais de acesso.
    SetCredentials(accessKey, secretKey);

    // Define a região.
    _regionString = region.SystemName;
  }

  /// <summary>
  /// Construtor com as credenciais de acesso, a região e a ACL.
  /// </summary>
  /// <param name="accessKey">Chave de acesso.</param>
  /// <param name="secretKey">Chave secreta.</param>
  /// <param name="region">Região da AWS.</param>
  /// <param name="acl">Tipo de ACL.</param>
  public AwsOptions(string accessKey, string secretKey, RegionEndpoint region, S3CannedACL acl)
  {
    // Define as credenciais de acesso.
    SetCredentials(accessKey, secretKey);

    // Define a região.
    _regionString = region.SystemName;

    // Define a ACL.
    _aclString = acl;
  }


  /// <summary>
  /// Define as credenciais de acesso.
  /// </summary>
  public string AccessKey { get; set; } = string.Empty;

  /// <summary>
  /// Define a chave secreta.
  /// </summary>
  public string SecretKey { get; set; } = string.Empty;

  /// <summary>
  /// Região da AWS.
  /// </summary>
  /// <value>Valor padrão é RegionEndpoint.USEast1.</value>
  /// <remarks>Veja mais em: <see href="https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TRegionEndpointpoint.html">RegionEndpoint</see>.</remarks>
  [JsonIgnore]
  public RegionEndpoint Region
  {
    get => RegionEndpoint.GetBySystemName(_regionString);
  }

  /// <summary>
  /// Região da AWS em formato de string.
  /// </summary>
  /// <value>Valor padrão é RegionEndpoint.USEast1.</value>
  /// <remarks>Veja mais em: <see href="https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Amazon/TRegionEndpointpoint.html">RegionEndpoint</see>.</remarks>
  [JsonPropertyName("Region")]
  public string RegionString
  {
    get => _regionString;
    set => _regionString = value;
  }

  /// <summary>
  /// ACL (Lista de Controle de Acesso) dos arquivos na AWS.
  /// </summary>
  /// <value>Valor padrão é Nulo.</value>
  /// <remarks>
  /// Veja mais em: <see href="https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3CannedACL.html">S3CannedACL</see>.
  /// Só utilize este campo se você deseja definir um ACL específico para os arquivos na AWS.
  /// Não é possível inserir ACL para um objeto quando o acesso uniforme em nível de bucket está habilitado. Leia mais em <see href="https://docs.aws.amazon.com/AmazonS3/latest/userguide/about-object-ownership.html">Controlling ownership of objects and disabling ACLs for your bucket</see>.
  /// </remarks>
  [JsonIgnore]
  public S3CannedACL? Acl
  {
    get => !string.IsNullOrEmpty(_aclString) ? S3CannedACL.FindValue(_aclString) : null;
  }

  /// <summary>
  /// ACL (Lista de Controle de Acesso) dos arquivos na AWS em formato de string.
  /// </summary>
  /// <value>Valor padrão é Nulo.</value>
  /// <remarks>
  /// Veja mais em: <see href="https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/S3/TS3CannedACL.html">S3CannedACL</see>.
  /// Só utilize este campo se você deseja definir um ACL específico para os arquivos na AWS.
  /// Não é possível inserir ACL para um objeto quando o acesso uniforme em nível de bucket está habilitado. Leia mais em <see href="https://docs.aws.amazon.com/AmazonS3/latest/userguide/about-object-ownership.html">Controlling ownership of objects and disabling ACLs for your bucket</see>.
  /// </remarks>
  [JsonPropertyName("Acl")]
  public string? AclString
  {
    get => _aclString;
    set => _aclString = value;
  }


  /// <summary>
  /// Define as credenciais de acesso.
  /// </summary>
  /// <param name="accessKey">Chave de acesso.</param>
  /// <param name="secretKey">Chave secreta.</param>
  private void SetCredentials(string accessKey, string secretKey)
  {
    // Verifica se as credenciais são válidas.
    AccessKey = !string.IsNullOrEmpty(accessKey) ? accessKey : throw new ArgumentNullException(nameof(accessKey));

    // Verifica se a chave secreta é válida.
    SecretKey = !string.IsNullOrEmpty(secretKey) ? secretKey : throw new ArgumentNullException(nameof(secretKey));
  }

  /// <summary>
  /// Retorna as credenciais de acesso.
  /// </summary>
  /// <returns>Retorna as credenciais de acesso.</returns>
  /// <remarks>Veja mais em: <see href="https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Runtime/TAWSCredentials.html">AWSCredentials</see>.</remarks>
  public override ImmutableCredentials GetCredentials()
  {
    // Verifica se as credenciais são válidas.
    if (string.IsNullOrEmpty(AccessKey) || string.IsNullOrEmpty(SecretKey))
    {
      return null!;
    }

    // Retorna as credenciais de acesso.
    return new (AccessKey, SecretKey, null);
  }
}
