# Secrets

Para utilizar a injeção de segredos em tempo de execução em vez de armazenar credenciais diretamente no arquivo `appsettings.json`, você pode seguir os passos abaixo para integrar com serviços de gerenciamento de segredos, como o Google Secret Manager ou o AWS Secrets Manager. Vou fornecer um exemplo para ambos os serviços.

## Exemplo com Google Secret Manager

### Passo 1: Armazenar o Segredo no Google Secret Manager

```bash
echo -n '{"type":"service_account","project_id":"my-project","private_key_id":"my-key-id","private_key":"-----BEGIN PRIVATE KEY-----\n...\n-----END PRIVATE KEY-----\n","client_email":"my-email@my-project.iam.gserviceaccount.com","client_id":"my-client-id","auth_uri":"https://accounts.google.com/o/oauth2/auth","token_uri":"https://oauth2.googleapis.com/token","auth_provider_x509_cert_url":"https://www.googleapis.com/oauth2/v1/certs","client_x509_cert_url":"https://www.googleapis.com/robot/v1/metadata/x509/my-email%40my-project.iam.gserviceaccount.com"}' | gcloud secrets create my-gcp-credentials --data-file=-
```

### Passo 2: Conceder Permissões ao Serviço

```bash
gcloud secrets add-iam-policy-binding my-gcp-credentials \
  --member=serviceAccount:my-service-account@my-project.iam.gserviceaccount.com \
  --role=roles/secretmanager.secretAccessor
```

### Passo 3: Modificar o Código para Carregar Credenciais do Google Secret Manager

**1. Adicionar Dependências:**

```bash
dotnet add package Google.Cloud.SecretManager.V1
```

**2. Modificar o Código para Carregar Credenciais:**

```csharp
// filepath: /c:/Repositorio/0-Pacote/tooark/Tooark.Storages/StorageService.cs
using Google.Cloud.SecretManager.V1;

public StorageService(
    ILogger<StorageService> logger,
    IOptions<StorageOptions> bucketOptions)
{
    _logger = logger;
    _storageOptions = bucketOptions.Value;

    if (_storageOptions == null || string.IsNullOrEmpty(_storageOptions.BucketName))
    {
        throw new InternalServerErrorException("Storage.StorageOptionsEmpty");
    }

    if (_storageOptions.CloudProvider == ECloudProvider.None)
    {
        throw new InternalServerErrorException("Storage.CloudProviderEmpty");
    }
    else if (_storageOptions.CloudProvider == ECloudProvider.Amazon && _storageOptions.AWS == null)
    {
        throw new InternalServerErrorException("Storage.CredentialsEmpty;AWS");
    }
    else if (_storageOptions.CloudProvider == ECloudProvider.Google)
    {
        if (_storageOptions.GCP == null)
        {
            throw new InternalServerErrorException("Storage.CredentialsEmpty;GCP");
        }

        // Carregar credenciais do Google Secret Manager
        var secretManagerClient = SecretManagerServiceClient.Create();
        var secretName = new SecretVersionName("my-project", "my-gcp-credentials", "latest");
        var secret = secretManagerClient.AccessSecretVersion(secretName);
        var secretPayload = secret.Payload.Data.ToStringUtf8();

        _storageOptions.GCP.CredentialsJson = secretPayload;
    }
}
```

## Exemplo com AWS Secrets Manager

### Passo 1: Armazenar o Segredo no AWS Secrets Manager

```bash
aws secretsmanager create-secret --name my-aws-credentials --secret-string '{"accessKeyId":"my-access-key-id","secretAccessKey":"my-secret-access-key"}'
```

### Passo 2: Conceder Permissões ao Serviço na AWS

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": "secretsmanager:GetSecretValue",
      "Resource": "arn:aws:secretsmanager:region:account-id:secret:my-aws-credentials"
    }
  ]
}
```

### Passo 3: Modificar o Código para Carregar Credenciais do AWS Secrets Manager

**1. Adicionar Dependências:**

```bash
dotnet add package AWSSDK.SecretsManager
```

**2. Modificar o Código para Carregar Credenciais:**

```csharp
// filepath: /c:/Repositorio/0-Pacote/tooark/Tooark.Storages/StorageService.cs
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

