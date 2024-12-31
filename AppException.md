# AppException

A classe `AppException` representa uma exceção que é lançada quando uma requisição HTTP falha.

## Propriedades

- `HttpStatusCode HttpStatusCode`: Código de status da exceção.

## Métodos

### Construtores

- `AppException()`: Cria uma exceção BadRequest (400) com a mensagem padrão.
- `AppException(string message)`: Cria uma exceção BadRequest (400) com a mensagem especificada.
- `AppException(string message, HttpStatusCode statusCode)`: Cria uma exceção com a mensagem e o código de status especificados.

### Métodos Estáticos

- `static void Configure(IStringLocalizer localizer)`: Configura o localizador de strings.

- `static AppException BadRequest(string message = "BadRequest")`: Cria uma exceção BadRequest (400).
- `static AppException Unauthorized(string message = "Unauthorized")`: Cria uma exceção Unauthorized (401).
- `static AppException Forbidden(string message = "Forbidden")`: Cria uma exceção Forbidden (403).
- `static AppException NotFound(string message = "NotFound")`: Cria uma exceção NotFound (404).
- `static AppException Conflict(string message = "Conflict")`: Cria uma exceção Conflict (409).
- `static AppException PayloadTooLarge(string message = "PayloadTooLarge")`: Cria uma exceção PayloadTooLarge (413).
- `static AppException InternalServerError(string message = "InternalServerError")`: Cria uma exceção InternalServerError (500).
- `static AppException ServiceUnavailable(string message = "ServiceUnavailable")`: Cria uma exceção ServiceUnavailable (503).

## Configuração

Adicione o AppException ao contêiner de injeção de dependência no arquivo `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// ...existing code...

builder.Services.AddAppException();

// ...existing code...

var app = builder.Build();

// ...existing code...

app.Run();
```

## Exemplos de uso

```csharp
using Tooark.Exceptions;

try
{
  throw AppException.BadRequest("Bad Request");
}
catch (AppException ex)
{
  Console.WriteLine(ex.Message); // Output: Bad Request
}
```

```csharp
using Tooark.Exceptions;

try
{
  throw AppException.BadRequest("CustomError");
}
catch (AppException ex)
{
  Console.WriteLine(ex.Message); // Output: CustomError
}
```

```csharp
using Tooark.Exceptions;

try
{
  throw AppException.BadRequest();
}
catch (AppException ex)
{
  Console.WriteLine(ex.Message); // Output: Bad Request
}
```
