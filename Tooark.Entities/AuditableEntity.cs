using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.ValueObjects;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades que precisam de auditoria completa.
/// </summary>
/// <remarks>
/// Herda de <see cref="DetailedEntity"/> para incluir informações para auditoria.
/// Esta classe é usada para representar entidades que precisam de auditoria completa.
/// Contém propriedades para armazenar informações sobre a versão da entidade, se foi excluída logicamente, e se foi restaurada.
/// </remarks>
public abstract class AuditableEntity : DetailedEntity
{
  #region Constructors

  /// <summary>
  /// Construtor vazio para a entidade AuditableEntity.
  /// </summary>
  /// <remarks>
  /// Utilizado pelo Entity Framework.
  /// </remarks>
  protected AuditableEntity() { }

  /// <summary>
  /// Cria uma nova instância da entidade auditoria.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  protected AuditableEntity(CreatedBy createdBy) : base(createdBy) { }

  #endregion

  #region Properties

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
  /// A coluna correspondente no banco de dados é 'deleted_by' é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("deleted_by", TypeName = "uuid")]
  [Required]
  public Guid DeletedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora da exclusão da entidade.
  /// </summary>
  /// <value>
  /// A data e hora da exclusão é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'deleted_at' é do tipo 'timestamp with time zone' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("deleted_at", TypeName = "timestamp with time zone")]
  public DateTime? DeletedAt { get; private set; }

  /// <summary>
  /// Identificador do usuário que restaurou a entidade.
  /// </summary>
  /// <value>
  /// O identificador do restaurador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'restored_by' é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("restored_by", TypeName = "uuid")]
  [Required]
  public Guid RestoredBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora da restauração da entidade.
  /// </summary>
  /// <value>
  /// A data e hora da restauração é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'restored_at' é do tipo 'timestamp with time zone' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("restored_at", TypeName = "timestamp with time zone")]
  public DateTime? RestoredAt { get; private set; }

  #endregion

  #region Private Methods

  /// <summary>
  /// Incrementa a versão da entidade.
  /// </summary>
  private void IncrementVersion()
  {
    Version++;
  }

  #endregion

  #region Methods

  /// <summary>
  /// Verifica se a entidade foi excluída logicamente.
  /// </summary>
  /// <remarks>
  /// Adiciona uma notificação se a entidade foi excluída logicamente e não pode ser alterada.
  /// </remarks>
  public void ChangeNotAllowedIsDeleted()
  {
    // Verifica se a entidade foi excluída logicamente.
    if (Deleted)
    {
      AddNotification("ChangeNotAllowedIsDeleted", "Entity", "T.ENT.AUD1");
    }
  }

  /// <summary>
  /// Atualiza entidade e incrementa a versão.
  /// </summary>
  /// <param name="updatedBy">O valor do identificador do atualizador a ser definido.</param>
  public new void SetUpdatedBy(UpdatedBy updatedBy)
  {
    // Adiciona as validações dos atributos.
    AddNotifications(updatedBy);

    // Verifica se não houve notificações de erro.
    if (IsValid)
    {
      IncrementVersion();

      base.SetUpdatedBy(updatedBy);
    }
  }

  /// <summary>
  /// Marca a entidade como excluída.
  /// </summary>
  /// <param name="deletedBy">O valor do identificador do excluidor a ser definido.</param>
  public void SetDeleted(DeletedBy deletedBy)
  {
    // Adiciona as validações dos atributos.
    AddNotifications(deletedBy);

    // Verifica se não houve notificações de erros e se a entidade não foi excluída.
    if (IsValid && !Deleted)
    {
      Deleted = true;
      DeletedBy = deletedBy;
      DeletedAt = DateTime.UtcNow;

      IncrementVersion();
    }
  }

  /// <summary>
  /// Marca a entidade como restaurada.
  /// </summary>
  /// <param name="restoredBy">O valor do identificador do restaurador a ser definido.</param>
  public void SetRestored(RestoredBy restoredBy)
  {
    // Adiciona as validações dos atributos.
    AddNotifications(restoredBy);

    // Verifica se não houve notificações de erros e se a entidade foi excluída
    if (IsValid && Deleted)
    {
      Deleted = false;
      RestoredBy = restoredBy;
      RestoredAt = DateTime.UtcNow;

      IncrementVersion();
    }
  }

  #endregion
}
