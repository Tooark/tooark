using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades que precisam de auditoria completa.
/// </summary>
/// <remarks>
/// Herda de <see cref="DetailedEntity"/> para incluir informações para auditoria.
/// Esta classe é usada para representar entidades que precisam de auditoria completa.
/// </remarks>
public abstract class AuditableEntity : DetailedEntity
{
  /// <summary>
  /// Versão da entidade.
  /// </summary>
  /// <value>
  /// O valor é do tipo <see cref="long"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'version' é do tipo 'bigint' com valor padrão '1' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("version", TypeName = "bigint")]
  [DefaultValue(1)]
  [Required]
  public long Version { get; private set; } = 1;

  /// <summary>
  /// Indica se a entidade foi excluída logicamente.
  /// </summary>
  /// <value>
  /// O valor é do tipo <see cref="bool"/>. O valor padrão é 'false'.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'deleted' é do tipo 'bool' com valor padrão 'false' e é obrigatória.
  /// </remarks>
  [Column("deleted", TypeName = "bool")]
  [DefaultValue(false)]
  [Required]
  public bool Deleted { get; private set; } = false;

  /// <summary>
  /// Identificador do usuário que excluiu a entidade.
  /// </summary>
  /// <value>
  /// O identificador do excluidor é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'deletedby' é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("deletedby", TypeName = "uuid")]
  [Required]
  public Guid DeletedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora da exclusão da entidade.
  /// </summary>
  /// <value>
  /// A data e hora da exclusão é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'deletedat' é do tipo 'timestamp with time zone' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("deletedat", TypeName = "timestamp with time zone")]
  public DateTime? DeletedAt { get; private set; }

  /// <summary>
  /// Identificador do usuário que restaurou a entidade.
  /// </summary>
  /// <value>
  /// O identificador do restaurador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'restoredby' é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("restoredby", TypeName = "uuid")]
  [Required]
  public Guid RestoredBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora da restauração da entidade.
  /// </summary>
  /// <value>
  /// A data e hora da restauração é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'restoredat' é do tipo 'timestamp with time zone' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("restoredat", TypeName = "timestamp with time zone")]
  public DateTime? RestoredAt { get; private set; }


  /// <summary>
  /// Cria uma nova instância da entidade auditoria.
  /// </summary>
  protected AuditableEntity()
  { }

  /// <summary>
  /// Cria uma nova instância da entidade auditoria.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  protected AuditableEntity(Guid createdBy)
  {
    // Define o identificador do criador
    SetCreatedBy(createdBy);
  }


  /// <summary>
  /// Marca a entidade como excluída.
  /// </summary>
  /// <param name="deletedBy">O valor do identificador do excluidor a ser definido.</param>
  public void SetDeleted(Guid deletedBy)
  {
    // Verifica se o parâmetro é vazio
    if (deletedBy == Guid.Empty)
    {
      // Adiciona uma notificação de erro
      AddNotification("IdentifierEmpty;DeletedBy", "DeletedBy");
    }
    else
    {
      // Verifica se a entidade não foi excluída
      if (!Deleted)
      {
        // Define o identificador do usuário que excluiu a entidade
        SetUpdatedBy(deletedBy);

        // Incrementa a versão
        IncrementVersion();

        // Define a entidade como excluída
        Deleted = true;

        // Define o identificador de quem excluiu a entidade
        DeletedBy = deletedBy;

        // Define a data e hora da última exclusão
        DeletedAt = DateTime.UtcNow;
      }
    }
  }

  /// <summary>
  /// Marca a entidade como restaurada.
  /// </summary>
  /// <param name="restoredBy">O valor do identificador do restaurador a ser definido.</param>
  public void SetRestored(Guid restoredBy)
  {

    // Verifica se o parâmetro é vazio
    if (restoredBy == Guid.Empty)
    {
      // Adiciona uma notificação de erro
      AddNotification("IdentifierEmpty;RestoredBy", "RestoredBy");
    }
    else
    {
      // Verifica se a entidade foi excluída
      if (Deleted)
      {
        // Define o identificador do usuário que restaurou a entidade
        SetUpdatedBy(restoredBy);

        // Incrementa a versão
        IncrementVersion();

        // Define a entidade como não excluída
        Deleted = false;

        // Define o identificador de quem restaurou a entidade
        RestoredBy = restoredBy;

        // Define a data e hora da última restauração
        RestoredAt = DateTime.UtcNow;
      }
    }
  }

  /// <summary>
  /// Incrementa a versão da entidade.
  /// </summary>
  private void IncrementVersion()
  {
    // Incrementa a versão
    Version++;
  }
}
