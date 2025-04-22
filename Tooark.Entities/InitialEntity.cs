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
  /// <summary>
  /// Identificador do usuário que criou a entidade.
  /// </summary>
  /// <value>
  /// O identificador do criador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'createdby' é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("createdby", TypeName = "uuid")]
  [Required]
  public Guid CreatedBy { get; private set; }

  /// <summary>
  /// Data e hora de criação da entidade.
  /// </summary>
  /// <value>
  /// A data e hora de criação é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'createdat' é do tipo 'timestamp with time zone' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("createdat", TypeName = "timestamp with time zone")]
  [Required]
  public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


  /// <summary>
  /// Cria uma nova instância da entidade inicial.
  /// </summary>
  protected InitialEntity()
  { }

  /// <summary>
  /// Cria uma nova instância da entidade inicial.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  protected InitialEntity(CreatedBy createdBy)
  {
    // Define o identificador do criador
    SetCreatedBy(createdBy);
  }


  /// <summary>
  /// Define o identificador do criador da entidade e a data e hora de criação.
  /// </summary>
  /// <param name="createdBy">O valor do identificador do criador a ser definido.</param>
  public void SetCreatedBy(CreatedBy createdBy)
  {
    // Verifica se o identificador da entidade é vazio
    if (CreatedBy != Guid.Empty)
    {
      // Adiciona uma notificação de erro
      AddNotification("ChangeNotAllowed;CreatedBy", "CreatedBy", "T.ENT.INI1");
    }
    else
    {
      // Adiciona notificações
      AddNotifications(createdBy);

      // Verifica se não houve notificações de erro
      if (IsValid)
      {
        // Define o identificador do criador
        CreatedBy = createdBy;

        // Define a data e hora de criação
        CreatedAt = DateTime.UtcNow;
      }
    }
  }
}
