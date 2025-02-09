# Tooark.Extensions

Biblioteca para criar e gerenciar extensões e utilitários, facilitando o desenvolvimento e a manutenção de projetos .NET.

## Conteúdo

- [Enumerable Extensions](#enumerable-extensions)
- [Json String Localizer Extensions](#json-string-localizer-extensions-extensões-para-istringlocalizer)
- [String Extensions](#string-extensions)

## Funcionalidades e Métodos Disponíveis

### Enumerable Extensions

#### Métodos Enumerable

- `OrderByProperty<T>(string sortProperty)`: Ordena uma coleção de objetos de forma ascendente por uma propriedade específica.
- `OrderByPropertyDescending<T>(string sortProperty)`: Ordena uma coleção de objetos de forma descendente por uma propriedade específica

#### Exemplos de Uso Enumerable

**OrderByProperty com Parâmetro Simples:**

```csharp
using Tooark.Extensions;

List<MyObject> list = [{"Name": "B", "Age": 20}, {"Name": "A", "Age": 30}, {"Name": "C", "Age": 10}];

var sortedList = list.OrderByProperty("Name").toList();
// [{"Name": "A", "Age": 30}, {"Name": "B", "Age": 20}, {"Name": "C", "Age": 10}]
```

**OrderByPropertyDescending com Parâmetro Simples:**

```csharp
using Tooark.Extensions;

List<MyObject> list = [{"Name": "B", "Age": 20}, {"Name": "A", "Age": 30}, {"Name": "C", "Age": 10}];

var sortedList = list.OrderByPropertyDescending("Name").toList();
// [{"Name": "C", "Age": 10}, {"Name": "B", "Age": 20}, {"Name": "A", "Age": 30}]
```

**OrderByPropertyDescending com Parâmetro Complexo:**

```csharp
using Tooark.Extensions;

List<MyObject> list = [
  {"Name": "B", "Age": 20, "Address": {"City": "City C"}},
  {"Name": "A", "Age": 30, "Address": {"City": "City B"}},
  {"Name": "C", "Age": 10, "Address": {"City": "City A"}}
];

var sortedList = list.OrderByProperty("Address.City").toList();
// [
//  {"Name": "C", "Age": 10, "Address": {"City": "City A"}},
//  {"Name": "A", "Age": 30, "Address": {"City": "City B"}},
//  {"Name": "B", "Age": 20, "Address": {"City": "City C"}}
//]
```

### Json String Localizer Extensions (Extensões para IStringLocalizer)

Utiliza os [arquivos](#arquivos-de-recursos-multiculturais) de recursos multiculturais para localização de strings.

#### Métodos Json String Localizer

- `LocalizedString this[string name]`: Representa um valor localizado.
- `LocalizedString this[string name, params object[] arguments]`: Representa um valor localizado com argumentos.
- `GetAllStrings(bool includeParentCultures)`: Obtém todos os valores localizados. Se `includeParentCultures` utilizado para valores da cultura `default` caso `true` ou `current` caso `false`.

#### Exemplos de Uso Json String Localizer

**Obter Valor Localizado:**

```csharp
using Tooark.Extensions;

var localizedString = _localizer["Field"]; // "Campo"
```

**Obter Valor Localizado com Argumentos juntos:**

```csharp
using Tooark.Extensions;

var localizedString = _localizer["Field.Empty;Name"]; // "O campo Name está vazio"
```

**Obter Valor Localizado com Argumentos:**

```csharp
using Tooark.Extensions;

var localizedString = _localizer["Field.Empty", "Name"]; // "O campo Name está vazio"
```

### Configuração do JsonStringLocalizer

#### Métodos de Injeção

- `AddJsonStringLocalizer`: Adiciona o serviço de localização de strings com base em arquivos JSON.

#### Parâmetros de Injeção

- `LocalizerOptions`: Opções de configuração para o serviço de localização de strings.

#### Opções de Configuração

- `ResourceAdditionalPath`: Lista de caminhos adicionais para arquivos de recursos.
- `ResourceAdditionalStream`: Lista de streams adicionais para arquivos de recursos.

#### Parâmetros de Recursos Adicionais

**ResourceAdditionalPath:**

- `LanguageCode`: Código da cultura do recurso adicional.
- `Path`: Caminho do arquivo adicional. Para funcionar, o nome do arquivo sempre deve iniciar com o código da cultura.

**ResourceAdditionalStream:**

- `LanguageCode`: Código da cultura do recurso adicional.
- `Stream`: Stream do arquivo adicional.

#### Exemplos de Configuração

**Configuração com Caminhos Adicionais:**

```csharp
using Microsoft.Extensions.DependencyInjection;
using Tooark.Extensions.Options;
using Tooark.Extensions.Injections;

var services = new ServiceCollection();

services.AddJsonStringLocalizer(new LocalizerOptions
{
  ResourceAdditionalPath = 
  [
    new ("en-US", "path/to/en-US.json"),
    new ("pt-BR", "path/to/pt-BR.json")
  ]
});
```

**Configuração com Streams Adicionais:**

```csharp
using Microsoft.Extensions.DependencyInjection;
using Tooark.Extensions.Options;
using Tooark.Extensions.Injections;
using System.IO;

var services = new ServiceCollection();

services.AddJsonStringLocalizer(new LocalizerOptions
{
  ResourceAdditionalStream = 
  [
    new ("en-US", new FileStream("path/to/en-US.json", FileMode.Open)),
    new ("pt-BR", new FileStream("path/to/pt-BR.json", FileMode.Open))
  ]
});
```

### String Extensions

#### Métodos String

- `ToNormalize`: Normaliza uma string removendo espaços, convertendo para maiúscula e substituindo caracteres especiais.
- `ToNormalizeRegex`: Normaliza uma string removendo espaços, convertendo para maiúscula e substituindo caracteres especiais usando expressões regulares.
- `FromSnakeToPascalCase`: Converte uma string de snake_case para PascalCase.
- `FromSnakeToCamelCase`: Converte uma string de snake_case para camelCase.
- `FromSnakeToKebabCase`: Converte uma string de snake_case para kebab-case.
- `FromPascalToSnakeCase`: Converte uma string de PascalCase para snake_case.
- `FromCamelToSnakeCase`: Converte uma string de camelCase para snake_case.
- `FromKebabToSnakeCase`: Converte uma string de kebab-case para snake_case.

#### Exemplos de Uso String

**ToNormalize:**

```csharp
using Tooark.Extensions;

string value = "Olá Mundo!";
string normalizedValue = value.ToNormalize(); // OLAMUNDO
```

**ToNormalizeRegex:**

```csharp
using Tooark.Extensions;

string value = "Olá Mundo!";
string normalizedValue = value.ToNormalizeRegex(); // OLAMUNDO
```

**FromSnakeToPascalCase:**

```csharp
using Tooark.Extensions;

string value = "hello_world";
string pascalCaseValue = value.FromSnakeToPascalCase(); // HelloWorld
```

**FromSnakeToCamelCase:**

```csharp
using Tooark.Extensions;

string value = "hello_world";
string camelCaseValue = value.FromSnakeToCamelCase(); // helloWorld
```

**FromSnakeToKebabCase:**

```csharp
using Tooark.Extensions;

string value = "hello_world";
string kebabCaseValue = value.FromSnakeToKebabCase(); // hello-world
```

**FromPascalToSnakeCase:**

```csharp
using Tooark.Extensions;

string value = "HelloWorld";
string snakeCaseValue = value.FromPascalToSnakeCase(); // hello_world
```

**FromCamelToSnakeCase:**

```csharp
using Tooark.Extensions;

string value = "helloWorld";
string snakeCaseValue = value.FromCamelToSnakeCase(); // hello_world
```

**FromKebabToSnakeCase:**

```csharp
using Tooark.Extensions;

string value = "hello-world";
string snakeCaseValue = value.FromKebabToSnakeCase(); // hello_world
```

## Arquivos de Recursos Multiculturais

- [en-US.json](./Resources/en-US.default.json)
- [es-ES.json](./Resources/es-ES.default.json)
- [pt-BR.json](./Resources/pt-BR.default.json)
- [pt-PT.json](./Resources/pt-PT.default.json)

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests no repositório [Tooark.Extensions](https://github.com/Tooark/tooark).

## Licença

Este projeto está licenciado sob a licença BSD 3-Clause. Veja o arquivo [LICENSE](../LICENSE) para mais detalhes.
