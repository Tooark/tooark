using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
  /// <summary>
  /// Identificador do usuário que criou a entidade.
  /// </summary>
  /// <value>
  /// O identificador do criador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'createdby' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("createdby", TypeName = "uuid")]
  [Required]
  public Guid CreatedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora de criação da entidade.
  /// </summary>
  /// <value>
  /// A data e hora de criação é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'createdat' e é obrigatória do tipo
  /// 'timestamp with time zone'.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("createdat", TypeName = "timestamp with time zone")]
  [Required]
  public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


  /// <summary>
  /// Define o identificador do criador da entidade e a data e hora de criação.
  /// </summary>
  /// <param name="createdBy">O valor do identificador do criador a ser definido.</param>
  public void SetCreatedBy(Guid createdBy)
  {
    // Verifica se o identificador da entidade é vazio
    if (CreatedBy != Guid.Empty)
    {
      // Adiciona uma notificação de erro
      AddNotification("ChangeNotAllowed;CreatedBy", "CreatedBy");
    }
    else
    {
      // Verifica se o parâmetro é vazio
      if (createdBy == Guid.Empty)
      {
        // Adiciona uma notificação de erro
        AddNotification("IdentifierEmpty;CreatedBy", "CreatedBy");
      }
      else
      {
        // Define o identificador do criador
        CreatedBy = createdBy;

        // Define a data e hora de criação
        CreatedAt = DateTime.UtcNow;
      }
    }
  }
}
