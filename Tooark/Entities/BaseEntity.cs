namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades.
/// </summary>
public abstract class BaseEntity
{
  /// <summary>
  /// Identificador único para a entidade.
  /// </summary>
  public Guid Id { get; private set; } = Guid.NewGuid();

  /// <summary>
  /// Define o identificador único para a entidade.
  /// </summary>
  /// <param name="id">O novo identificador para a entidade.</param>
  public void SetId(Guid id)
  {
    if (id == Guid.Empty)
    {
      throw new ArgumentException("IdentifierEmpty");
    }

    Id = id;
  }
}
