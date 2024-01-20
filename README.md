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

``` XML
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

---

## Estrutura do projeto

- tooark
  - .github
    - workflows
  - Media
  - Tooark
    - Extensions
    - Types
  - Tooark.Benchmarks
    - Benchmarks
    - Models
    - Services
  - Tooark.Tests
    - Extensions
    - Models
