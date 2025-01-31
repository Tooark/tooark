# Tooark.Utils

Este pacote contém várias classes utilitárias para normalização de strings, gerenciamento de idiomas, obtenção de informações localizadas, geração de strings, validação de arquivos e conversão de arquivos.

## Conteúdo

- [FileConvert](#fileconvert)
- [FileValid](#filevalid)
- [GenerateString](#generatestring)
- [GetInfo](#getinfo)
- [Language](#language)
- [Normalize](#normalize)

## Funcionalidades e Métodos Disponíveis

### Language

Classe estática que contém constantes e propriedades para gerenciamento de idiomas.

#### Métodos

- `SetCulture(string culture)`: Define a cultura atual para a aplicação usando o nome da cultura no formato `xx-XX`.
- `SetCulture(CultureInfo culture)`: Define a cultura atual para a aplicação usando um objeto `CultureInfo`.

#### Propriedades

- `Default`: O código de idioma padrão usado na aplicação. Padrão "en-US".
- `Current`: O código de idioma atual do ambiente de execução.
- `CurrentCulture`: A cultura atual do ambiente de execução.

#### Exemplo de Uso

```csharp
using Tooark.Utils;
using System.Globalization;

Language.SetCulture("pt-BR");
CultureInfo currentCulture = Language.CurrentCulture; // pt-BR
```

### Normalize

Classe estática que fornece métodos para normalização de strings.

#### Métodos

- `Value(string value)`: Normaliza um valor removendo espaços, convertendo para maiúscula e substituindo caracteres especiais.
- `ValueRegex(string value)`: Normaliza um valor removendo espaços, convertendo para maiúscula e substituindo caracteres especiais usando expressões regulares.

#### Exemplo de Uso

```csharp
using Tooark.Utils;

string normalizedValue = Normalize.Value("Olá Mundo!"); // OLAMUNDO
string normalizedValueRegex = Normalize.ValueRegex("Olá Mundo!"); // OLAMUNDO
```

### GetInfo

Classe estática que fornece métodos para buscar campos `name`, `title`, `description` e `custom` de uma lista de objetos.

#### Métodos

- `Name<T>(IList<T> list, string? languageCode = null)`: Obtém o nome localizado de uma lista de objetos.
- `Title<T>(IList<T> list, string? languageCode = null)`: Obtém o título localizado de uma lista de objetos.
- `Description<T>(IList<T> list, string? languageCode = null)`: Obtém a descrição localizada de uma lista de objetos.
- `Custom<T>(IList<T> list, string property, string? languageCode = null)`: Obtém um valor localizado de uma propriedade em uma lista de objetos.

#### Exemplo de Uso

```csharp
using Tooark.Utils;

var list = new List<MyObject> { /* ... */ };
string name = GetInfo.Name(list);
string title = GetInfo.Title(list);
string description = GetInfo.Description(list);
string customValue = GetInfo.Custom(list, "CustomProperty");
```

### GenerateString

Classe estática que fornece métodos para gerar strings.

#### Métodos

- `Sequential(int number)`: Converte um número inteiro em uma representação equivalente alfabética do número.
- `Password(int len = 12, bool upper = true, bool lower = true, bool number = true, bool special = true, bool similarity = false)`: Gera uma string com critérios específicos.
- `Hexadecimal(int sizeToken = 128)`: Gera uma string hexadecimal aleatória.
- `GuidCode()`: Gera uma string Guid sem hífens.
- `Token(int length = 256)`: Gera uma string de token.

#### Exemplo de Uso

```csharp
using Tooark.Utils;

string sequential = GenerateString.Sequential(27); // AA
string password = GenerateString.Password(); // 1aB2cD3eF4gH
string hex = GenerateString.Hexadecimal(); // 0x1A2B3C4D5E6F...
string guid = GenerateString.GuidCode(); // 1A2B3C4D5E6F7G8H9I0J1K2L3M4N5O6P
string token = GenerateString.Token(); // 1A2B3C4D5E6F7G8H9I0J1K2L3M4N5O6P...
```

### FileValid

Classe estática que fornece métodos para verificar a validade de arquivos.

#### Métodos

- `IsImage(IFormFile file, long fileSize = 0)`: Verifica se o arquivo é uma imagem válida.
- `IsDocument(IFormFile file, long fileSize = 0)`: Verifica se o arquivo é um documento válido.
- `IsVideo(IFormFile file, long fileSize = 0)`: Verifica se o arquivo é um vídeo válido.
- `IsCustom(IFormFile file, long fileSize = 0, string[]? permittedExtensions = null)`: Verifica se o arquivo é válido para extensões personalizadas.

#### Exemplo de Uso

```csharp
using Tooark.Utils;
using Microsoft.AspNetCore.Http;

bool isImage = FileValid.IsImage(formFile);
bool isDocument = FileValid.IsDocument(formFile);
bool isVideo = FileValid.IsVideo(formFile);
bool isCustom = FileValid.IsCustom(formFile, permittedExtensions: new[] { ".TXT", ".CSV" });
```

### FileConvert

Classe estática que fornece métodos para conversão de arquivos e extração de extensões.

#### Métodos

- `ToMemoryStream(string stringFile)`: Converte uma string base64 para `MemoryStream`.
- `ToMemoryStream(IFormFile fromFile)`: Converte um `IFormFile` para `MemoryStream`.
- `Extension(string stringFile)`: Extrai a extensão do arquivo de uma string base64.
- `Extension(IFormFile fromFile)`: Extrai a extensão do arquivo de um `IFormFile`.

#### Exemplo de Uso

```csharp
using Tooark.Utils;
using Microsoft.AspNetCore.Http;

MemoryStream? memoryStream = FileConvert.ToMemoryStream(base64String);
MemoryStream? memoryStreamFromFile = FileConvert.ToMemoryStream(formFile);
string? extension = FileConvert.Extension(base64String);
string? extensionFromFile = FileConvert.Extension(formFile);
```

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests no repositório [Tooark.Validations](https://github.com/Tooark/tooark).

## Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](../LICENSE) para mais detalhes.
