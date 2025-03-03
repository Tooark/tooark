using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.Enums;
using Tooark.Validations;

namespace Tooark.Entities;

/// <summary>
/// Entidade para gerenciar arquivos dentro do bucket.
/// </summary>
/// <remarks>
/// Herda de <see cref="InitialEntity"/> para incluir informações iniciais.
/// Esta classe é usada para representar arquivos armazenados no bucket.
/// </remarks>
public abstract class FileEntity : InitialEntity
{
  /// <summary>
  /// Nome do arquivo incluindo path do bucket.
  /// </summary>
  /// <value>
  /// O nome do arquivo é do tipo <see cref="string"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'filename' é do tipo 'text' e é obrigatório.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("filename", TypeName = "text")]
  [Required]
  public string FileName { get; set; } = null!;

  /// <summary>
  /// Título do arquivo para exibição.
  /// </summary>
  /// <value>
  /// O título do arquivo é do tipo <see cref="string"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'title' é do tipo 'varchar(255)' e é obrigatório.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("title", TypeName = "varchar(255)")]
  [Required]
  public string Title { get; set; } = null!;

  /// <summary>
  /// Link do arquivo para utilização em páginas web.
  /// </summary>
  /// <value>
  /// O link público do arquivo é do tipo <see cref="string"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'link' é do tipo 'text' e é obrigatório.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("link", TypeName = "text")]
  [Required]
  public string? Link { get; set; } = null;

  /// <summary>
  /// Formato do arquivo.
  /// </summary>
  /// <value>
  /// O formato do arquivo é do tipo <see cref="string"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'fileformat' é do tipo 'varchar(10)'.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("fileformat", TypeName = "varchar(10)")]
  public string? FileFormat { get; set; } = null;

  /// <summary>
  /// Tipo do arquivo.
  /// </summary>
  /// <value>
  /// O tipo do arquivo é do tipo <see cref="int"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'type' é do tipo 'int'.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("type", TypeName = "int")]
  public EFileType Type { get; set; } = EFileType.Unknown;

  /// <summary>
  /// Tamanho do arquivo.
  /// </summary>
  /// <value>
  /// O tamanho do arquivo é do tipo <see cref="long"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'size' é do tipo 'bigint'.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("size", TypeName = "bigint")]
  public long Size { get; set; } = 0;


  /// <summary>
  /// Construtor da classe FileEntity.
  /// </summary>
  protected FileEntity()
  { }

  /// <summary>
  /// Construtor da classe FileEntity.
  /// </summary>
  /// <param name="fileName">O nome do arquivo.</param>
  /// <param name="title">O título do arquivo.</param>
  /// <param name="createdBy">O identificador do usuário que criou o arquivo.</param>
  protected FileEntity(string fileName, string title, Guid createdBy)
  {
    // Valida os parâmetros
    AddNotifications(new Validation()
      .IsUrl(fileName, "FileName", "Field.Invalid;FileName")
      .IsNotNullOrEmpty(title, "Title", "Field.Required;Title")
    );

    // Define o identificador do criador
    SetCreatedBy(createdBy);

    // Verifica se não houve notificações de erro
    if (IsValid)
    {
      FileName = fileName;
      Title = title;
      Link = fileName;
    }
  }

  /// <summary>
  /// Construtor da classe FileEntity.
  /// </summary>
  /// <param name="fileName">O nome do arquivo.</param>
  /// <param name="title">O título do arquivo.</param>
  /// <param name="link">O link do arquivo.</param>
  /// <param name="createdBy">O identificador do usuário que criou o arquivo.</param>
  protected FileEntity(string fileName, string title, string link, Guid createdBy)
  {
    // Valida os parâmetros
    AddNotifications(new Validation()
      .IsUrl(fileName, "FileName", "Field.Invalid;FileName")
      .IsNotNullOrEmpty(title, "Title", "Field.Required;Title")
      .IsUrl(link, "Link", "Field.Invalid;Link")
    );

    // Define o identificador do criador
    SetCreatedBy(createdBy);

    // Verifica se não houve notificações de erro
    if (IsValid)
    {
      FileName = fileName;
      Title = title;
      Link = link;
    }
  }

  /// <summary>
  /// Construtor da classe FileEntity.
  /// </summary>
  /// <param name="fileName">O nome do arquivo.</param>
  /// <param name="title">O título do arquivo.</param>
  /// <param name="link">O link do arquivo.</param>
  /// <param name="fileFormat">O formato do arquivo.</param>
  /// <param name="type">O tipo do arquivo.</param>
  /// <param name="size">O tamanho do arquivo.</param>
  /// <param name="createdBy">O identificador do usuário que criou o arquivo.</param>
  protected FileEntity(string fileName, string title, string link, string fileFormat, EFileType type, long size, Guid createdBy)
  {
    // Valida os parâmetros
    AddNotifications(new Validation()
      .IsUrl(fileName, "FileName", "Field.Invalid;FileName")
      .IsNotNullOrEmpty(title, "Title", "Field.Required;Title")
      .IsUrl(link, "Link", "Field.Invalid;Link")
      .IsNotNullOrEmpty(fileFormat, "FileFormat", "Field.Required;FileFormat")
      .IsNotNullOrEmpty(type, "Type", "Field.Required;Type")
      .IsGreaterOrEquals(size, 0, "Size", "Field.Invalid;Size")
    );

    // Define o identificador do criador
    SetCreatedBy(createdBy);

    // Verifica se não houve notificações de erro
    if (IsValid)
    {
      FileName = fileName;
      Title = title;
      Link = link;
      FileFormat = fileFormat;
      Type = type;
      Size = size;
    }
  }
}
