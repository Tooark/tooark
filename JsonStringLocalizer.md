# IStringLocalizer

## Introdução

Este pacote fornece uma implementação de `IStringLocalizer` que utiliza arquivos JSON para localizar strings. A seguir, você encontrará a documentação dos métodos e funções, bem como instruções de configuração e exemplos de uso. Recursos disponíveis: [Resources](./Resources.md).

## Métodos e Funções

### JsonStringLocalizerDependencyInjection

#### `AddJsonStringLocalizer`

Adiciona o serviço `JsonStringLocalizer` ao contêiner de injeção de dependência.

**Parâmetros:**

- `services` (IServiceCollection): A coleção de serviços para adicionar o serviço `JsonStringLocalizer`.
- `localizerOptions` (LocalizerOptions, opcional): Configurações para o serviço de localização de recursos.

**Retorno:**

- `IServiceCollection`: A coleção de serviços com o serviço `JsonStringLocalizer` adicionado.

### JsonStringLocalizerFactory

#### `Create(Type resourceSource)`

Cria um novo `JsonStringLocalizerExtension` para o tipo de recurso especificado.

**Parâmetros:**

- `resourceSource` (Type): O tipo do recurso.

**Retorno:**

- `IStringLocalizer`: Uma nova instância de `JsonStringLocalizerExtension`.

#### `Create(string baseName, string location)`

Cria um novo `JsonStringLocalizerExtension` para o nome base e localização especificados.

**Parâmetros:**

- `baseName` (string): O nome base do recurso.
- `location` (string): A localização do recurso.

**Retorno:**

- `IStringLocalizer`: Uma nova instância de `JsonStringLocalizerExtension`.

### JsonStringLocalizerExtension

#### `this[string name]`

Obtém uma string localizada com base no nome fornecido.

**Parâmetros:**

- `name` (string): O nome (key) da string localizada a ser obtida.

**Retorno:**

- `LocalizedString`: Uma instância de `LocalizedString` contendo a string localizada, ou o nome fornecido para busca.

#### `this[string name, params object[] arguments]`

Obtém uma string localizada formatada com base no nome fornecido e nos argumentos.

**Parâmetros:**

- `name` (string): O nome da string localizada.
- `arguments` (object[]): Os argumentos a serem formatados na string localizada.

**Retorno:**

- `LocalizedString`: Um objeto `LocalizedString` que contém a string localizada formatada com os argumentos fornecidos.

#### `GetAllStrings(bool includeParentCultures)`

Recupera todas as strings localizadas de um arquivo JSON para a cultura especificada.

**Parâmetros:**

- `includeParentCultures` (bool): Se verdadeiro, a cultura atual é usada. Caso contrário, a cultura padrão é usada.

**Retorno:**

- `IEnumerable<LocalizedString>`: Uma coleção enumerável de `LocalizedString` contendo todas as strings localizadas.

### InternalJsonStringLocalizer

#### `GetLocalizedString(string keyParameter, string? cultureSelect = null)`

Obtém uma string localizada com base na key fornecida, suporta parâmetro de idioma.

**Parâmetros:**

- `keyParameter` (string): A key da string localizada a ser obtida.
- `cultureSelect` (string, opcional): O código da cultura para selecionar o arquivo JSON apropriado. Se nulo ou vazio, a cultura atual é usada.

**Retorno:**

- `string`: Uma instância de `LocalizedString` contendo a string localizada, ou a key fornecida para busca.

#### `GetAllStrings(bool includeParentCultures)`

Recupera todas as strings localizadas de um arquivo JSON para a cultura especificada.

**Parâmetros:**

- `includeParentCultures` (bool): Se verdadeiro, a cultura atual é usada. Caso contrário, a cultura padrão é usada.

**Retorno:**

- `IEnumerable<LocalizedString>`: Uma coleção enumerável de `LocalizedString` contendo todas as strings localizadas.

## Configuração

Para configurar o `JsonStringLocalizer` em seu projeto, siga os passos abaixo:

1. Adicione o pacote `Tooark` ao seu projeto.
2. Configure o serviço `JsonStringLocalizer` no contêiner de injeção de dependência no `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// ...existing code...

builder.Services.AddJsonStringLocalizer();

// ...existing code...

var app = builder.Build();

// ...existing code...

app.Run();
```

### Opções de Configuração

```csharp
var builder = WebApplication.CreateBuilder(args);

// ...existing code...

var localizerOptions = new LocalizerOptions
{
  ResourceAdditionalPaths = new Dictionary<string, string>
  {
    { "en-US", "Resources/en-US.additional.json" },
    { "pt-BR", "Resources/pt-BR.additional.json" }
  },
  FileStream = new FileStream("Resources/stream.json", FileMode.Open)
};

services.AddJsonStringLocalizer(localizerOptions);

// ...existing code...

var app = builder.Build();

// ...existing code...

app.Run();
```

## Exemplos de Uso

### Obtendo uma String Localizada

```csharp
using Microsoft.Extensions.Localization;

var serviceProvider = services.BuildServiceProvider();
var localizer = serviceProvider.GetRequiredService<IStringLocalizer>();

var localizedString = localizer["Hello"];
Console.WriteLine(localizedString);
```

### Obtendo uma String Localizada com Argumentos

```csharp
var localizedStringWithArgs = localizer["Hello {0}", "World"];
Console.WriteLine(localizedStringWithArgs);
```

### Obtendo Todas as Strings Localizadas

```csharp
var allStrings = localizer.GetAllStrings(includeParentCultures: true);
foreach (var localizedString in allStrings)
{
  Console.WriteLine($"{localizedString.Name}: {localizedString.Value}");
}
```
