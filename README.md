# Tooark

Projeto com todos os recursos e funcionalidades do Tooark voltadas para projetos .NET [Link](/Tooark/README.md).

| Package                | Version                                                                                                | Downloads                                                                                               |
| ---------------------- | ------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------- |
| `Tooark`               | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark)               | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark)               |
| `Tooark.Attributes`    | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Attributes)    | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Attributes)    |
| `Tooark.Dtos`          | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Dtos)          | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Dtos)          |
| `Tooark.Entities`      | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Entities)      | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Entities)      |
| `Tooark.Exceptions`    | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Exceptions)    | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Exceptions)    |
| `Tooark.Extensions`    | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Extensions)    | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Extensions)    |
| `Tooark.Enums`         | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Enums)         | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Enums)         |
| `Tooark.Notifications` | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Notifications) | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Notifications) |
| `Tooark.Validations`   | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Validations)   | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Validations)   |
| `Tooark.ValueObjects`  | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.ValueObjects)  | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.ValueObjects)  |
| `Tooark.Utils`         | [![NuGet](https://img.shields.io/nuget/v/Tooark.svg)](https://nuget.org/packages/Tooark.Utils)         | [![Nuget](https://img.shields.io/nuget/dt/Tooark.svg)](https://nuget.org/packages/Tooark.Utils)         |

---

## Criar ou revisar funcionalidades

Clone o projeto (tem que ter permissão de acesso)

`git clone https://github.com/Tooark/tooark.git`

Acesse a pasta do projeto

`cd tooark`

Restaure configurações do projeto .NET

`dotnet restore`

---

## Documentações

- [Validação de tipos de arquivo suportados](./ValidFile.md)
- [IStringLocalizer](./JsonStringLocalizer.md)
- [Resources disponíveis](./Resources.md)
- [HttpClientService](./HttpClientService.md)
- [Serviço de Bucket](./BucketService.md)
- [AppException](./AppException.md)
- [Conversão de arquivo e Extração de extensão](./ConvertFile.md)

---

## Configurar package source para o Nuget utilizar também o GitHub Package

### Opção 1: Editar arquivo 'Nuget.Config' global

Abra o arquivo de configuração do Nuget.Config global:

`c:\Users\<seu-usuario>\AppData\Roaming\NuGet\NuGet.Config`

Revise as configurações conforme abaixo

```XML
<configuration>
  <packageSources>
    <... Manter aqui outras fontes que já utiliza ...>
    <add key="github" value="https://nuget.pkg.github.com/Grupo-Jacto/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <github>
      <add key="Username" value="<username-github>" />
      <add key="ClearTextPassword" value="<personal-access-tokens-classic>" />
    </github>
  </packageSourceCredentials>
</configuration>
```

### Opção 2: Criar arquivo 'Nuget.Config' dentro do projeto

No projeto que vai utilizar a biblioteca crie um arquivo 'Nuget.Config' e adicione as configurações abaixo. Lembre de adicionar o 'Nuget.Config' ao arquivo '.gitignore' para não subir para o repositório chave de acesso.

```XML
<configuration>
  <packageSources>
    <add key="github" value="https://nuget.pkg.github.com/Grupo-Jacto/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <github>
      <add key="Username" value="<username-github>" />
      <add key="ClearTextPassword" value="<personal-access-tokens-classic>" />
    </github>
  </packageSourceCredentials>
</configuration>
```

### Opção 3: Executar comando para configurar fonte de pacotes

Abra o terminal e execute o comando abaixo

```sh
dotnet nuget add source https://nuget.pkg.github.com/Grupo-Jacto/index.json -n github -u <username-github> -p <personal-access-tokens-classic> --store-password-in-clear-text
```

> ### Para todas as opções substitua os textos abaixo pelos seus dados de acesso
>
> - `<username-github>` <= Usuário do GitHub que tenha permissão de acesso ao pacote
> - `<personal-access-tokens-classic>` <= Token clássico do usuário do GitHub

---

## Configurar para exibir documentação do swagger

1. No arquivo `*.csproj` do projeto que vai utilizar a biblioteca Tooark adicione a configuração abaixo:

```XML
<PropertyGroup>
  <TooarkPackagePath Condition="'$(OS)' == 'Windows_NT'">$(UserProfile)\.nuget\packages\tooark\[version-using-tooark]\lib\[version-dotnet-tooark]\Tooark.xml</TooarkPackagePath>
  <TooarkPackagePath Condition="'$(OS)' != 'Windows_NT'">$(HOME)/.nuget/packages/tooark/[version-using-tooark]/lib/[version-dotnet-tooark]/Tooark.xml</TooarkPackagePath>
</PropertyGroup>
<Target Name="PostBuildPublish" AfterTargets="PostBuildEvent;Publish">
  <Copy SourceFiles="$(TooarkPackagePath)" DestinationFolder="$(OutputPath)" Condition="Exists('$(TooarkPackagePath)')" />
  <Copy SourceFiles="$(TooarkPackagePath)" DestinationFolder="$(PublishDir)" Condition="Exists('$(TooarkPackagePath)')" />
</Target>
```

2. No arquivo `Program.cs` adicione o código abaixo dentro do `builder.Services.AddSwaggerGen`.

```C#
var tooarkXmlFile = "Tooark.xml";
var tooarkXmlPath = Path.Combine(AppContext.BaseDirectory, tooarkXmlFile);

if (File.Exists(tooarkXmlPath))
{
  c.IncludeXmlComments(tooarkXmlPath);
}
```

O resultado do trecho do código do `Program.cs`:

```C#
builder.Services.AddSwaggerGen(c =>
{
  // SEU CÓDIGO AQUI...

  var tooarkXmlFile = "Tooark.xml";
  var tooarkXmlPath = Path.Combine(AppContext.BaseDirectory, tooarkXmlFile);

  if (File.Exists(tooarkXmlPath))
  {
    c.IncludeXmlComments(tooarkXmlPath);
  }
});
```

> ### Para a etapa 1 substitua os textos abaixo pelas informações da versão utilizada do pacote
>
> - `[version-using-tooark]` <= Versão do pacote que esta utilizando
> - `[version-dotnet-tooark]` <= Versão do dotnet do pacote que esta utilizando

---

## Colaboradores

Os seguintes colaboradores estão trabalhando no microsserviço:

| <img src="https://avatar-management--avatars.us-west-2.prod.public.atl-paas.net/62472c0ead6b7e006aa6225d/1adc1b60-182e-4cd2-9cad-668c8bf02ed0/128" width=150> |
| :-----------------------------------------------------------------------------------------------------------------------------------------------------------: |
|                                                                       **Paulo Freitas**                                                                       |
|                                                               <paulo.freitas@grupojacto.com.br>                                                               |
