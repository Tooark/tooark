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
  /// URL do arquivo para utilização em APIs.
  /// </summary>
  /// <value>
  /// A URL do arquivo é do tipo <see cref="string"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'fileurl' é do tipo 'text' e é obrigatório.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("fileurl", TypeName = "text")]
  [Required]
  public string FileUrl { get; set; } = null!;

  /// <summary>
  /// Título do arquivo para exibição.
  /// </summary>
  /// <value>
  /// O título do arquivo é do tipo <see cref="string"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'name' é do tipo 'varchar(255)' e é obrigatório.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("name", TypeName = "varchar(255)")]
  [Required]
  public string Name { get; set; } = null!;

  /// <summary>
  /// URL pública do arquivo para utilização em links e páginas web.
  /// </summary>
  /// <value>
  /// A URL pública do arquivo é do tipo <see cref="string"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'publicurl' é do tipo 'text' e é obrigatório.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("publicurl", TypeName = "text")]
  [Required]
  public string? PublicUrl { get; set; } = null;

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
  /// Construtor da classe FileEntity.
  /// </summary>
  protected FileEntity()
  { }

  /// <summary>
  /// Construtor da classe FileEntity.
  /// </summary>
  /// <param name="fileUrl">A URL do arquivo.</param>
  /// <param name="name">O título do arquivo.</param>
  /// <param name="createdBy">O identificador do usuário que criou o arquivo.</param>
  protected FileEntity(string fileUrl, string name, Guid createdBy)
  {
    // Valida os parâmetros
    AddNotifications(new Validation()
      .IsUrl(fileUrl, "FileUrl", "Field.Invalid;FileUrl")
      .IsNotNullOrEmpty(name, "Name", "Field.Required;Name")
    );

    // Define o identificador do criador
    SetCreatedBy(createdBy);

    // Verifica se não houve notificações de erro
    if (IsValid)
    {
      FileUrl = fileUrl;
      Name = name;
      PublicUrl = fileUrl;
    }
  }

  /// <summary>
  /// Construtor da classe FileEntity.
  /// </summary>
  /// <param name="fileUrl">A URL do arquivo.</param>
  /// <param name="name">O título do arquivo.</param>
  /// <param name="publicUrl">A URL pública do arquivo. Parâmetro opcional.</param>
  /// <param name="createdBy">O identificador do usuário que criou o arquivo.</param>
  protected FileEntity(string fileUrl, string name, string publicUrl, Guid createdBy)
  {
    // Valida os parâmetros
    AddNotifications(new Validation()
      .IsUrl(fileUrl, "FileUrl", "Field.Invalid;FileUrl")
      .IsNotNullOrEmpty(name, "Name", "Field.Required;Name")
      .IsUrl(publicUrl, "PublicUrl", "Field.Invalid;PublicUrl")
    );

    // Define o identificador do criador
    SetCreatedBy(createdBy);

    // Verifica se não houve notificações de erro
    if (IsValid)
    {
      FileUrl = fileUrl;
      Name = name;
      PublicUrl = publicUrl;
    }
  }

  /// <summary>
  /// Construtor da classe FileEntity.
  /// </summary>
  /// <param name="fileUrl">A URL do arquivo.</param>
  /// <param name="name">O título do arquivo.</param>
  /// <param name="publicUrl">A URL pública do arquivo.</param>
  /// <param name="fileFormat">O formato do arquivo.</param>
  /// <param name="type">O tipo do arquivo.</param>
  /// <param name="createdBy">O identificador do usuário que criou o arquivo.</param>
  protected FileEntity(string fileUrl, string name, string publicUrl, string fileFormat, EFileType type, Guid createdBy)
  {
    // Valida os parâmetros
    AddNotifications(new Validation()
      .IsUrl(fileUrl, "FileUrl", "Field.Invalid;FileUrl")
      .IsNotNullOrEmpty(name, "Name", "Field.Required;Name")
      .IsUrl(publicUrl, "PublicUrl", "Field.Invalid;PublicUrl")
      .IsNotNullOrEmpty(fileFormat, "FileFormat", "Field.Required;FileFormat")
      .IsNotNullOrEmpty(type, "Type", "Field.Required;Type")
    );

    // Define o identificador do criador
    SetCreatedBy(createdBy);

    // Verifica se não houve notificações de erro
    if (IsValid)
    {
      FileUrl = fileUrl;
      Name = name;
      PublicUrl = publicUrl;
      FileFormat = fileFormat;
      Type = type;
    }
  }
}
