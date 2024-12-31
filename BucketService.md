# Serviço de Bucket

O `Bucket Service` é responsável por gerenciar o armazenamento de arquivos em provedores de nuvem como AWS S3 e Google Cloud Storage. Ele oferece funcionalidades para criar, atualizar, deletar e baixar arquivos.

## Funcionalidades

- **Criar Arquivo**: Envia um arquivo para o bucket.
- **Atualizar Arquivo**: Substitui um arquivo existente no bucket.
- **Deletar Arquivo**: Remove um arquivo do bucket.
- **Baixar Arquivo**: Faz o download de um arquivo do bucket.

## Configuração

Para configurar o serviço de Bucket, adicione as opções de configuração no arquivo `appsettings.json`:

### AWS S3

```json
{
  "Bucket": {
    "BucketName": "nome-do-bucket",
    "FileSize": 1024,
    "CloudProvider": "AWS",
    "AWS": {
      "AccessKey": "sua-access-key",
      "SecretKey": "sua-secret-key"
    },
    "AWSRegion": RegionEndpoint.USEast1,
    "AWSAcl": S3CannedACL.PublicRead
  }
}
```

### Google Cloud Storage

```json
{
  "Bucket": {
    "BucketName": "nome-do-bucket",
    "FileSize": 1024,
    "CloudProvider": "GCP",
    "GCP": {
      "type": "service_account",
      "project_id": "seu-projeto-id",
      "private_key_id": "sua-private-key-id",
      "private_key": "sua-private-key",
      "client_email": "seu-client-email",
      "client_id": "seu-client-id",
      "auth_uri": "https://accounts.google.com/o/oauth2/auth",
      "token_uri": "https://oauth2.googleapis.com/token",
      "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
      "client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/seu-client-email"
    }
  }
}
```

### Injeção de Dependência

Em seguida, adicione o serviço de Bucket ao contêiner de injeção de dependência no arquivo `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// ...existing code...

builder.Services.AddBucketService(builder.Configuration);

// ...existing code...

var app = builder.Build();

// ...existing code...

app.Run();
```

## Exemplos de Uso

### Criar Arquivo

```csharp
public class BucketController : ControllerBase
{
  private readonly IBucketService _bucketService;

  public BucketController(IBucketService bucketService)
  {
    _bucketService = bucketService;
  }

  [HttpPost("upload")]
  public async Task<IActionResult> UploadFile(IFormFile file)
  {
    var result = await _bucketService.Create(file, file.FileName);
    return Ok(result);
  }
}
```

### Atualizar Arquivo

```csharp
public class BucketController : ControllerBase
{
  private readonly IBucketService _bucketService;

  public BucketController(IBucketService bucketService)
  {
    _bucketService = bucketService;
  }

  [HttpPut("update")]
  public async Task<IActionResult> UpdateFile(string oldFile, IFormFile file)
  {
    var result = await _bucketService.Update(oldFile, file, file.FileName);
    return Ok(result);
  }
}
```

### Deletar Arquivo

```csharp
public class BucketController : ControllerBase
{
  private readonly IBucketService _bucketService;

  public BucketController(IBucketService bucketService)
  {
    _bucketService = bucketService;
  }

  [HttpDelete("delete")]
  public async Task<IActionResult> DeleteFile(string fileName)
  {
    await _bucketService.Delete(fileName);
    return NoContent();
  }
}
```

### Baixar Arquivo

```csharp
public class BucketController : ControllerBase
{
  private readonly IBucketService _bucketService;

  public BucketController(IBucketService bucketService)
  {
    _bucketService = bucketService;
  }

  [HttpGet("download")]
  public async Task<IActionResult> DownloadFile(string fileName)
  {
    var fileStream = await _bucketService.Download(fileName);
    return File(fileStream, "application/octet-stream", fileName);
  }
}
```
