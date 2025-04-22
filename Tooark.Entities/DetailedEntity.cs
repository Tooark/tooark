using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.ValueObjects;

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
  /// <summary>
  /// Identificador do usuário que atualizou a entidade pela última vez.
  /// </summary>
  /// <value>
  /// O identificador do atualizador é do tipo <see cref="Guid"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'updatedby' é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("updatedby", TypeName = "uuid")]
  [Required]
  public Guid UpdatedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora da última atualização da entidade.
  /// </summary>
  /// <value>
  /// A data e hora da última atualização é do tipo <see cref="DateTime"/> em UTC.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'updatedat' é do tipo 'timestamp with time zone' e é obrigatória.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("updatedat", TypeName = "timestamp with time zone")]
  [Required]
  public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;


  /// <summary>
  /// Cria uma nova instância da entidade detalhada.
  /// </summary>
  protected DetailedEntity()
  { }

  /// <summary>
  /// Cria uma nova instância da entidade detalhada.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  protected DetailedEntity(CreatedBy createdBy)
  {
    // Define o identificador do criador
    SetCreatedBy(createdBy);
  }


  /// <summary>
  /// Define o identificador do criador e o atualizador da entidade.
  /// </summary>
  /// <param name="createdBy">O valor do identificador do criador a ser definido.</param>
  public new void SetCreatedBy(CreatedBy createdBy)
  {
    // Chama o método da classe base
    base.SetCreatedBy(createdBy);

    // Verifica se não houve notificações de erro
    if(IsValid)
    {
      // Define o identificador do atualizador
      UpdatedBy = createdBy;
    }
  }

  /// <summary>
  /// Define o identificador do atualizador da entidade e a data e hora da última atualização.
  /// </summary>
  /// <param name="updatedBy">O valor do identificador do atualizador a ser definido.</param>
  public void SetUpdatedBy(UpdatedBy updatedBy)
  {
    // Adiciona notificações
    AddNotifications(updatedBy);
    
    // Verifica se não houve notificações de erro
    if (IsValid)
    {
      // Define o identificador do atualizador
      UpdatedBy = updatedBy;

      // Define a data e hora da última atualização
      UpdatedAt = DateTime.UtcNow;
    }
  }
}
