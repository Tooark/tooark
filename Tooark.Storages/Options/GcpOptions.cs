using System.Text.Json.Serialization;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Tooark.Storages.Options;

/// <summary>
/// Parâmetros de configuração do GCP.
/// </summary>
public class GcpOptions
{
  /// <summary>
  /// Variável privada para ACL (Lista de Controle de Acesso) dos arquivos na GCP em formato de string.
  /// </summary>
  private string? _aclString = null;

  /// <summary>
  /// Tipo de credencial.
  /// </summary>
  private string _type = null!;

  /// <summary>
  /// ID do projeto.
  /// </summary>
  private string _project_id = null!;

  /// <summary>
  /// ID da chave privada.
  /// </summary>
  private string _private_key_id = null!;

  /// <summary>
  /// Chave privada.
  /// </summary>
  private string _private_key = null!;

  /// <summary>
  /// E-mail do cliente.
  /// </summary>
  private string _client_email = null!;

  /// <summary>
  /// ID do cliente.
  /// </summary>
  private string _client_id = null!;

  /// <summary>
  /// URI de autenticação.
  /// </summary>
  private string _auth_uri = null!;

  /// <summary>
  /// URI do token.
  /// </summary>
  private string _token_uri = null!;

  /// <summary>
  /// URL do certificado de autenticação.
  /// </summary>
  private string _auth_provider_x509_cert_url = null!;

  /// <summary>
  /// URL do certificado x509 do cliente.
  /// </summary>
  private string _client_x509_cert_url = null!;

  /// <summary>
  /// Domínio do universo.
  /// </summary>
  private string _universe_domain = null!;


  /// <summary>
  /// Tipo de credencial.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("type")]
  public string TypeSnakeCase
  {
    set => _type = value;
  }

  /// <summary>
  /// ID do projeto.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("project_id")]
  public string ProjectIdSnakeCase
  {
    set => _project_id = value;
  }

  /// <summary>
  /// ID da chave privada.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("private_key_id")]
  public string PrivateKeyIdSnakeCase
  {
    set => _private_key_id = value;
  }

  /// <summary>
  /// Chave privada.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("private_key")]
  public string PrivateKeySnakeCase
  {
    set => _private_key = value;
  }

  /// <summary>
  /// E-mail do cliente.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("client_email")]
  public string ClientEmailSnakeCase
  {
    set => _client_email = value;
  }

  /// <summary>
  /// ID do cliente.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("client_id")]
  public string ClientIdSnakeCase
  {
    set => _client_id = value;
  }

  /// <summary>
  /// URI de autenticação.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("auth_uri")]
  public string AuthUriSnakeCase
  {
    set => _auth_uri = value;
  }

  /// <summary>
  /// URI do token.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("token_uri")]
  public string TokenUriSnakeCase
  {
    set => _token_uri = value;
  }

  /// <summary>
  /// URL do certificado de autenticação.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("auth_provider_x509_cert_url")]
  public string AuthProviderX509CertUrlSnakeCase
  {
    set => _auth_provider_x509_cert_url = value;
  }

  /// <summary>
  /// URL do certificado x509 do cliente.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("client_x509_cert_url")]
  public string ClientX509CertUrlSnakeCase
  {
    set => _client_x509_cert_url = value;
  }

  /// <summary>
  /// Domínio do universo.
  /// </summary>
  /// <remarks>
  /// Nome da propriedade no formato snake_case.
  /// </remarks>
  [JsonPropertyName("universe_domain")]
  public string UniverseDomainSnakeCase
  {
    set => _universe_domain = value;
  }

  /// <summary>
  /// Tipo de credencial.
  /// </summary>
  [JsonPropertyName("Type")]
  public string Type
  {
    get => _type;
    set => _type = value;
  }

  /// <summary>
  /// ID do projeto.
  /// </summary>
  [JsonPropertyName("ProjectId")]
  public string ProjectId
  {
    get => _project_id;
    set => _project_id = value;
  }

  /// <summary>
  /// ID da chave privada.
  /// </summary>
  [JsonPropertyName("PrivateKeyId")]
  public string PrivateKeyId
  {
    get => _private_key_id;
    set => _private_key_id = value;
  }

