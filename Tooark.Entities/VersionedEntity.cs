using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades que suportam versionamento.
/// </summary>
/// /// <remarks>
/// Herda de <see cref="DetailedEntity"/> para incluir informações para auditoria.
/// Esta classe é usada para representar entidades que suportam versionamento.
/// </remarks>
public abstract class VersionedEntity : DetailedEntity
{
  /// <summary>
  /// Versão da entidade.
  /// </summary>
  /// <value>
  /// O valor é do tipo <see cref="long"/>.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'version'.
  /// </remarks>
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("version", TypeName = "bigint")]
  [DefaultValue(1)]
  [Required]
  public long Version { get; private set; } = 1;

  /// <summary>
  /// Incrementa a versão da entidade.
  /// </summary>
  /// <remarks>
  /// Este método deve ser chamado sempre que a entidade for atualizada.
  /// </remarks>
  /// <param name="updatedBy">O identificador do usuário que atualizou a entidade.</param>
  public new void SetUpdatedBy(Guid updatedBy)
  {
    // Chama o método da classe base
    base.SetUpdatedBy(updatedBy);
    
    // Verifica se não houve notificações de erro
    if (IsValid)
    {
      // Incrementa a versão
      IncrementVersion();
    }
  }

  /// <summary>
  /// Incrementa a versão da entidade.
  /// </summary>
  private void IncrementVersion()
  {
    // Incrementa a versão
    Version++;
  }
}
