using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.ValueObjects;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades iniciais.
/// </summary>
/// <remarks>
/// Herda de <see cref="BaseEntity"/> para incluir informações de criação.
/// Esta classe é usada para representar entidades com informações de auditoria,
/// como quem criou a entidade, além de quando esse evento ocorreu.
/// </remarks>
public abstract class InitialEntity : BaseEntity
{
  #region Constructors

  /// <summary>
  /// Construtor vazio para a entidade InitialEntity.
  /// </summary>
  /// <remarks>
  /// Utilizado pelo Entity Framework.
  /// </remarks>
  protected InitialEntity() { }

  /// <summary>
  /// Cria uma nova instância da entidade inicial.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  protected InitialEntity(CreatedBy createdBy)
  {
    SetCreatedBy(createdBy);
  }

  #endregion

  #region Properties

  /// <summary>
  /// Identificador do usuário que criou a entidade.
  /// </summary>
  /// <value>
  /// O identificador do criador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'created_by', é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("created_by", TypeName = "uuid")]
  [Required]
  public Guid CreatedBy { get; private set; }

  /// <summary>
  /// Data e hora de criação da entidade.
  /// </summary>
  /// <value>
  /// A data e hora de criação é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'created_at', é do tipo 'timestamp with time zone' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("created_at", TypeName = "timestamp with time zone")]
  [Required]
  public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

  #endregion

  #region Methods

  /// <summary>
  /// Define o identificador do criador da entidade e a data e hora de criação.
  /// </summary>
  /// <param name="createdBy">O valor do identificador do criador a ser definido.</param>
  public virtual void SetCreatedBy(CreatedBy createdBy)
  {
    // Verifica se o identificador da entidade é vazio.
    if (CreatedBy != Guid.Empty)
    {
      AddNotification("ChangeNotAllowed;CreatedBy", "CreatedBy", "T.ENT.INI1");
    }
    else
    {
      // Adiciona as validações dos atributos.
      AddNotifications(createdBy);

      // Verifica se não houve notificações de erros.
      if (IsValid)
      {
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
      }
    }
  }

  #endregion
}
