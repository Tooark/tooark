# Tooark

Biblioteca de ferramentas C#

---

## Criar ou revisar funcionalidades

Clone o projeto (tem que ter missão de acesso)

`git clone https://github.com/Grupo-Jacto/tooark.git`

Acesse a pasta do projeto

`cd tooark`

Restaure configurações do projeto .NET

`dotnet restore`

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
