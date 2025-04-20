using System.Text.Json;
using Google.Cloud.Storage.V1;
using Tooark.Storages.Options;

namespace Tooark.Tests.Storages.Options;

public class GcpOptionsTest
{
  // Teste para verificar se o construtor inicializa as propriedades corretamente
  [Fact]
  public void Constructor_WithJson_ShouldSetValues()
  {
    // Arrange
    string acl = "Private";
    string type = "service_account";
    string projectId = "my-project-id";
    string privateKeyId = "private-key-id";
    string privateKey = "private-key";
    string clientEmail = "client-email@my-project-id.iam.gserviceaccount.com";
    string clientId = "client-id";
    string authUri = "https://accounts.google.com/o/oauth2/auth";
    string tokenUri = "https://oauth2.googleapis.com/token";
    string authProviderX509CertUrl = "https://www.googleapis.com/oauth2/v1/certs";
    string clientX509CertUrl = "https://www.googleapis.com/robot/v1/metadata/x509/client-email%40my-project-id.iam.gserviceaccount.com";
    string universeDomain = "universe-domain";
    var json = $@"
    {{
      ""Acl"": ""{acl}"",
      ""Type"": ""{type}"",
      ""ProjectId"": ""{projectId}"",
      ""PrivateKeyId"": ""{privateKeyId}"",
      ""PrivateKey"": ""{privateKey}"",
      ""ClientEmail"": ""{clientEmail}"",
      ""ClientId"": ""{clientId}"",
      ""AuthUri"": ""{authUri}"",
      ""TokenUri"": ""{tokenUri}"",
      ""AuthProviderX509CertUrl"": ""{authProviderX509CertUrl}"",
      ""ClientX509CertUrl"": ""{clientX509CertUrl}"",
      ""UniverseDomain"": ""{universeDomain}""
    }}";

    // Act
    var options = JsonSerializer.Deserialize<GcpOptions>(json)!;

    // Assert
    Assert.Equal(PredefinedObjectAcl.Private, options.Acl);
    Assert.Equal(acl, options.AclString);
    Assert.Equal(type, options.Type);
    Assert.Equal(projectId, options.ProjectId);
    Assert.Equal(privateKeyId, options.PrivateKeyId);
    Assert.Equal(privateKey, options.PrivateKey);
    Assert.Equal(clientEmail, options.ClientEmail);
    Assert.Equal(clientId, options.ClientId);
    Assert.Equal(authUri, options.AuthUri);
    Assert.Equal(tokenUri, options.TokenUri);
    Assert.Equal(authProviderX509CertUrl, options.AuthProviderX509CertUrl);
    Assert.Equal(clientX509CertUrl, options.ClientX509CertUrl);
    Assert.Equal(universeDomain, options.UniverseDomain);
  }

  // Teste para verificar se o construtor inicializa as propriedades corretamente com o formato snake_case
  [Fact]
  public void Constructor_WithJsonSnakeCase_ShouldSetValues()
  {
    // Arrange
    string type = "service_account";
    string projectId = "my-project-id";
    string privateKeyId = "private-key-id";
    string privateKey = "private-key";
    string clientEmail = "client-email@my-project-id.iam.gserviceaccount.com";
    string clientId = "client-id";
    string authUri = "https://accounts.google.com/o/oauth2/auth";
    string tokenUri = "https://oauth2.googleapis.com/token";
    string authProviderX509CertUrl = "https://www.googleapis.com/oauth2/v1/certs";
    string clientX509CertUrl = "https://www.googleapis.com/robot/v1/metadata/x509/client-email%40my-project-id.iam.gserviceaccount.com";
    string universeDomain = "universe-domain";
    var json = $@"
    {{
      ""type"": ""{type}"",
      ""project_id"": ""{projectId}"",
      ""private_key_id"": ""{privateKeyId}"",
      ""private_key"": ""{privateKey}"",
      ""client_email"": ""{clientEmail}"",
      ""client_id"": ""{clientId}"",
      ""auth_uri"": ""{authUri}"",
      ""token_uri"": ""{tokenUri}"",
      ""auth_provider_x509_cert_url"": ""{authProviderX509CertUrl}"",
      ""client_x509_cert_url"": ""{clientX509CertUrl}"",
      ""universe_domain"": ""{universeDomain}""
    }}";

    // Act
    var options = JsonSerializer.Deserialize<GcpOptions>(json)!;

    // Assert
    Assert.Null(options.Acl);
    Assert.Equal(type, options.Type);
    Assert.Equal(projectId, options.ProjectId);
    Assert.Equal(privateKeyId, options.PrivateKeyId);
    Assert.Equal(privateKey, options.PrivateKey);
    Assert.Equal(clientEmail, options.ClientEmail);
    Assert.Equal(clientId, options.ClientId);
    Assert.Equal(authUri, options.AuthUri);
    Assert.Equal(tokenUri, options.TokenUri);
    Assert.Equal(authProviderX509CertUrl, options.AuthProviderX509CertUrl);
    Assert.Equal(clientX509CertUrl, options.ClientX509CertUrl);
    Assert.Equal(universeDomain, options.UniverseDomain);
  }

  // Teste para verificar se o método GetCredentials retorna os parâmetros corretamente
  [Fact]
  public void GetCredentials_ShouldReturnJsonCredentialParameters()
  {
    // Arrange
    string type = "service_account";
    string projectId = "my-project-id";
    string privateKeyId = "private-key-id";
    string privateKey = "private-key";
    string clientEmail = "client-email@my-project-id.iam.gserviceaccount.com";
    string clientId = "client-id";
    string authUri = "https://accounts.google.com/o/oauth2/auth";
    string tokenUri = "https://oauth2.googleapis.com/token";
    string authProviderX509CertUrl = "https://www.googleapis.com/oauth2/v1/certs";
    string clientX509CertUrl = "https://www.googleapis.com/robot/v1/metadata/x509/client-email%40my-project-id.iam.gserviceaccount.com";
    string universeDomain = "universe-domain";
    var json = $@"
    {{
      ""type"": ""{type}"",
      ""project_id"": ""{projectId}"",
      ""private_key_id"": ""{privateKeyId}"",
      ""private_key"": ""{privateKey}"",
      ""client_email"": ""{clientEmail}"",
      ""client_id"": ""{clientId}"",
      ""auth_uri"": ""{authUri}"",
      ""token_uri"": ""{tokenUri}"",
      ""auth_provider_x509_cert_url"": ""{authProviderX509CertUrl}"",
      ""client_x509_cert_url"": ""{clientX509CertUrl}"",
      ""universe_domain"": ""{universeDomain}""
    }}";
    var options = JsonSerializer.Deserialize<GcpOptions>(json)!;

    // Act
    var credentials = options.GetCredentials();

    // Assert
    Assert.Equal(type, credentials.Type);
    Assert.Equal(projectId, credentials.ProjectId);
    Assert.Equal(privateKeyId, credentials.PrivateKeyId);
    Assert.Equal(privateKey, credentials.PrivateKey);
    Assert.Equal(clientEmail, credentials.ClientEmail);
    Assert.Equal(clientId, credentials.ClientId);
    Assert.Equal(tokenUri, credentials.TokenUri);
    Assert.Equal(universeDomain, credentials.UniverseDomain);
  }
}
