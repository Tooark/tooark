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
  /// Cria uma nova instância da entidade exclusão lógica.
  /// </summary>
  protected SoftDeletableEntity()
  { }

  /// <summary>
  /// Cria uma nova instância da entidade exclusão lógica.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  protected SoftDeletableEntity(CreatedBy createdBy)
  {
    // Define o identificador do criador
    SetCreatedBy(createdBy);
  }


  /// <summary>
  /// Marca a entidade como excluída logicamente.
  /// </summary>
  /// <param name="changedBy">O identificador do usuário que excluiu a entidade.</param>
  public void SetDeleted(UpdatedBy changedBy)
  {
    // Adiciona notificações de erro
    AddNotifications(changedBy);

    // Verifica se o parâmetro é vazio
    if (IsValid && !Deleted)
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
  public void SetRestored(UpdatedBy changedBy)
  {
    // Adiciona notificações de erro
    AddNotifications(changedBy);

    // Verifica se o parâmetro é vazio
    if (IsValid && Deleted)
    {
      // Define o identificador do usuário que restaurou excluiu a entidade
      SetUpdatedBy(changedBy);

      // Define a entidade como não excluída
      Deleted = false;
    }
  }
}
