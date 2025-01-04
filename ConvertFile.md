# Documentação de Utilização das Funções de Conversão e Extração de Extensão

## Funções Disponíveis

### ConvertBase64ToMemoryStream

Converte uma string base64 para `MemoryStream`.

#### Assinatura

```csharp
public static MemoryStream? ConvertBase64ToMemoryStream(string stringFile)
public static MemoryStream? ConvertBase64ToMemoryStream(IFormFile fromFile)
```

#### Parâmetros

- `stringFile`: String de arquivo em base64.
- `fromFile`: Arquivo em formato de `IFormFile`.

#### Retorno

Retorna um `MemoryStream` ou `null` em caso de erro.

#### Exemplo de Uso

```csharp
string base64String = "data:application/pdf;base64,JVBERi0xLjQKJcfs"; // Exemplo de string base64
MemoryStream? memoryStream = Util.ConvertBase64ToMemoryStream(base64String);

IFormFile formFile = ...; // Suponha que formFile seja um arquivo válido
MemoryStream? memoryStreamFromFile = Util.ConvertBase64ToMemoryStream(formFile);
```

### ExtractExtension

Extrai a extensão do arquivo a partir de uma string base64.

#### Assinatura

```csharp
public static string? ExtractExtension(string stringFile)
public static string? ExtractExtension(IFormFile fromFile)
```

#### Parâmetros

- `stringFile`: String de arquivo em base64.
- `fromFile`: Arquivo em formato de `IFormFile`.

#### Retorno

Retorna a extensão do arquivo ou `null` em caso de erro.

#### Exemplo de Uso

```csharp
string base64String = "data:application/pdf;base64,JVBERi0xLjQKJcfs"; // Exemplo de string base64
string? extension = Util.ExtractExtension(base64String);

IFormFile formFile = ...; // Suponha que formFile seja um arquivo válido
string? extensionFromFile = Util.ExtractExtension(formFile);
```

## Notas

- As funções utilizam métodos internos para realizar as conversões e extrações.
- Certifique-se de que a string base64 está corretamente formatada antes de chamar as funções.
