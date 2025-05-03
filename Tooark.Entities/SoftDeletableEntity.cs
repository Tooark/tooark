using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.ValueObjects;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades que suportam exclusão lógica.
/// </summary>
/// <remarks>
/// Herda de <see cref="DetailedEntity"/> para incluir informações para auditoria.
/// Esta classe é usada para representar entidades que podem ser excluídas logicamente.
/// </remarks>
public abstract class SoftDeletableEntity : DetailedEntity
{
  #region Constructors

  /// <summary>
  /// Construtor vazio para a entidade SoftDeletableEntity.
  /// </summary>
  /// <remarks>
  /// Utilizado pelo Entity Framework.
  /// </remarks>
  protected SoftDeletableEntity() { }

  /// <summary>
  /// Cria uma nova instância da entidade exclusão lógica.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  protected SoftDeletableEntity(CreatedBy createdBy) : base(createdBy) { }

  #endregion

  #region Properties

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
      AddNotification("ChangeNotAllowedIsDeleted", "Entity", "T.ENT.SOF1");
    }
  }

  /// <summary>
  /// Marca a entidade como excluída logicamente.
  /// </summary>
  /// <param name="changedBy">O identificador do usuário que excluiu a entidade.</param>
  public void SetDeleted(UpdatedBy changedBy)
  {
    // Adiciona as validações dos atributos.
    AddNotifications(changedBy);

    // Verifica se não houve notificações de erros e se a entidade não foi excluída.
    if (IsValid && !Deleted)
    {
      Deleted = true;

      SetUpdatedBy(changedBy);
    }
  }

  /// <summary>
  /// Marca a entidade como não excluída logicamente.
  /// </summary>
  /// <param name="changedBy">O identificador do usuário que restaurou a entidade.</param>
  public void SetRestored(UpdatedBy changedBy)
  {
    // Adiciona as validações dos atributos.
    AddNotifications(changedBy);

    // Verifica se não houve notificações de erros e se a entidade foi excluída.
    if (IsValid && Deleted)
    {
      Deleted = false;

      SetUpdatedBy(changedBy);
    }
  }

  #endregion
}
