# Tooark.Entities

Biblioteca para facilitar a criação, gerenciamento e manutenção de entidades base em projetos .NET.

## Enumeradores

### EFileType

Enumerador que define os tipos de arquivos.

- **Valores:**
  - `Image`: Imagem.
  - `Document`: Documento.
  - `Video`: Vídeo.
  - `Audio`: Áudio.
  - `Unknown`: Tipo desconhecido.

## Classes

### BaseEntity

Classe base abstrata para entidades.

- **Campos:**

  - `Id` (Guid): Identificador único para a entidade.

- **Métodos:**
  - `SetId(Guid id)`: Define o identificador único para a entidade.

### InitialEntity

Classe base abstrata para entidades iniciais. Herda de [`BaseEntity`](#baseentity).

- **Campos:**

  - `CreatedBy` (Guid): Identificador do usuário que criou a entidade.
  - `CreatedAt` (DateTime): Data e hora de criação da entidade.

- **Métodos:**
  - `SetCreatedBy(Guid createdBy)`: Define o identificador do criador da entidade e a data e hora de criação.

### DetailedEntity

Classe base abstrata para entidades detalhadas. Herda de [`InitialEntity`](#initialentity).

- **Campos:**

  - `UpdatedBy` (Guid): Identificador do usuário que atualizou a entidade pela última vez.
  - `UpdatedAt` (DateTime): Data e hora da última atualização da entidade.

- **Métodos:**
  - `SetCreatedBy(Guid createdBy)`: Define o identificador do criador e o atualizador da entidade.
  - `SetUpdatedBy(Guid updatedBy)`: Define o identificador do atualizador da entidade e a data e hora da última atualização.

### VersionedEntity

Classe base abstrata para entidades que suportam versionamento. Herda de [`DetailedEntity`](#detailedentity).

- **Campos:**

  - `Version` (long): Versão da entidade.

- **Métodos:**
  - `SetUpdatedBy(Guid updatedBy)`: Incrementa a versão da entidade ao atualizar.
  - `IncrementVersion()`: Incrementa a versão da entidade.

### SoftDeletableEntity

Classe base abstrata para entidades que suportam exclusão lógica. Herda de [`DetailedEntity`](#detailedentity).

- **Campos:**

  - `Deleted` (bool): Indica se a entidade foi excluída logicamente.

- **Métodos:**
  - `SetDeleted(Guid changedBy)`: Marca a entidade como excluída logicamente.
  - `SetRestored(Guid changedBy)`: Marca a entidade como não excluída logicamente.

### AuditableEntity

Classe base abstrata para entidades que precisam de auditoria completa. Herda de [`DetailedEntity`](#detailedentity).

- **Campos:**

  - `Version` (long): Versão da entidade.
  - `Deleted` (bool): Indica se a entidade foi excluída logicamente.
  - `DeletedBy` (Guid): Identificador do usuário que excluiu a entidade.
  - `DeletedAt` (DateTime?): Data e hora da exclusão da entidade.
  - `RestoredBy` (Guid): Identificador do usuário que restaurou a entidade.
  - `RestoredAt` (DateTime?): Data e hora da restauração da entidade.

- **Métodos:**
  - `SetDeleted(Guid deletedBy)`: Marca a entidade como excluída.
  - `SetRestored(Guid restoredBy)`: Marca a entidade como restaurada.
  - `IncrementVersion()`: Incrementa a versão da entidade.

### FileEntity

Classe base abstrata para entidades que gerenciam arquivos dentro do bucket. Herda de [`InitialEntity`](#initialentity).

- **Campos:**
  - `FileUrl` (string): URL do arquivo no bucket.
  - `Name` (string): Nome do arquivo.
  - `PublicUrl` (string): URL pública do arquivo.
  - `FileFormat` (string): Formato do arquivo.
  - `Type` (string): Tipo do arquivo.
- **Construtores:**
  - `FileEntity(string fileUrl, string name, Guid createdBy)`: Inicializa uma nova instância da classe `FileEntity`.
  - `FileEntity(string fileUrl, string name, string publicUrl, Guid createdBy)`: Inicializa uma nova instância da classe `FileEntity`.
  - `FileEntity(string fileUrl, string name, string publicUrl, string fileFormat, EFileType type, Guid createdBy)`: Inicializa uma nova instância da classe `FileEntity`.

## Exemplo de Uso

**Utilizando os enumeradores:**

```csharp
using Tooark.Entities.Enums;

int typeInt = EFileType.Image; // 2
string typeString = EFileType.Image; // Image
```

**Utilizando a entidade:**

```csharp
using Tooark.Entities;

public class Produto : AuditableEntity
{
  public string Nome { get; set; }
  public decimal Valor { get; set; }
}

public class FileRepository : FileEntity
{
  public string Bucket { get; set; }
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

    // Definindo o criador da entidade
    produto.SetCreatedBy(Guid.NewGuid());

    // Atualizando a entidade
    produto.SetUpdatedBy(Guid.NewGuid());

    // Excluindo logicamente a entidade
    produto.SetDeleted(Guid.NewGuid());

    // Restaurando a entidade
    produto.SetRestored(Guid.NewGuid());

    var fileUrl = "https://bucket.s3.amazonaws.com/documento.pdf";
    var name = "Documento Exemplo";

    var file = new FileRepository(fileUrl, name, Guid.NewGuid())
    {
      Bucket = "bucket"
    };
  }
}
```
