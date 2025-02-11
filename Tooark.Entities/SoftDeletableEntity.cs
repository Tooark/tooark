using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
  /// Marca a entidade como excluída logicamente.
  /// </summary>
  /// <param name="changedBy">O identificador do usuário que excluiu a entidade.</param>
  public void SetDeleted(Guid changedBy)
  {
    // Verifica se o parâmetro é vazio
    if (DeletedByValid(changedBy) && !Deleted)
    {
      // Define o identificador do usuário que excluiu a entidade
      SetUpdatedBy(changedBy);

      // Define a entidade como excluída
      Deleted = true;
    }
  }

  /// <summary>
  /// Marca a entidade como não excluída logicamente.
  /// </summary>
  /// <param name="changedBy">O identificador do usuário que restaurou a entidade.</param>
  public void SetRestored(Guid changedBy)
  {
    // Verifica se o parâmetro é vazio
    if (DeletedByValid(changedBy) && Deleted)
    {
      // Define o identificador do usuário que restaurou excluiu a entidade
      SetUpdatedBy(changedBy);

      // Define a entidade como não excluída
      Deleted = false;
    }
  }

  /// <summary>
  /// Verifica se o identificador do usuário que excluiu ou restaurou a entidade é válido.
  /// </summary>
  /// <param name="changedBy">O identificador do usuário que excluiu ou restaurou a entidade.</param>
  /// <returns>Verdadeiro se o identificador do usuário é válido.</returns>
  private bool DeletedByValid(Guid changedBy)
  {
    // Verifica se o parâmetro é vazio
    if (changedBy == Guid.Empty)
    {
      // Adiciona uma notificação de erro
      AddNotification("IdentifierEmpty;ChangedBy", "ChangedBy");

      // Retorna falso para indicar que o parâmetro é inválido
      return false;
    }

    // Retorna verdadeiro para indicar que o parâmetro é válido
    return true;
  }
}
