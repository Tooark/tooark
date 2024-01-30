namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades detalhadas que herda de BaseEntity.
/// Esta classe é usada para representar entidades com informações de auditoria,
/// como quem criou e atualizou a entidade, além de quando esses eventos ocorreram.
/// </summary>
public abstract class DetailedEntity: BaseEntity
{
  /// <summary>
  /// Identificador do usuário que criou a entidade.
  /// O valor padrão é Guid.Empty, indicando que ainda não foi atribuído.
  /// </summary>
  public Guid CreatedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora de criação da entidade.
  /// O valor padrão é a hora atual em UTC, indicando o momento da criação.
  /// </summary>
  public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

  /// <summary>
  /// Identificador do usuário que atualizou a entidade pela última vez.
  /// O valor padrão é Guid.Empty, indicando que ainda não foi atribuído.
  /// </summary>
  public Guid UpdatedBy { get; private set; } = Guid.Empty;

  /// <summary>
  /// Data e hora da última atualização da entidade.
  /// O valor padrão é a hora atual em UTC, indicando o momento da atualização.
  /// </summary>
  public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

  /// <summary>
  /// Atualiza o usuário que criou e atualizou a entidade.
  /// </summary>
  /// <param name="createdBy">O identificador do usuário que criou a entidade.</param>
  public void SetCreatedBy(Guid createdBy)
  {
    if (CreatedBy == Guid.Empty)
    {
      CreatedBy = createdBy;
      UpdatedBy = createdBy;
    }
    else
    {
      throw new InvalidOperationException("ChangeNotAllowed;CreatedBy");
    }
  }

  /// <summary>
  /// Atualiza o usuário que atualizou a entidade pela última vez e a data da atualização.
  /// </summary>
  /// <param name="updatedBy">O identificador do usuário que atualizou a entidade.</param>
  public void SetUpdatedBy(Guid updatedBy)
  {
    UpdatedBy = updatedBy;
    UpdatedAt = DateTime.UtcNow;
  }
}