public StorageService(
    ILogger<StorageService> logger,
    IOptions<StorageOptions> bucketOptions)
{
    _logger = logger;
    _storageOptions = bucketOptions.Value;

    if (_storageOptions == null || string.IsNullOrEmpty(_storageOptions.BucketName))
    {
        throw new InternalServerErrorException("Storage.StorageOptionsEmpty");
    }

    if (_storageOptions.CloudProvider == ECloudProvider.None)
    {
        throw new InternalServerErrorException("Storage.CloudProviderEmpty");
    }
    else if (_storageOptions.CloudProvider == ECloudProvider.Amazon)
    {
        if (_storageOptions.AWS == null)
        {
            throw new InternalServerErrorException("Storage.CredentialsEmpty;AWS");
        }

        // Carregar credenciais do AWS Secrets Manager
        var secretsManagerClient = new AmazonSecretsManagerClient();
        var secretValueRequest = new GetSecretValueRequest
        {
            SecretId = "my-aws-credentials"
        };
        var secretValueResponse = secretsManagerClient.GetSecretValueAsync(secretValueRequest).Result;
        var secretString = secretValueResponse.SecretString;

        var awsCredentials = JsonConvert.DeserializeObject<AwsOptions>(secretString);
        _storageOptions.AWS.AccessKeyId = awsCredentials.AccessKeyId;
        _storageOptions.AWS.SecretAccessKey = awsCredentials.SecretAccessKey;
    }
    else if (_storageOptions.CloudProvider == ECloudProvider.Google && _storageOptions.GCP == null)
    {
        throw new InternalServerErrorException("Storage.CredentialsEmpty;GCP");
    }
}
```

## Conclusão

Utilizar a injeção de segredos em tempo de execução com serviços de gerenciamento de segredos como Google Secret Manager ou AWS Secrets Manager é uma prática recomendada para garantir a segurança das credenciais. Isso evita o armazenamento de credenciais em arquivos de configuração e permite um gerenciamento centralizado e seguro dos segredos.

---

## Cache de Credenciais

### Acesso a Arquivos Internos no Cloud Run

Em geral, o Cloud Run é um serviço gerenciado que executa contêineres em uma infraestrutura sem servidor. Acesso direto ao sistema de arquivos de um contêiner em execução no Cloud Run não é uma prática comum e não é suportado diretamente. No entanto, você pode acessar arquivos internos do contêiner durante a execução do aplicativo, mas isso deve ser feito com cuidado para evitar exposição acidental de dados sensíveis.

### Desempenho ao Usar Secret Manager

Utilizar serviços de gerenciamento de segredos como Google Secret Manager ou AWS Secrets Manager pode introduzir uma pequena latência adicional ao buscar segredos, mas isso pode ser mitigado de várias maneiras:

1. Cache de Segredos:

   Implemente um cache em memória para armazenar segredos após a primeira recuperação. Isso evita a necessidade de buscar segredos repetidamente.

2. Carregamento de Segredos na Inicialização:

   Carregue os segredos uma vez durante a inicialização do aplicativo e armazene-os em variáveis de ambiente ou em um objeto de configuração em memória.

### Implementação de Cache de Segredos

Aqui está um exemplo de como você pode implementar um cache simples para segredos usando Google Secret

```csharp
using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Caching.Memory;

public class SecretManagerService
{
    private readonly SecretManagerServiceClient _secretManagerClient;
    private readonly IMemoryCache _cache;

    public SecretManagerService(SecretManagerServiceClient secretManagerClient, IMemoryCache cache)
    {
        _secretManagerClient = secretManagerClient;
        _cache = cache;
    }

    public string GetSecret(string projectId, string secretId, string versionId = "latest")
    {
        string cacheKey = $"{projectId}-{secretId}-{versionId}";
        if (!_cache.TryGetValue(cacheKey, out string secret))
        {
            var secretName = new SecretVersionName(projectId, secretId, versionId);
            var secretVersion = _secretManagerClient.AccessSecretVersion(secretName);
            secret = secretVersion.Payload.Data.ToStringUtf8();

            // Cache the secret for 1 hour
            _cache.Set(cacheKey, secret, TimeSpan.FromHours(1));
        }

        return secret;
    }
}

// No seu StorageService
public StorageService(
    ILogger<StorageService> logger,
    IOptions<StorageOptions> bucketOptions,
    SecretManagerService secretManagerService)
{
    _logger = logger;
    _storageOptions = bucketOptions.Value;

    if (_storageOptions == null || string.IsNullOrEmpty(_storageOptions.BucketName))
    {
        throw new InternalServerErrorException("Storage.StorageOptionsEmpty");
    }

    if (_storageOptions.CloudProvider == ECloudProvider.None)
    {
        throw new InternalServerErrorException("Storage.CloudProviderEmpty");
    }
    else if (_storageOptions.CloudProvider == ECloudProvider.Amazon && _storageOptions.AWS == null)
    {
        throw new InternalServerErrorException("Storage.CredentialsEmpty;AWS");
    }
    else if (_storageOptions.CloudProvider == ECloudProvider.Google)
    {
        if (_storageOptions.GCP == null)
        {
            throw new InternalServerErrorException("Storage.CredentialsEmpty;GCP");
        }

        // Carregar credenciais do Google Secret Manager e cachear
        var secretPayload = secretManagerService.GetSecret("my-project", "my-gcp-credentials");
        _storageOptions.GCP.CredentialsJson = secretPayload;
    }
}
```

## Resumo

Utilizar serviços de gerenciamento de segredos como Google Secret Manager ou AWS Secrets Manager é uma prática recomendada para garantir a segurança das credenciais. Implementar um cache de segredos pode mitigar a latência adicional introduzida pela busca de segredos, garantindo que o desempenho da aplicação não seja significativamente impactado. Carregar segredos na inicialização e armazená-los em memória é uma abordagem eficaz para equilibrar segurança e desempenho.
