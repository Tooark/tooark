using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades iniciais.
/// Herda de <see cref="BaseEntity"/> para incluir informações de criação.
/// Esta classe é usada para representar entidades com informações de auditoria,
/// como quem criou a entidade, além de quando esse evento ocorreu.
/// </summary>
public abstract class InitialEntity : BaseEntity
{
  /// <summary>
  /// Identificador do usuário que criou a entidade.
  /// </summary>
  /// <value>
  /// O identificador do criador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'createdby' e é obrigatória.
  /// </remarks>
  [Column("createdby")]
  [Required]
  public Guid CreatedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora de criação da entidade.
  /// </summary>
  /// <value>
  /// A data e hora de criação é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'createdat' e é obrigatória.
  /// </remarks>
  [Column("createdat", TypeName = "timestamp with time zone")]
  [Required]
  public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

  /// <summary>
  /// Define o identificador do criador da entidade.
  /// </summary>
  /// <param name="createdBy">O valor do identificador do criador a ser definido.</param>
  /// <exception cref="InvalidOperationException">Lançada quando uma tentativa é feita para alterar o criador após a criação inicial.</exception>
  public void SetCreatedBy(Guid createdBy)
  {
    if (CreatedBy == Guid.Empty)
    {
      if (createdBy != Guid.Empty)
      {
        CreatedBy = createdBy;
      }
      else
      {
        throw new ArgumentException("IdentifierEmpty;CreatedBy");
      }
    }
    else
    {
      throw new InvalidOperationException("ChangeNotAllowed;CreatedBy");
    }
  }
}
