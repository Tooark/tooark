using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.ValueObjects;
using Tooark.Exceptions;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades detalhadas.
/// </summary>
/// <remarks>
/// Herda de <see cref="InitialEntity"/> para incluir informações de criação.
/// Esta classe é usada para representar entidades com informações de auditoria,
/// como quem criou e atualizou a entidade, além de quando esses eventos ocorreram.
/// </remarks>
public abstract class DetailedEntity : InitialEntity
{
  #region Constructors

  /// <summary>
  /// Construtor vazio para a entidade DetailedEntity.
  /// </summary>
  /// <remarks>
  /// Utilizado pelo Entity Framework.
  /// </remarks>
  protected DetailedEntity() { }

  /// <summary>
  /// Cria uma nova instância da entidade detalhada.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  protected DetailedEntity(CreatedBy createdBy)
  {
    SetCreatedBy(createdBy);
  }

  #endregion

  #region Properties

  /// <summary>
  /// Identificador do usuário que atualizou a entidade pela última vez.
  /// </summary>
  /// <value>
  /// O identificador do atualizador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'updated_by' é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("updated_by", TypeName = "uuid")]
  [Required]
  public Guid UpdatedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora da última atualização da entidade.
  /// </summary>
  /// <value>
  /// A data e hora da última atualização é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'updated_at' é do tipo 'timestamp with time zone' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("updated_at", TypeName = "timestamp with time zone")]
  [Required]
  public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

  #endregion

  #region Methods

  /// <summary>
  /// Define o identificador do criador e o atualizador da entidade.
  /// </summary>
  /// <param name="createdBy">O valor do identificador do criador a ser definido.</param>
  public override void SetCreatedBy(CreatedBy createdBy)
  {
    base.SetCreatedBy(createdBy);

    UpdatedBy = createdBy;
  }

  /// <summary>
  /// Define o identificador do atualizador da entidade e a data e hora da última atualização.
  /// </summary>
  /// <param name="updatedBy">O valor do identificador do atualizador a ser definido.</param>
  public virtual void SetUpdatedBy(UpdatedBy updatedBy)
  {
    // Adiciona as validações dos atributos.
    AddNotifications(updatedBy);

    // Se houver notificações, lança exceção de bad request
    if (!IsValid)
    {
      throw new BadRequestException(this);
    }

    UpdatedBy = updatedBy;
    UpdatedAt = DateTime.UtcNow;
  }

  #endregion
}
