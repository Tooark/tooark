using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.Notifications;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades.
/// </summary>
public abstract class BaseEntity : Notification, IEquatable<Guid>
{
  #region Constructors

  /// <summary>
  /// Construtor vazio para a entidade BaseEntity.
  /// </summary>
  /// <remarks>
  /// Utilizado pelo Entity Framework.
  /// </remarks>
  protected BaseEntity() { }

  #endregion

  #region Properties

  /// <summary>
  /// Identificador único para a entidade.
  /// </summary>
  /// <value>
  /// O identificador é do tipo <see cref="Guid"/> e é gerado automaticamente quando
  /// a entidade é criada.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'id' é do tipo 'uuid' e é obrigatória.
  /// </remarks>
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("id", TypeName = "uuid")]
  [Required]
  public Guid Id { get; private set; } = Guid.NewGuid();

  #endregion
  
  #region Methods

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
      AddNotification("IdentifierEmpty;Id", "Id", "T.ENT.BAS1");
    }
    else
    {
      // Define o identificador
      Id = id;
    }
  }

  #endregion

  #region Equatable Implementation

  /// <summary>
  /// Compara o identificador da entidade com outro identificador.
  /// </summary>
  /// <param name="id">O identificador a ser comparado.</param>
  /// <returns>Verdadeiro se os identificadores forem iguais, falso caso contrário.</returns>
  public bool Equals(Guid id) => Id == id;

  /// <summary>
  /// Sobrecarga do método GetHashCode.
  /// </summary>
  public override int GetHashCode() => Id.GetHashCode();

  #endregion
}
