using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades detalhadas.
/// Herda de <see cref="InitialEntity"/> para incluir informações de criação.
/// Esta classe é usada para representar entidades com informações de auditoria,
/// como quem criou e atualizou a entidade, além de quando esses eventos ocorreram.
/// </summary>
public abstract class DetailedEntity : InitialEntity
{
  /// <summary>
  /// Identificador do usuário que atualizou a entidade pela última vez.
  /// </summary>
  /// <value>
  /// O identificador do atualizador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'updatedby' e é obrigatória.
  /// </remarks>
  [Column("updatedby")]
  [Required]
  public Guid UpdatedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora da última atualização da entidade.
  /// </summary>
  /// <value>
  /// A data e hora da última atualização é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'updatedat' e é obrigatória
  /// do tipo 'timestamp with time zone'.
  /// </remarks>
  [Column("updatedat", TypeName = "timestamp with time zone")]
  [Required]
  public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

  /// <summary>
  /// Define o identificador do criador e o atualizador da entidade.
  /// </summary>
  /// <param name="createdBy">O valor do identificador do criador a ser definido.</param>
  /// <exception cref="InvalidOperationException">Lançada quando uma tentativa é feita para alterar o criador após a criação inicial.</exception>
  public new void SetCreatedBy(Guid createdBy)
  {
    base.SetCreatedBy(createdBy);

    UpdatedBy = createdBy;
  }

  /// <summary>
  /// Define o identificador do atualizador da entidade e atualiza a data e hora da última atualização.
  /// </summary>
  /// <param name="updatedBy">O valor do identificador do atualizador a ser definido.</param>
  /// <exception cref="ArgumentException">Lançada se parâmetro do atualizador não estiver sido definido.</exception>
  public void SetUpdatedBy(Guid updatedBy)
  {
    if (updatedBy != Guid.Empty)
    {
      UpdatedBy = updatedBy;
      UpdatedAt = DateTime.UtcNow;
    }
    else
    {
      throw new ArgumentException("IdentifierEmpty;UpdateBy");
    }
  }
}