  /// <summary>
  /// Chave privada.
  /// </summary>
  [JsonPropertyName("PrivateKey")]
  public string PrivateKey
  {
    get => _private_key;
    set => _private_key = value;
  }

  /// <summary>
  /// E-mail do cliente.
  /// </summary>
  [JsonPropertyName("ClientEmail")]
  public string ClientEmail
  {
    get => _client_email;
    set => _client_email = value;
  }

  /// <summary>
  /// ID do cliente.
  /// </summary>
  [JsonPropertyName("ClientId")]
  public string ClientId
  {
    get => _client_id;
    set => _client_id = value;
  }

  /// <summary>
  /// URI de autenticação.
  /// </summary>
  [JsonPropertyName("AuthUri")]
  public string AuthUri
  {
    get => _auth_uri;
    set => _auth_uri = value;
  }

  /// <summary>
  /// URI do token.
  /// </summary>
  [JsonPropertyName("TokenUri")]
  public string TokenUri
  {
    get => _token_uri;
    set => _token_uri = value;
  }

  /// <summary>
  /// URL do certificado de autenticação.
  /// </summary>
  [JsonPropertyName("AuthProviderX509CertUrl")]
  public string AuthProviderX509CertUrl
  {
    get => _auth_provider_x509_cert_url;
    set => _auth_provider_x509_cert_url = value;
  }

  /// <summary>
  /// URL do certificado x509 do cliente.
  /// </summary>
  [JsonPropertyName("ClientX509CertUrl")]
  public string ClientX509CertUrl
  {
    get => _client_x509_cert_url;
    set => _client_x509_cert_url = value;
  }

  /// <summary>
  /// Domínio do universo.
  /// </summary>
  [JsonPropertyName("UniverseDomain")]
  public string UniverseDomain
  {
    get => _universe_domain;
    set => _universe_domain = value;
  }

  /// <summary>
  /// ACL (Lista de Controle de Acesso) dos arquivos na GCP.
  /// </summary>
  /// <value>Valor padrão é Nulo.</value>
  /// <remarks>
  /// Veja mais em: <see href="https://cloud.google.com/dotnet/docs/reference/Google.Cloud.Storage.V1/4.3.0/Google.Cloud.Storage.V1.PredefinedObjectAcl">PredefinedObjectAcl</see>.
  /// Só utilize este campo se você deseja definir um ACL específico para os arquivos na GCP.
  /// Não é possível inserir ACL legado para um objeto quando o acesso uniforme em nível de bucket está habilitado. Leia mais em <see href="https://cloud.google.com/storage/docs/uniform-bucket-level-access">Uniform bucket-level access</see>.
  /// </remarks>
  [JsonIgnore]
  public PredefinedObjectAcl? Acl
  {
    get => Enum.TryParse<PredefinedObjectAcl>(_aclString, out var acl) ? acl : null;
  }

  /// <summary>
  /// ACL (Lista de Controle de Acesso) dos arquivos na GCP em formato de string.
  /// </summary>
  /// <value>Valor padrão é Nulo.</value>
  /// <remarks>
  /// Veja mais em: <see href="https://cloud.google.com/dotnet/docs/reference/Google.Cloud.Storage.V1/4.3.0/Google.Cloud.Storage.V1.PredefinedObjectAcl">PredefinedObjectAcl</see>.
  /// Só utilize este campo se você deseja definir um ACL específico para os arquivos na GCP.
  /// Não é possível inserir ACL legado para um objeto quando o acesso uniforme em nível de bucket está habilitado. Leia mais em <see href="https://cloud.google.com/storage/docs/uniform-bucket-level-access">Uniform bucket-level access</see>.
  /// </remarks>
  [JsonPropertyName("Acl")]
  public string? AclString
  {
    get => _aclString;
    set => _aclString = value;
  }


  /// <summary>
  /// Função para retornar as credenciais do Google Cloud Storage.
  /// </summary>
  /// <returns>Retorna um <see cref="JsonCredentialParameters"/>.</returns>
  public JsonCredentialParameters GetCredentials() => new()
  {
    Type = _type,
    ProjectId = _project_id,
    PrivateKeyId = _private_key_id,
    PrivateKey = _private_key,
    ClientEmail = _client_email,
    ClientId = _client_id,
    TokenUri = _token_uri,
    UniverseDomain = _universe_domain
  };
}
