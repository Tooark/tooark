# Tooark.Entities

Biblioteca com entidades base para aplicações .NET, incluindo suporte a identificadores únicos, auditoria, controle de versão e exclusão lógica.

## 📦 Conteúdo do Pacote

### Entidades

| Classe                                        | Descrição                                                                                                                                         |
| --------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------- |
| [`BaseEntity`](#baseentity)                   | Identificador único + suporte a notificações/validações                                                                                           |
| [`InitialEntity`](#initialentity)             | Informações de criação (`CreatedById`/`CreatedAt`)                                                                                                  |
| [`DetailedEntity`](#detailedentity)           | Informações de atualização (`UpdatedById`/`UpdatedAt`)                                                                                            |
| [`VersionedEntity`](#versionedentity)         | Controle de versão (`Version`) incrementada em atualizações                                                                                       |
| [`SoftDeletableEntity`](#softdeletableentity) | Exclusão lógica simples (`Deleted`) + atualização via `UpdatedById`                                                                               |
| [`AuditableEntity`](#auditableentity)         | Auditoria completa: versão (`Version`) + exclusão(`Deleted`)/restauração com usuário/data (`DeletedById`/`DeletedAt`/`RestoredById`/`RestoredAt`) |
| [`FileEntity`](#fileentity)                   | Entidade base para arquivos (`FileName`, `Title`, `Link`, `FileFormat`, `Type`, `Size`)                                                           |

### Value Objects usados nas entidades

As entidades usam Value Objects do pacote `Tooark.ValueObjects` (ex.: `CreatedById`, `UpdatedById`, `DeletedById`, `RestoredById`, `FileStorage`, `Title`).

---

## 🔧 Instalação

```bash
dotnet add package Tooark.Entities
```

---

## ⚙️ Configuração

Não há configuração adicional.

---

## 🧩 Entidades (Detalhes)

### BaseEntity

- **Propriedades**
  - `Id` (Guid) — coluna `id` (`uuid`)
- **Construtores (para classes derivadas)**
  - `BaseEntity()` — gera `Id` automaticamente
  - `BaseEntity(Guid id)` — define `Id` determinístico (seed/testes/factories)
- **Observações**
  - O `Id` tem setter privado; não existe `SetId` público.
  - [Exemplos de Uso](#entidade-base).

### InitialEntity

- **Propriedades**
  - `CreatedById` (Guid) — coluna `created_by` (`uuid`)
  - `CreatedAt` (DateTime/UTC) — coluna `created_at` (`timestamp with time zone`)
- **Métodos**
  - `SetCreatedBy(CreatedBy createdById)`
- **Observações**
  - Herda de `BaseEntity`.
  - `CreatedById` é Value Object e aceita conversão implícita a partir de `Guid`.
  - Em caso de dados inválidos, lança `BadRequestException`.
  - [Exemplos de Uso](#entidade-inicial).

### DetailedEntity

- **Propriedades**
  - `UpdatedById` (Guid) — coluna `updated_by` (`uuid`)
  - `UpdatedAt` (DateTime/UTC) — coluna `updated_at` (`timestamp with time zone`)
- **Métodos**
  - `SetCreatedBy(CreatedBy createdById)` — define também `UpdatedById`
  - `SetUpdatedBy(UpdatedBy updatedById)`
- **Observações**
  - Herda de `InitialEntity`.
  - `UpdatedById` é Value Object e aceita conversão implícita a partir de `Guid`.
  - Em caso de dados inválidos, lança `BadRequestException`.
  - [Exemplos de Uso](#entidade-detalhada).

### VersionedEntity

- **Propriedades**
  - `Version` (long) — coluna `version` (`bigint`), valor padrão `1`
- **Métodos**
  - `SetUpdatedBy(UpdatedBy updatedById)` — atualiza e incrementa a versão
- **Observações**
  - Herda de `DetailedEntity`.
  - Em caso de dados inválidos, lança `BadRequestException`.
  - [Exemplos de Uso](#entidade-versionada).

### SoftDeletableEntity

- **Propriedades**
  - `Deleted` (bool) — coluna `deleted` (`bool`), valor padrão `false`
- **Métodos**
  - `ValidateNotDeleted()` — valida se não está deletada e adiciona notificação
  - `EnsureNotDeleted()` — lança exception se estiver deletada
  - `SetDeleted(UpdatedBy changedById)` — marca como deletada e atualiza
  - `SetRestored(UpdatedBy changedById)` — restaura e atualiza
- **Observações**
  - Herda de `DetailedEntity`.
  - Em caso de dados inválidos, lança `BadRequestException`.
  - [Exemplos de Uso](#entidade-deletável).

### AuditableEntity

- **Propriedades**
  - `Version` (long)
  - `Deleted` (bool)
  - `DeletedById` (Guid?) — coluna `deleted_by` (`uuid`)
  - `DeletedAt` (DateTime?) — coluna `deleted_at` (`timestamp with time zone`)
  - `RestoredById` (Guid?) — coluna `restored_by` (`uuid`)
  - `RestoredAt` (DateTime?) — coluna `restored_at` (`timestamp with time zone`)
- **Métodos**
  - `ValidateNotDeleted()` — valida se não está deletada e adiciona notificação
  - `EnsureNotDeleted()` — lança exception se estiver deletada
  - `SetUpdatedBy(UpdatedBy updatedById)` — atualiza e incrementa a versão
  - `SetDeleted(DeletedById deletedById)` — marca como deletada, registra o usuário e a data da exclusão, e incrementa a versão
  - `SetRestored(RestoredById restoredById)` — restaura, registra o usuário e a data da restauração, e incrementa a versão
- **Observações**
  - Herda de `DetailedEntity`.
  - Em caso de dados inválidos, lança `BadRequestException`.
  - [Exemplos de Uso](#entidade-auditável).

### FileEntity

- **Propriedades**
  - `FileName` (string) — coluna `file_name` (`text`)
  - `Title` (string) — coluna `title` (`varchar(255)`)
  - `Link` (string) — coluna `link` (`text`)
  - `FileFormat` (string?) — coluna `file_format` (`varchar(10)`)
  - `Type` (EFileType) — coluna `type` (`int`)
  - `Size` (long) — coluna `size` (`bigint`)
- **Construtores (para classes derivadas)**
  - `FileEntity(FileStorage file, Title title, CreatedBy createdById)`
  - `FileEntity(FileStorage file, Title title, string fileFormat, EFileType type, long size, CreatedBy createdById)`
- **Observações**
  - Herda de `InitialEntity`.
  - `FileStorage` e `Title` são Value Objects. Em caso de dados inválidos, lança `BadRequestException`.
  - [Exemplos de Uso](#entidade-de-arquivo).

---

## 📝 Exemplos de Uso

### [Entidade Base](#baseentity)

```csharp
using Tooark.Entities;

public class Produto : BaseEntity
{
  public Produto() { }
  public Produto(Guid id) : base(id) { }

  public string Nome { get; set; } = string.Empty;
  public decimal Valor { get; set; }
}

public class Program
{
  public static void Main()
  {
    var produto = new Produto
    {
      Nome = "Produto A",
      Valor = 100.0m
    };

    var idGerado = produto.Id;
    var produtoDeterministico = new Produto(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));
  }
}
```

### [Entidade Inicial](#initialentity)

```csharp
using Tooark.Entities;

public class Produto : InitialEntity
{
  public string Nome { get; set; } = string.Empty;
  public decimal Valor { get; set; }
}

public class Program
{
  public static void Main()
  {
    var produto = new Produto
    {
      Nome = "Produto A",
      Valor = 100.0m
    };

    // SetCreatedBy recebe CreatedBy (Value Object), mas Guid converte implicitamente.
    produto.SetCreatedBy(Guid.NewGuid());
  }
}
```

### [Entidade Detalhada](#detailedentity)

```csharp
using Tooark.Entities;

public class Produto : DetailedEntity
{
  public string Nome { get; set; } = string.Empty;
  public decimal Valor { get; set; }
}

public class Program
{
  public static void Main()
  {
    var produto = new Produto
    {
      Nome = "Produto A",
      Valor = 100.0m
    };

    produto.SetCreatedBy(Guid.NewGuid());
    produto.SetUpdatedBy(Guid.NewGuid());
  }
}
```

### [Entidade Versionada](#versionedentity)

```csharp
using Tooark.Entities;

public class Produto : VersionedEntity
{
  public string Nome { get; set; } = string.Empty;
  public decimal Valor { get; set; }
}

public class Program
{
  public static void Main()
  {
    var produto = new Produto
    {
      Nome = "Produto A",
      Valor = 100.0m
    };

    produto.SetCreatedBy(Guid.NewGuid());
    produto.SetUpdatedBy(Guid.NewGuid());

    var version = produto.Version;
  }
}
```

### [Entidade Deletável](#softdeletableentity)

```csharp
using Tooark.Entities;

public class Produto : SoftDeletableEntity
{
  public string Nome { get; set; } = string.Empty;
  public decimal Valor { get; set; }
}

public class Program
{
  public static void Main()
  {
    var produto = new Produto
    {
      Nome = "Produto A",
      Valor = 100.0m
    };

    produto.SetCreatedBy(Guid.NewGuid());
    produto.SetDeleted(Guid.NewGuid());
    produto.SetRestored(Guid.NewGuid());
  }
}
```

### [Entidade Auditável](#auditableentity)

```csharp
using Tooark.Entities;

public class Produto : AuditableEntity
{
  public string Nome { get; set; } = string.Empty;
  public decimal Valor { get; set; }
}

public class Program
{
  public static void Main()
  {
    var produto = new Produto
    {
      Nome = "Produto A",
      Valor = 100.0m
    };

    produto.SetCreatedBy(Guid.NewGuid());
    produto.SetUpdatedBy(Guid.NewGuid());
    produto.SetDeleted(Guid.NewGuid());
    produto.SetRestored(Guid.NewGuid());
  }
}
```

### [Entidade de Arquivo](#fileentity)

```csharp
using Tooark.Entities;
using Tooark.Enums;
using Tooark.ValueObjects;

public class Arquivo : FileEntity
{
  public Arquivo(string link, string name, string title, Guid createdById)
    : base(new FileStorage(link, name), new Title(title), new CreatedBy(createdById))
  { }

  public Arquivo(string link, string name, string title, string fileFormat, EFileType type, long size, Guid createdById)
    : base(new FileStorage(link, name), new Title(title), fileFormat, type, size, createdById)
  { }
}

public class Program
{
  public static void Main()
  {
    var arquivo = new Arquivo(
      link: "https://bucket.com/arquivo.pdf",
      name: "Arquivo.pdf",
      title: "Arquivo de teste",
      createdById: Guid.NewGuid()
    );

    var arquivoDetalhado = new Arquivo(
      link: "https://bucket.com/arquivo.pdf",
      name: "Arquivo.pdf",
      title: "Arquivo de teste",
      fileFormat: "pdf",
      type: EFileType.Document,
      size: 1024,
      createdById: Guid.NewGuid()
    );
  }
}
```

---

## 📋 Dependências

| Projeto                | Versão | Descrição                                                   |
| ---------------------- | ------ | ----------------------------------------------------------- |
| `Tooark.Enums`         | —      | Tipos/enums compartilhados (ex.: `EFileType`)               |
| `Tooark.Exceptions`    | —      | Exceções (ex.: `BadRequestException`)                       |
| `Tooark.Notifications` | —      | Base de notificações usada pelas entidades                  |
| `Tooark.Utils`         | —      | Utilitários internos do toolkit                             |
| `Tooark.ValueObjects`  | —      | Value Objects usados por propriedades/métodos/constructores |

---

## ⚠️ Códigos de Erro, Notificações e Soluções

Os códigos de erro para notificações seguem o padrão `T.ENT.<SIGLA><N>` (ex.: `T.ENT.BAS1`).

Alguns códigos utilizados diretamente nas entidades:

- `BaseEntity`: `T.ENT.BAS1`, `T.ENT.BAS2`
- `InitialEntity`: `T.ENT.INI1`
- `SoftDeletableEntity`: `T.ENT.SOF1`
- `AuditableEntity`: `T.ENT.AUD1`

Tabela de erros/notificações:

| Entidade              | Mensagem                     | Descrição                           | Solução                                                                  | Retorno      |
| --------------------- | ---------------------------- | ----------------------------------- | ------------------------------------------------------------------------ | ------------ |
| `BaseEntity`          | `Empty;Id`                   | Identificador vazio                 | Defina um identificador válido para a entidade                           | Notification |
| `BaseEntity`          | `ChangeBlocked;Id`           | Identificador não pode ser alterado | Informe o identificador do registro                                      | Notification |
| `InitialEntity`       | `ChangeBlocked;CreatedBy`    | Criador não pode ser alterado       | Informe o criador do registro                                            | Exception    |
| `InitialEntity`       | `Field.Invalid;CreatedBy`    | Campo do Criador inválido           | Informe um criador válido                                                | Exception    |
| `DetailedEntity`      | `ChangeBlocked;CreatedBy`    | Criador não pode ser alterado       | Informe o criador do registro                                            | Exception    |
| `DetailedEntity`      | `Field.Invalid;CreatedBy`    | Campo do Criador inválido           | Informe um criador válido                                                | Exception    |
| `DetailedEntity`      | `Field.Invalid;UpdatedBy`    | Campo do Atualizador inválido       | Informe um atualizador válido                                            | Exception    |
| `VersionedEntity`     | `ChangeBlocked;CreatedBy`    | Criador não pode ser alterado       | Informe o criador do registro                                            | Exception    |
| `VersionedEntity`     | `Field.Invalid;CreatedBy`    | Campo do Criador inválido           | Informe um criador válido                                                | Exception    |
| `VersionedEntity`     | `Field.Invalid;UpdatedBy`    | Campo do Atualizador inválido       | Informe um atualizador válido                                            | Exception    |
| `SoftDeletableEntity` | `ChangeBlocked;CreatedBy`    | Criador não pode ser alterado       | Informe o criador do registro                                            | Exception    |
| `SoftDeletableEntity` | `Field.Invalid;CreatedBy`    | Campo do Criador inválido           | Informe um criador válido                                                | Exception    |
| `SoftDeletableEntity` | `Field.Invalid;UpdatedBy`    | Campo do Atualizador inválido       | Informe um atualizador válido                                            | Exception    |
| `SoftDeletableEntity` | `Record.Deleted`             | Registro deletado                   | Análise se é necessário restaurar o registro antes de realizar operações | Notification |
| `SoftDeletableEntity` | `Record.Deleted`             | Registro deletado                   | Restaure o registro se necessário antes de realizar operações            | Exception    |
| `AuditableEntity`     | `ChangeBlocked;CreatedBy`    | Criador não pode ser alterado       | Informe o criador do registro                                            | Exception    |
| `AuditableEntity`     | `Field.Invalid;CreatedBy`    | Campo do Criador inválido           | Informe um criador válido                                                | Exception    |
| `AuditableEntity`     | `Field.Invalid;UpdatedBy`    | Campo do Atualizador inválido       | Informe um atualizador válido                                            | Exception    |
| `AuditableEntity`     | `Field.Invalid;DeletedBy`  | Campo do Deletador inválido         | Informe um deletador válido                                              | Exception    |
| `AuditableEntity`     | `Field.Invalid;RestoredBy` | Campo do Restaurador inválido       | Informe um restaurador válido                                            | Exception    |
| `AuditableEntity`     | `Record.Deleted`             | Registro deletado                   | Análise se é necessário restaurar o registro antes de realizar operações | Notification |
| `AuditableEntity`     | `Record.Deleted`             | Registro deletado                   | Restaure o registro se necessário antes de realizar operações            | Exception    |

---

## 🪪 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests no repositório [Tooark.Entities](https://github.com/Tooark/tooark/issues).

## 📄 Licença

Este projeto está licenciado sob a licença BSD 3-Clause. Veja o arquivo [LICENSE](../LICENSE) para mais detalhes.
