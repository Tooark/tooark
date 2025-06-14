using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tooark.Notifications;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades.
/// </summary>
public abstract class BaseEntity : Notification, IEquatable<Guid>, IEquatable<BaseEntity>
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
  /// Sobrecarga do método GetHashCode.
  /// </summary>
  public override int GetHashCode() => Id.GetHashCode();

  /// <summary>
  /// Compara o identificador da entidade com outro identificador.
  /// </summary>
  /// <param name="id">O identificador a ser comparado.</param>
  /// <returns>Verdadeiro se os identificadores forem iguais, falso caso contrário.</returns>
  public bool Equals(Guid id) => Id == id;

  /// <summary>
  /// Compara a entidade atual com outra entidade.
  /// </summary>
  /// <param name="other">A outra entidade a ser comparada.</param>
  /// <returns>Verdadeiro se as entidades forem iguais, falso caso contrário.</returns>
  public bool Equals(BaseEntity? other)
  {
    if (other is null)
    {
      return false;
    }

    if (ReferenceEquals(this, other))
    {
      return true;
    }

    return Id.Equals(other.Id);
  }

  /// <summary>
  /// Compara se a entidade atual é igual a um objeto.
  /// </summary>
  /// <param name="left">O objeto a ser comparado.</param>
  /// <param name="right">A outra entidade a ser comparada.</param>
  /// <returns>Verdadeiro se o objeto for igual à entidade, falso caso contrário.</returns>
  public static bool operator ==(BaseEntity? left, BaseEntity? right) => Equals(left, right);

  /// <summary>
  /// Compara se a entidade atual é diferente de um objeto.
  /// </summary>
  /// <param name="left">A entidade a ser comparada.</param>
  /// <param name="right">O objeto a ser comparado.</param>
  /// <returns>Verdadeiro se a entidade for diferente do objeto, falso caso contrário.</returns>
  public static bool operator !=(BaseEntity? left, BaseEntity? right) => !Equals(left, right);

  /// <summary>
  /// Compara se a entidade atual é igual a um objeto.
  /// </summary>
  /// <param name="obj">O objeto a ser comparado.</param>
  /// <returns>Verdadeiro se o objeto for igual à entidade, falso caso contrário.</returns>
  public override bool Equals(object? obj) => Equals(obj as BaseEntity);

  #endregion
}
