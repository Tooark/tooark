using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tooark.Entities;

/// <summary>
/// Classe base abstrata para entidades.
/// </summary>
public abstract class BaseEntity
{
  /// <summary>
  /// Identificador único para a entidade.
  /// </summary>
  /// <value>
  /// O identificador é do tipo <see cref="Guid"/> e é gerado automaticamente quando a entidade é criada.
  /// </value>
  /// <remarks>
  /// A coluna correspondente no banco de dados é 'id' e é obrigatória.
  /// </remarks>
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  [Column("id")]
  [Required]
  public Guid Id { get; private set; } = Guid.NewGuid();

  /// <summary>
  /// Define o identificador único para a entidade.
  /// </summary>
  /// <param name="id">O valor do identificador a ser definido.</param>
  /// <exception cref="ArgumentException">Lançada quando um <see cref="Guid"/> vazio é passado.</exception>
  public void SetId(Guid id)
  {
    if (id == Guid.Empty)
    {
      throw new ArgumentException("IdentifierEmpty");
    }

    Id = id;
  }
}
