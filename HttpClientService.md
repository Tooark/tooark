# HttpClientService

O `HttpClientService` fornece métodos para enviar solicitações HTTP e receber respostas HTTP de um recurso identificado por um URI.

## Configuração

Para configurar o `HttpClientService` no seu projeto, siga os passos abaixo:

Registre o serviço no `Program.cs`.

```csharp
var builder = WebApplication.CreateBuilder(args);

// ...existing code...

builder.Services.AddHttpClientService();

// ...existing code...

var app = builder.Build();

// ...existing code...

app.Run();
```

## Exemplos de Uso

### Enviar uma solicitação GET

```csharp
public class ExampleService
{
  private readonly IHttpClientService _httpClientService;

  public ExampleService(IHttpClientService httpClientService)
  {
    _httpClientService = httpClientService;
  }

  public async Task<MyResponseType?> GetExampleAsync()
  {
    string requestUri = "https://api.example.com/resource";
    return await _httpClientService.GetFromJsonAsync<MyResponseType>(requestUri);
  }
}
```

### Enviar uma solicitação POST

```csharp
public class ExampleService
{
  private readonly IHttpClientService _httpClientService;

  public ExampleService(IHttpClientService httpClientService)
  {
    _httpClientService = httpClientService;
  }

  public async Task<HttpResponseMessage> PostExampleAsync(MyRequestType requestData)
  {
    string requestUri = "https://api.example.com/resource";
    return await _httpClientService.PostAsJsonAsync(requestUri, requestData);
  }
}
```

### Enviar uma solicitação PUT

```csharp
public class ExampleService
{
  private readonly IHttpClientService _httpClientService;

  public ExampleService(IHttpClientService httpClientService)
  {
    _httpClientService = httpClientService;
  }

  public async Task<HttpResponseMessage> PutExampleAsync(MyRequestType requestData)
  {
    string requestUri = "https://api.example.com/resource";
    return await _httpClientService.PutAsJsonAsync(requestUri, requestData);
  }
}
```

### Enviar uma solicitação DELETE

```csharp
public class ExampleService
{
  private readonly IHttpClientService _httpClientService;

  public ExampleService(IHttpClientService httpClientService)
  {
    _httpClientService = httpClientService;
  }

  public async Task<HttpResponseMessage> DeleteExampleAsync()
  {
    string requestUri = "https://api.example.com/resource";
    return await _httpClientService.DeleteAsJsonAsync(requestUri);
  }
}
```

### Enviar uma solicitação PATCH

```csharp
public class ExampleService
{
  private readonly IHttpClientService _httpClientService;

  public ExampleService(IHttpClientService httpClientService)
  {
    _httpClientService = httpClientService;
  }

  public async Task<HttpResponseMessage> PatchExampleAsync(MyRequestType requestData)
  {
    string requestUri = "https://api.example.com/resource";
    return await _httpClientService.PatchAsJsonAsync(requestUri, requestData);
  }
}
```
