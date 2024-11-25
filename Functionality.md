# Documentação do Projeto Tooark

## Visão Geral

O projeto Tooark é um conjunto de ferramentas e funções para projetos C#. Ele oferece diversas funcionalidades que facilitam o desenvolvimento de aplicações, incluindo manipulação de strings, validação de emails, serviços HTTP, entre outros.

## Injeção de Dependência

A classe `TooarkDependencyInjection` oferece métodos de extensão para configurar e adicionar serviços específicos da Tooark ao contêiner de injeção de dependência.

**Exemplo de uso:**

```csharp
using Microsoft.Extensions.DependencyInjection;
using Tooark.Injections;

var services = new ServiceCollection();
services.AddTooarkServices();
```

## Funcionalidades

### Manipulação de Strings

A classe `NormalizeValue` oferece métodos para normalizar strings, removendo caracteres especiais e acentos.

**Exemplo de uso:**

```csharp
using Tooark.Utils;

string normalized = NormalizeValue.Normalize("Olá, eu sou o Tooark!");
Console.WriteLine(normalized); // Output: OLAEUSOUOTOOARK
```

### Validação de Emails

A classe `Email` representa um email válido e oferece métodos para validação de endereços de email.

**Exemplo de uso:**

```csharp
using Tooark.ValueObjects;

Email email = new Email("teste@example.com");
Console.WriteLine(email.IsValid); // Output: True

Email email = new Email("_teste@example.com");
Console.WriteLine(email.IsValid); // Output: False
```

### Serviços HTTP

A interface `IHttpClientService` e a classe `HttpClientService` fornecem métodos para realizar requisições HTTP de forma simplificada.

**Exemplo de uso:**

```csharp
using Tooark.Interfaces;
using Tooark.Factories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTooarkServices();
var serviceProvider = services.BuildServiceProvider();
var httpClientService = serviceProvider.GetRequiredService<IHttpClientService>();

var response = await httpClientService.GetAsync("https://api.example.com/data");
Console.WriteLine(response.StatusCode); // Output: 200
```

### Extensões

A classe `StringExtensions` oferece métodos de extensão para manipulação de strings, como a ordenação por propriedade.

**Exemplo de uso:**

```csharp
using Tooark.Extensions;

var list = new List<MyClass> { /*...*/ };
var sortedList = list.OrderByProperty("PropertyName").ToList();
```

## Configuração

### Adicionando o Pacote ao Projeto

No arquivo `*.csproj` do projeto que vai utilizar a biblioteca Tooark, adicione a configuração abaixo:

```xml
<PropertyGroup>
  <TooarkPackagePath Condition="'$(OS)' == 'Windows_NT'">$(UserProfile)\.nuget\packages\tooark\[version-using-tooark]\lib\[version-dotnet-tooark]\Tooark.xml</TooarkPackagePath>
  <TooarkPackagePath Condition="'$(OS)' != 'Windows_NT'">$(HOME)/.nuget/packages/tooark/[version-using-tooark]/lib/[version-dotnet-tooark]/Tooark.xml</TooarkPackagePath>
</PropertyGroup>
<Target Name="PostBuildPublish" AfterTargets="PostBuildEvent;Publish">
  <Copy SourceFiles="$(TooarkPackagePath)" DestinationFolder="$(OutputPath)" Condition="Exists('$(TooarkPackagePath)')" />
  <Copy SourceFiles="$(TooarkPackagePath)" DestinationFolder="$(PublishDir)" Condition="Exists('$(TooarkPackagePath)')" />
</Target>
```

### Configurando a Documentação do Swagger

No arquivo `Program.cs`, adicione o código abaixo dentro do `builder.Services.AddSwaggerGen`:

```csharp
var tooarkXmlFile = "Tooark.xml";
var tooarkXmlPath = Path.Combine(AppContext.BaseDirectory, tooarkXmlFile);

if (File.Exists(tooarkXmlPath))
{
  c.IncludeXmlComments(tooarkXmlPath);
}
```

### Contribuidores

Os seguintes colaboradores estão trabalhando no microsserviço:

- Paulo Sergio de Freitas Junior

### Repositório

Para mais informações, visite o repositório do projeto no GitHub: [Tooark](https://github.com/Grupo-Jacto/tooark)
