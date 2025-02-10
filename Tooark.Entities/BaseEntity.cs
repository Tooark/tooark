using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.Notifications;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades.
/// </summary>
public abstract class BaseEntity : Notification
{
  /// <summary>
  /// Identificador único para a entidade.
  /// </summary>
  /// <value>
  /// O identificador é do tipo <see cref="Guid"/> e é gerado automaticamente quando
  /// a entidade é criada.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'id' e é obrigatória.
  /// </remarks>
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("id", TypeName = "uuid")]
  [Required]
  public Guid Id { get; private set; } = Guid.NewGuid();

  /// <summary>
  /// Define o identificador único para a entidade.
  /// </summary>
  /// <param name="id">O valor do identificador a ser definido.</param>
  public void SetId(Guid id)
  {
    // Verifica se o identificador é vazio
    if (id == Guid.Empty)
    {
      // Adiciona uma notificação de erro
      AddNotification("IdentifierEmpty;Id", "Id");
    }
    else
    {
      // Define o identificador
      Id = id;
    }
  }
}
